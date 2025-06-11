using System;
using System.Windows.Forms;
using BMDSwitcherAPI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace WinformApp
{
    public partial class Form1 : Form
    {
        private IBMDSwitcher switcher;
        private IBMDSwitcherMixEffectBlock meBlock;
        private TextBox ipAddressTextBox;
        private Button connectButton;
        private Button disconnectButton;
        private Button cutButton;
        private Button autoButton;
        private Label statusLabel;
        private ComboBox inputComboBox;
        private Button setInputButton;
        private Dictionary<long, string> inputMap;
        private Label programLabel;
        private Label previewLabel;
        private IBMDSwitcherMixEffectBlockCallback meCallback;
        private const string CONFIG_FILE = "atem_config.json";

        public Form1()
        {
            InitializeComponent();
            this.Text = "ATEM Switcher Control";
            inputMap = new Dictionary<long, string>();
            InitializeATEMControls();
            LoadConfig();
        }

        private void InitializeATEMControls()
        {
            // IP Address TextBox
            ipAddressTextBox = new TextBox
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(150, 20),
                Text = "192.168.1.1"
            };
            ipAddressTextBox.TextChanged += IpAddressTextBox_TextChanged;
            this.Controls.Add(ipAddressTextBox);

            // Connect Button
            connectButton = new Button
            {
                Location = new System.Drawing.Point(180, 20),
                Size = new System.Drawing.Size(100, 23),
                Text = "เชื่อมต่อ"
            };
            connectButton.Click += ConnectButton_Click;
            this.Controls.Add(connectButton);

            // Disconnect Button
            disconnectButton = new Button
            {
                Location = new System.Drawing.Point(290, 20),
                Size = new System.Drawing.Size(100, 23),
                Text = "ยกเลิกการเชื่อมต่อ",
                Enabled = false
            };
            disconnectButton.Click += DisconnectButton_Click;
            this.Controls.Add(disconnectButton);

            // Program Label
            programLabel = new Label
            {
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(360, 20),
                Text = "Program: ไม่มีข้อมูล",
                Font = new System.Drawing.Font(programLabel.Font, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(programLabel);

            // Preview Label
            previewLabel = new Label
            {
                Location = new System.Drawing.Point(20, 85),
                Size = new System.Drawing.Size(360, 20),
                Text = "Preview: ไม่มีข้อมูล"
            };
            this.Controls.Add(previewLabel);

            // Input ComboBox
            inputComboBox = new ComboBox
            {
                Location = new System.Drawing.Point(20, 115),
                Size = new System.Drawing.Size(200, 23),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Enabled = false
            };
            this.Controls.Add(inputComboBox);

            // Set Input Button
            setInputButton = new Button
            {
                Location = new System.Drawing.Point(230, 115),
                Size = new System.Drawing.Size(100, 23),
                Text = "เลือก Input",
                Enabled = false
            };
            setInputButton.Click += SetInputButton_Click;
            this.Controls.Add(setInputButton);

            // Cut Button
            cutButton = new Button
            {
                Location = new System.Drawing.Point(20, 150),
                Size = new System.Drawing.Size(100, 23),
                Text = "Cut",
                Enabled = false
            };
            cutButton.Click += CutButton_Click;
            this.Controls.Add(cutButton);

            // Auto Button
            autoButton = new Button
            {
                Location = new System.Drawing.Point(130, 150),
                Size = new System.Drawing.Size(100, 23),
                Text = "Auto",
                Enabled = false
            };
            autoButton.Click += AutoButton_Click;
            this.Controls.Add(autoButton);

            // Status Label
            statusLabel = new Label
            {
                Location = new System.Drawing.Point(20, 190),
                Size = new System.Drawing.Size(360, 20),
                Text = "สถานะ: ไม่ได้เชื่อมต่อ"
            };
            this.Controls.Add(statusLabel);
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
                var switcherConnect = await Task.Run(() => discovery.ConnectTo(ipAddressTextBox.Text));
                
                if (switcherConnect != null)
                {
                    switcher = switcherConnect;
                    meBlock = switcher as IBMDSwitcherMixEffectBlock;
                    
                    // โหลด Input List
                    LoadInputList();
                    
                    connectButton.Enabled = false;
                    disconnectButton.Enabled = true;
                    cutButton.Enabled = true;
                    autoButton.Enabled = true;
                    inputComboBox.Enabled = true;
                    setInputButton.Enabled = true;
                    statusLabel.Text = "สถานะ: เชื่อมต่อสำเร็จ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการเชื่อมต่อ: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInputList()
        {
            try
            {
                inputComboBox.Items.Clear();
                inputMap.Clear();

                // ดึง Input Iterator
                IBMDSwitcherInputIterator inputIterator = switcher as IBMDSwitcherInputIterator;
                if (inputIterator != null)
                {
                    IBMDSwitcherInput input;
                    while (inputIterator.Next(out input) == 0)
                    {
                        long inputId;
                        input.GetInputId(out inputId);

                        string inputName;
                        input.GetLongName(out inputName);

                        inputMap[inputId] = inputName;
                        inputComboBox.Items.Add($"{inputId}: {inputName}");

                        Marshal.ReleaseComObject(input);
                    }
                }

                if (inputComboBox.Items.Count > 0)
                {
                    inputComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการโหลด Input List: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetInputButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (meBlock != null && inputComboBox.SelectedItem != null)
                {
                    string selectedInput = inputComboBox.SelectedItem.ToString();
                    long inputId = long.Parse(selectedInput.Split(':')[0]);

                    // ตั้งค่า Input เป็น Program
                    meBlock.SetProgramInput(inputId);
                    statusLabel.Text = $"สถานะ: เปลี่ยน Input เป็น {selectedInput}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการเปลี่ยน Input: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (switcher != null)
            {
                Marshal.ReleaseComObject(switcher);
                switcher = null;
                meBlock = null;

                connectButton.Enabled = true;
                disconnectButton.Enabled = false;
                cutButton.Enabled = false;
                autoButton.Enabled = false;
                inputComboBox.Enabled = false;
                setInputButton.Enabled = false;
                statusLabel.Text = "สถานะ: ไม่ได้เชื่อมต่อ";
            }
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (meBlock != null)
                {
                    meBlock.PerformCut();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการ Cut: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (meBlock != null)
                {
                    meBlock.PerformAutoTransition();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการ Auto: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (switcher != null)
            {
                Marshal.ReleaseComObject(switcher);
            }
        }

        private class ConfigData
        {
            public string IpAddress { get; set; }
        }
    }
} 