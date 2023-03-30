using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorKitSTM32Extension
{
    public partial class FormSetting : Form
    {
        UartProcess uartProcess_setting = new UartProcess();

        public UartProcess.ReceivedResults rcvResult
        {
            set { uartProcess_setting.receivedResult += value; }
        }

        public void SerialBuzzerSet(byte buzzer_level)
        {
            uartProcess_setting.UartSend_CmdBuzzerSet(buzzer_level);
        }

        public void SerialButtonSet(byte button_id, byte button_state)
        {
            uartProcess_setting.UartSend_CmdButtonSet(button_id, button_state);
        }

        public void SerialLedSet(byte led_id, byte led_color, byte led_num_blink, byte led_interval, byte led_last_state)
        {
            uartProcess_setting.UartSend_CmdLedSet(led_id, led_color, led_num_blink, led_interval, led_last_state);
        }

        public void SerialLcdSet(string text)
        {
            uartProcess_setting.UartSend_CmdLcdSet(text);
        }

        public void SerialInformationGet()
        {
            uartProcess_setting.UartSend_cmdInformationGet();
        }

        public FormSetting(String ComPort)
        {
            InitializeComponent();

            // Cài đặt cho BaudRate
            string[] BaudRate = { "4800", "9600", "19200", "38400", "56000", "57600", "115200" };
            string[] DataBit = { "7", "8" };
            string[] Parity = { "None", "Even", "Old", "Mark" };
            string[] Stopbit = { "One", "OnePointFive", "Two" };

            cb_BaudRate.Items.AddRange(BaudRate);
            cb_DataSize.Items.AddRange(DataBit);
            cb_Parity.Items.AddRange(Parity);
            cb_Stopbit.Items.AddRange(Stopbit);
            cb_ComPort.Items.Add(ComPort);

            cb_BaudRate.SelectedIndex = 6;
            cb_DataSize.SelectedIndex = 1;
            cb_Parity.SelectedIndex = 0;
            cb_Stopbit.SelectedIndex = 0;
            cb_ComPort.SelectedIndex = 0;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_Connect.Text == "CONNECT")
                {                 
                    Properties.Settings.Default.PortName = cb_ComPort.SelectedItem.ToString();
                    Properties.Settings.Default.BaudRate = cb_BaudRate.SelectedItem.ToString();
                    Properties.Settings.Default.Datasize = cb_DataSize.SelectedItem.ToString();
                    Properties.Settings.Default.Stopbit = cb_Stopbit.SelectedItem.ToString();
                    Properties.Settings.Default.Parity = cb_Parity.SelectedItem.ToString();
                    Properties.Settings.Default.Save();
                    btn_Connect.Text = "DISCONNECT";
                    uartProcess_setting.Open();

                }
                else
                {
                    SimulatorKitSTM32Extension.Properties.Settings.Default.PortName = "";
                    SimulatorKitSTM32Extension.Properties.Settings.Default.Save();
                    btn_Connect.Text = "CONNECT";
                    uartProcess_setting.Close();
                }
            }
            catch
            {
                MessageBox.Show(cb_ComPort.SelectedItem.ToString() + " không thể kết nối!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_ComPort_Click(object sender, EventArgs e)
        {
            cb_ComPort.Items.Clear();
            cb_ComPort.Items.AddRange(SerialPort.GetPortNames());
        }
    }
}
