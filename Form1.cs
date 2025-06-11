using System;
using System.Windows.Forms;
using BMDSwitcherAPI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinformApp
{
    public partial class Form1 : Form
    {
        private IBMDSwitcher? switcher;
        private IBMDSwitcherMixEffectBlock? meBlock;
        private Dictionary<long, string> inputMap;
        private IBMDSwitcherMixEffectBlockCallback? meCallback;
        private const string CONFIG_FILE = "atem_config.json";

        private class MixEffectBlockCallback : IBMDSwitcherMixEffectBlockCallback
        {
            private Form1 form;

            public MixEffectBlockCallback(Form1 form)
            {
                this.form = form;
            }

            public void Notify(_BMDSwitcherMixEffectBlockEventType eventType)
            {
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => Notify(eventType)));
                    return;
                }

                switch (eventType)
                {
                    case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeProgramInputChanged:
                        form.UpdateProgramInput();
                        break;
                    case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewInputChanged:
                        form.UpdatePreviewInput();
                        break;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Text = "ATEM Switcher Control";
            inputMap = new Dictionary<long, string>();
            LoadConfig();
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(CONFIG_FILE))
                {
                    string jsonString = File.ReadAllText(CONFIG_FILE);
                    var config = JsonSerializer.Deserialize<ConfigData>(jsonString);
                    if (config != null && !string.IsNullOrEmpty(config.IpAddress))
                    {
                        ipAddressTextBox.Text = config.IpAddress;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการโหลดการตั้งค่า: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConfig()
        {
            try
            {
                var config = new ConfigData { IpAddress = ipAddressTextBox.Text };
                string jsonString = JsonSerializer.Serialize(config);
                File.WriteAllText(CONFIG_FILE, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการบันทึกการตั้งค่า: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IpAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                // สร้าง Switcher Discovery
                var discovery = new CBMDSwitcherDiscovery();
                IBMDSwitcher? switcherDevice = null;
                _BMDSwitcherConnectToFailure failureReason = 0;

                await Task.Run(() => {
                    discovery.ConnectTo(ipAddressTextBox.Text, out switcherDevice, out failureReason);
                });
                
                if (failureReason == 0 && switcherDevice != null)
                {
                    switcher = switcherDevice;
                    meBlock = switcher as IBMDSwitcherMixEffectBlock;
                    
                    if (meBlock != null)
                    {
                        // สร้างและตั้งค่า callback
                        meCallback = new MixEffectBlockCallback(this);
                        meBlock.AddCallback(meCallback);
                        
                        // โหลด Input List
                        LoadInputList();
                        
                        connectButton.Enabled = false;
                        disconnectButton.Enabled = true;
                        inputComboBox.Enabled = true;
                        setInputButton.Enabled = true;
                        cutButton.Enabled = true;
                        autoButton.Enabled = true;
                        
                        statusLabel.Text = "สถานะ: เชื่อมต่อสำเร็จ";
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถเข้าถึง Mix Effect Block ได้", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"ไม่สามารถเชื่อมต่อได้: {failureReason}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInputList()
        {
            inputComboBox.Items.Clear();
            inputMap.Clear();

            if (switcher != null)
            {
                Guid inputIteratorGuid = typeof(IBMDSwitcherInputIterator).GUID;
                IntPtr iteratorPtr;
                switcher.CreateIterator(ref inputIteratorGuid, out iteratorPtr);
                var iterator = (IBMDSwitcherInputIterator)Marshal.GetObjectForIUnknown(iteratorPtr);

                IBMDSwitcherInput? input;
                while (true)
                {
                    iterator.Next(out input);
                    if (input == null) break;

                    long inputId;
                    input.GetInputId(out inputId);
                    string name;
                    input.GetLongName(out name);
                    inputMap[inputId] = name;
                    inputComboBox.Items.Add($"{inputId} - {name}");
                }

                Marshal.ReleaseComObject(iterator);
            }
        }

        private void SetInputButton_Click(object sender, EventArgs e)
        {
            if (inputComboBox.SelectedIndex >= 0 && inputComboBox.SelectedItem != null)
            {
                string? selectedItem = inputComboBox.SelectedItem.ToString();
                if (selectedItem != null)
                {
                    long inputId = long.Parse(selectedItem.Split('-')[0].Trim());
                    
                    if (meBlock != null)
                    {
                        meBlock.SetPreviewInput(inputId);
                    }
                }
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (switcher != null)
            {
                if (meBlock != null && meCallback != null)
                {
                    meBlock.RemoveCallback(meCallback);
                    meCallback = null;
                }
                
                switcher = null;
                meBlock = null;
                
                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
                inputComboBox.Enabled = false;
                setInputButton.Enabled = false;
                cutButton.Enabled = false;
                autoButton.Enabled = false;
                
                statusLabel.Text = "สถานะ: ไม่ได้เชื่อมต่อ";
            }
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            if (meBlock != null)
            {
                meBlock.PerformCut();
            }
        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            if (meBlock != null)
            {
                meBlock.PerformAutoTransition();
            }
        }

        private void UpdateProgramInput()
        {
            if (meBlock != null)
            {
                long programInput;
                meBlock.GetProgramInput(out programInput);
                programLabel.Text = $"Program: {inputMap.GetValueOrDefault(programInput, "Unknown")}";
            }
        }

        private void UpdatePreviewInput()
        {
            if (meBlock != null)
            {
                long previewInput;
                meBlock.GetPreviewInput(out previewInput);
                previewLabel.Text = $"Preview: {inputMap.GetValueOrDefault(previewInput, "Unknown")}";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (switcher != null)
            {
                if (meBlock != null && meCallback != null)
                {
                    meBlock.RemoveCallback(meCallback);
                }
                switcher = null;
            }
            base.OnFormClosing(e);
        }

        private class ConfigData
        {
            public string IpAddress { get; set; } = string.Empty;
        }
    }
} 