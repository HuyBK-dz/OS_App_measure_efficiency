namespace SimulatorKitSTM32Extension
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelContainerPrincipal = new System.Windows.Forms.Panel();
            this.panelContainerForm = new System.Windows.Forms.Panel();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.panelMenuVertical = new System.Windows.Forms.Panel();
            this.btnReportes_TASK = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.label_name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new SimulatorKitSTM32Extension.ButtonImage();
            this.btnMaximize = new SimulatorKitSTM32Extension.ButtonImage();
            this.btnMinimize = new SimulatorKitSTM32Extension.ButtonImage();
            this.label4 = new System.Windows.Forms.Label();
            this.panelContainerPrincipal.SuspendLayout();
            this.panelContainerForm.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.panelMenuVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainerPrincipal
            // 
            this.panelContainerPrincipal.BackColor = System.Drawing.Color.White;
            this.panelContainerPrincipal.Controls.Add(this.panelContainerForm);
            this.panelContainerPrincipal.Controls.Add(this.panelToolbar);
            this.panelContainerPrincipal.Controls.Add(this.panelMenuVertical);
            this.panelContainerPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainerPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelContainerPrincipal.Name = "panelContainerPrincipal";
            this.panelContainerPrincipal.Size = new System.Drawing.Size(776, 448);
            this.panelContainerPrincipal.TabIndex = 2;
            // 
            // panelContainerForm
            // 
            this.panelContainerForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContainerForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panelContainerForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelContainerForm.Controls.Add(this.label3);
            this.panelContainerForm.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelContainerForm.Location = new System.Drawing.Point(151, 45);
            this.panelContainerForm.Name = "panelContainerForm";
            this.panelContainerForm.Size = new System.Drawing.Size(625, 403);
            this.panelContainerForm.TabIndex = 3;
            // 
            // panelToolbar
            // 
            this.panelToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panelToolbar.Controls.Add(this.btnClose);
            this.panelToolbar.Controls.Add(this.btnMaximize);
            this.panelToolbar.Controls.Add(this.btnMinimize);
            this.panelToolbar.Location = new System.Drawing.Point(151, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(625, 45);
            this.panelToolbar.TabIndex = 2;
            this.panelToolbar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelToolbar_Paint);
            this.panelToolbar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelToolbar_MouseDoubleClick);
            this.panelToolbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelToolbar_MouseDown);
            // 
            // panelMenuVertical
            // 
            this.panelMenuVertical.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMenuVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panelMenuVertical.Controls.Add(this.label4);
            this.panelMenuVertical.Controls.Add(this.label2);
            this.panelMenuVertical.Controls.Add(this.label1);
            this.panelMenuVertical.Controls.Add(this.label_name);
            this.panelMenuVertical.Controls.Add(this.btnReportes_TASK);
            this.panelMenuVertical.Controls.Add(this.btnReportes);
            this.panelMenuVertical.Controls.Add(this.btnSetting);
            this.panelMenuVertical.Location = new System.Drawing.Point(0, 0);
            this.panelMenuVertical.Name = "panelMenuVertical";
            this.panelMenuVertical.Size = new System.Drawing.Size(151, 448);
            this.panelMenuVertical.TabIndex = 0;
            // 
            // btnReportes_TASK
            // 
            this.btnReportes_TASK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes_TASK.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes_TASK.ForeColor = System.Drawing.Color.White;
            this.btnReportes_TASK.Location = new System.Drawing.Point(0, 237);
            this.btnReportes_TASK.Name = "btnReportes_TASK";
            this.btnReportes_TASK.Size = new System.Drawing.Size(151, 64);
            this.btnReportes_TASK.TabIndex = 3;
            this.btnReportes_TASK.Text = "ĐO TASK";
            this.btnReportes_TASK.UseVisualStyleBackColor = true;
            this.btnReportes_TASK.Click += new System.EventHandler(this.btnReportes_TASK_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(0, 177);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(151, 39);
            this.btnReportes.TabIndex = 3;
            this.btnReportes.Text = "ĐO RAM";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.FlatAppearance.BorderSize = 0;
            this.btnSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetting.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetting.ForeColor = System.Drawing.Color.White;
            this.btnSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSetting.Location = new System.Drawing.Point(0, 116);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(151, 39);
            this.btnSetting.TabIndex = 2;
            this.btnSetting.Text = "Cài Đặt";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_name.ForeColor = System.Drawing.Color.White;
            this.label_name.Location = new System.Drawing.Point(3, 19);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(132, 19);
            this.label_name.TabIndex = 4;
            this.label_name.Text = "Một sản phẩm của";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Đỗ Quang Huy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "MSSV: 20192906";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label3.Location = new System.Drawing.Point(161, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(368, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cần cài đặt trước khi đo hiệu năng";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageHover = null;
            this.btnClose.ImageNormal = null;
            this.btnClose.Location = new System.Drawing.Point(594, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Image = ((System.Drawing.Image)(resources.GetObject("btnMaximize.Image")));
            this.btnMaximize.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnMaximize.ImageHover")));
            this.btnMaximize.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnMaximize.ImageNormal")));
            this.btnMaximize.Location = new System.Drawing.Point(551, 12);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new System.Drawing.Size(20, 20);
            this.btnMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMaximize.TabIndex = 0;
            this.btnMaximize.TabStop = false;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Image = ((System.Drawing.Image)(resources.GetObject("btnMinimize.Image")));
            this.btnMinimize.ImageHover = null;
            this.btnMinimize.ImageNormal = null;
            this.btnMinimize.Location = new System.Drawing.Point(509, 12);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(20, 20);
            this.btnMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMinimize.TabIndex = 0;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label4.Location = new System.Drawing.Point(3, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Viện ĐTVT";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(776, 448);
            this.Controls.Add(this.panelContainerPrincipal);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(776, 448);
            this.Name = "FormMain";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelContainerPrincipal.ResumeLayout(false);
            this.panelContainerForm.ResumeLayout(false);
            this.panelContainerForm.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.panelMenuVertical.ResumeLayout(false);
            this.panelMenuVertical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelContainerPrincipal;
        private System.Windows.Forms.Panel panelMenuVertical;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Panel panelContainerForm;
        private System.Windows.Forms.Panel panelToolbar;
        private ButtonImage btnClose;
        private ButtonImage btnMaximize;
        private ButtonImage btnMinimize;
        private System.Windows.Forms.Button btnReportes_TASK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

