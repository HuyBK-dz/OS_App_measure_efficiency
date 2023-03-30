namespace SimulatorKitSTM32Extension
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.Serial = new System.Windows.Forms.GroupBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.cb_ComPort = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.cb_Stopbit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_Parity = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_BaudRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_DataSize = new System.Windows.Forms.ComboBox();
            this.label45 = new System.Windows.Forms.Label();
            this.Serial.SuspendLayout();
            this.SuspendLayout();
            // 
            // Serial
            // 
            this.Serial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Serial.Controls.Add(this.btn_Connect);
            this.Serial.Controls.Add(this.cb_ComPort);
            this.Serial.Controls.Add(this.label44);
            this.Serial.Controls.Add(this.cb_Stopbit);
            this.Serial.Controls.Add(this.label2);
            this.Serial.Controls.Add(this.cb_Parity);
            this.Serial.Controls.Add(this.label1);
            this.Serial.Controls.Add(this.cb_BaudRate);
            this.Serial.Controls.Add(this.label3);
            this.Serial.Controls.Add(this.cb_DataSize);
            this.Serial.Controls.Add(this.label45);
            this.Serial.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Serial.ForeColor = System.Drawing.Color.Blue;
            this.Serial.Location = new System.Drawing.Point(12, 9);
            this.Serial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Serial.Name = "Serial";
            this.Serial.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Serial.Size = new System.Drawing.Size(822, 385);
            this.Serial.TabIndex = 23;
            this.Serial.TabStop = false;
            this.Serial.Text = "Serial";
            // 
            // btn_Connect
            // 
            this.btn_Connect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Connect.ForeColor = System.Drawing.Color.Black;
            this.btn_Connect.Location = new System.Drawing.Point(665, 307);
            this.btn_Connect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(110, 48);
            this.btn_Connect.TabIndex = 18;
            this.btn_Connect.Text = "CONNECT";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // cb_ComPort
            // 
            this.cb_ComPort.BackColor = System.Drawing.Color.White;
            this.cb_ComPort.ForeColor = System.Drawing.Color.Black;
            this.cb_ComPort.FormattingEnabled = true;
            this.cb_ComPort.Location = new System.Drawing.Point(148, 50);
            this.cb_ComPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_ComPort.Name = "cb_ComPort";
            this.cb_ComPort.Size = new System.Drawing.Size(105, 25);
            this.cb_ComPort.TabIndex = 14;
            this.cb_ComPort.Click += new System.EventHandler(this.cb_ComPort_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.Blue;
            this.label44.Location = new System.Drawing.Point(55, 53);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(71, 17);
            this.label44.TabIndex = 15;
            this.label44.Text = "Com Port";
            // 
            // cb_Stopbit
            // 
            this.cb_Stopbit.ForeColor = System.Drawing.Color.Black;
            this.cb_Stopbit.FormattingEnabled = true;
            this.cb_Stopbit.Location = new System.Drawing.Point(148, 252);
            this.cb_Stopbit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Stopbit.Name = "cb_Stopbit";
            this.cb_Stopbit.Size = new System.Drawing.Size(105, 25);
            this.cb_Stopbit.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(57, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Stop bit";
            // 
            // cb_Parity
            // 
            this.cb_Parity.ForeColor = System.Drawing.Color.Black;
            this.cb_Parity.FormattingEnabled = true;
            this.cb_Parity.Location = new System.Drawing.Point(148, 203);
            this.cb_Parity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_Parity.Name = "cb_Parity";
            this.cb_Parity.Size = new System.Drawing.Size(105, 25);
            this.cb_Parity.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(57, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "Parity";
            // 
            // cb_BaudRate
            // 
            this.cb_BaudRate.ForeColor = System.Drawing.Color.Black;
            this.cb_BaudRate.FormattingEnabled = true;
            this.cb_BaudRate.Location = new System.Drawing.Point(148, 103);
            this.cb_BaudRate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_BaudRate.Name = "cb_BaudRate";
            this.cb_BaudRate.Size = new System.Drawing.Size(105, 25);
            this.cb_BaudRate.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(55, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Baud Rate";
            // 
            // cb_DataSize
            // 
            this.cb_DataSize.ForeColor = System.Drawing.Color.Black;
            this.cb_DataSize.FormattingEnabled = true;
            this.cb_DataSize.Location = new System.Drawing.Point(148, 153);
            this.cb_DataSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cb_DataSize.Name = "cb_DataSize";
            this.cb_DataSize.Size = new System.Drawing.Size(105, 25);
            this.cb_DataSize.TabIndex = 16;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Blue;
            this.label45.Location = new System.Drawing.Point(55, 156);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(68, 17);
            this.label45.TabIndex = 17;
            this.label45.Text = "Data Size";
            // 
            // FormSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(846, 407);
            this.Controls.Add(this.Serial);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSetting";
            this.Text = "Setting";
            this.Serial.ResumeLayout(false);
            this.Serial.PerformLayout();
            this.ResumeLayout(false);

        }




        #endregion

        private System.Windows.Forms.GroupBox Serial;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.ComboBox cb_ComPort;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.ComboBox cb_Stopbit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_Parity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_BaudRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_DataSize;
        private System.Windows.Forms.Label label45;
    }
}