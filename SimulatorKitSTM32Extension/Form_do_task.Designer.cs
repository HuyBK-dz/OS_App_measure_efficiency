namespace SimulatorKitSTM32Extension
{
    partial class Form_do_task
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
            this.components = new System.ComponentModel.Container();
            this.zedGraphControl_do_task = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zedGraphControl_do_task
            // 
            this.zedGraphControl_do_task.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl_do_task.Location = new System.Drawing.Point(2, 1);
            this.zedGraphControl_do_task.Name = "zedGraphControl_do_task";
            this.zedGraphControl_do_task.ScrollGrace = 0D;
            this.zedGraphControl_do_task.ScrollMaxX = 0D;
            this.zedGraphControl_do_task.ScrollMaxY = 0D;
            this.zedGraphControl_do_task.ScrollMaxY2 = 0D;
            this.zedGraphControl_do_task.ScrollMinX = 0D;
            this.zedGraphControl_do_task.ScrollMinY = 0D;
            this.zedGraphControl_do_task.ScrollMinY2 = 0D;
            this.zedGraphControl_do_task.Size = new System.Drawing.Size(796, 447);
            this.zedGraphControl_do_task.TabIndex = 0;
            this.zedGraphControl_do_task.UseExtendedPrintDialog = true;
            // 
            // Form_do_task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.zedGraphControl_do_task);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_do_task";
            this.Text = "Form_do_task";
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zedGraphControl_do_task;
    }
}