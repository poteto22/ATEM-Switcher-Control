using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace WinformApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.programLabel = new System.Windows.Forms.Label();
            this.previewLabel = new System.Windows.Forms.Label();
            this.inputComboBox = new System.Windows.Forms.ComboBox();
            this.setInputButton = new System.Windows.Forms.Button();
            this.cutButton = new System.Windows.Forms.Button();
            this.autoButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(12, 12);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(150, 23);
            this.ipAddressTextBox.TabIndex = 0;
            this.ipAddressTextBox.Text = "192.168.1.1";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(168, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "เชื่อมต่อ";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(249, 12);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 2;
            this.disconnectButton.Text = "ยกเลิก";
            this.disconnectButton.UseVisualStyleBackColor = true;
            // 
            // programLabel
            // 
            this.programLabel.AutoSize = true;
            this.programLabel.Location = new System.Drawing.Point(12, 47);
            this.programLabel.Name = "programLabel";
            this.programLabel.Size = new System.Drawing.Size(100, 15);
            this.programLabel.TabIndex = 3;
            this.programLabel.Text = "Program: 1 - Camera 1";
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Location = new System.Drawing.Point(12, 72);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(100, 15);
            this.previewLabel.TabIndex = 4;
            this.previewLabel.Text = "Preview: 2 - Camera 2";
            // 
            // inputComboBox
            // 
            this.inputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputComboBox.Enabled = false;
            this.inputComboBox.FormattingEnabled = true;
            this.inputComboBox.Location = new System.Drawing.Point(12, 100);
            this.inputComboBox.Name = "inputComboBox";
            this.inputComboBox.Size = new System.Drawing.Size(150, 23);
            this.inputComboBox.TabIndex = 5;
            // 
            // setInputButton
            // 
            this.setInputButton.Enabled = false;
            this.setInputButton.Location = new System.Drawing.Point(168, 100);
            this.setInputButton.Name = "setInputButton";
            this.setInputButton.Size = new System.Drawing.Size(75, 23);
            this.setInputButton.TabIndex = 6;
            this.setInputButton.Text = "เลือก Input";
            this.setInputButton.UseVisualStyleBackColor = true;
            // 
            // cutButton
            // 
            this.cutButton.Enabled = false;
            this.cutButton.Location = new System.Drawing.Point(12, 140);
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(75, 23);
            this.cutButton.TabIndex = 7;
            this.cutButton.Text = "Cut";
            this.cutButton.UseVisualStyleBackColor = true;
            // 
            // autoButton
            // 
            this.autoButton.Enabled = false;
            this.autoButton.Location = new System.Drawing.Point(93, 140);
            this.autoButton.Name = "autoButton";
            this.autoButton.Size = new System.Drawing.Size(75, 23);
            this.autoButton.TabIndex = 8;
            this.autoButton.Text = "Auto";
            this.autoButton.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 180);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(100, 15);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "สถานะ: ไม่ได้เชื่อมต่อ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 211);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.autoButton);
            this.Controls.Add(this.cutButton);
            this.Controls.Add(this.setInputButton);
            this.Controls.Add(this.inputComboBox);
            this.Controls.Add(this.previewLabel);
            this.Controls.Add(this.programLabel);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.ipAddressTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATEM Switcher Control";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label programLabel;
        private System.Windows.Forms.Label previewLabel;
        private System.Windows.Forms.ComboBox inputComboBox;
        private System.Windows.Forms.Button setInputButton;
        private System.Windows.Forms.Button cutButton;
        private System.Windows.Forms.Button autoButton;
        private System.Windows.Forms.Label statusLabel;
    }
} 