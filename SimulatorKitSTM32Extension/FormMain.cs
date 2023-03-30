using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SimulatorKitSTM32Extension
{
    public partial class FormMain : Form
    {
        private bool maximize = false;
        private int lx, ly;
        private int sw, sh;

        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;
        FormSetting frmSetting;
        FormReportes frmReportes;
        Form_do_task form_Do_Task;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContainerPrincipal.Region = region;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        /// <summary>j
        /// Khởi tạo các thông số liên quan
        /// </summary>
        void Init()
        {
            frmSetting = new FormSetting(Properties.Settings.Default.PortName);
            frmReportes = new FormReportes();
           
            form_Do_Task = new Form_do_task();
            frmSetting.rcvResult = ReceivedResultToolTest;
        }

        public FormMain()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;

            Init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //picBoxLogo_Click(null, e);
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            
            if (maximize == false)
            {
                lx = this.Location.X;
                ly = this.Location.Y;
                sw = this.Size.Width;
                sh = this.Size.Height;
                btnMaximize.Image = btnMaximize.ImageHover;
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
                this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            }
            else
            {
                this.Size = new Size(sw, sh);
                this.Location = new Point(lx, ly);
                btnMaximize.Image = btnMaximize.ImageNormal;
            }

            maximize = !maximize;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelToolbar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void DisplayFormInPanel(object formsetting)
        {
            if (this.panelContainerForm.Controls.Count > 0)
                this.panelContainerForm.Controls.RemoveAt(0);

            Form fh = formsetting as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelContainerForm.Controls.Add(fh);
            this.panelContainerForm.Tag = fh;
            fh.Show();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            DisplayFormInPanel(frmSetting);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            DisplayFormInPanel(frmReportes);
        }

        private void panelToolbar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMaximize_Click(null, e);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            //DisplayFormInPanel(frmInformation);
        }

        private void panelToolbar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReportes_TASK_Click(object sender, EventArgs e)
        {
            DisplayFormInPanel(form_Do_Task);
        }

        /// <summary>
        /// Callback trả kết quả về từ ToolTest
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="defInfo"></param>
        delegate void Delegate_ReceivedResultToolTest(Constants.CMD_ID cmdId, Constants.Service srv);
        private void ReceivedResultToolTest(Constants.CMD_ID cmdId, Constants.Service srv)
        {
            this.BeginInvoke(new Action(() =>
            {
                switch (cmdId)
                {
                    case Constants.CMD_ID.CMD_ID_INFOR:

                        break;

                    case Constants.CMD_ID.CMD_ID_BUTTON:

                        break;

                    case Constants.CMD_ID.CMD_ID_BUZZER:
                        if (srv.cmd_buzzer_state.state == 1)
                        {
                        }
                        break;

                    case Constants.CMD_ID.CMD_ID_LED:
                        break;

                    case Constants.CMD_ID.CMD_ID_LCD:
                        break;

                    case Constants.CMD_ID.CMD_ID_LIGHT_SENSOR:
                        break;

                    case Constants.CMD_ID.CMD_ID_TEMP_SENSOR:
                        break;

                    case Constants.CMD_ID.CMD_ID_HUM_SENSOR:
                        break;

                    case Constants.CMD_ID.CMD_ID_TRACKING_PERFORMANCE:
                        frmReportes.set_totalCount(srv.cmd_tracking_performance.totalcount);
                        frmReportes.set_ramused(srv.cmd_tracking_performance.ramused);
                        frmReportes.set_ticktime(srv.cmd_tracking_performance.ticktime);

                        form_Do_Task.set_totalCount(srv.cmd_tracking_performance.totalcount);
                        form_Do_Task.set_ramused(srv.cmd_tracking_performance.ramused);
                        form_Do_Task.set_ticktime(srv.cmd_tracking_performance.ticktime);

                        /*Vẽ đồ thị đo ram */
                        frmReportes.DrawT1(srv.cmd_tracking_performance.ticktime.ToString(), srv.cmd_tracking_performance.ramused.ToString());
                        /*vẽ đồ thị đo task*/
                        form_Do_Task.DrawT2(srv.cmd_tracking_performance.ticktime.ToString(), srv.cmd_tracking_performance.totalcount.ToString());
                        break;
                }
            }));
        }
    }
}
