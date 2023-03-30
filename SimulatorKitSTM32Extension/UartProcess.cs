using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;

namespace SimulatorKitSTM32Extension
{
    public class UartProcess
    {
        struct CmdRecvFifo_t
        {
            public string Time;
            public byte[] UartData;
            public byte PacketLength;
            public byte CheckXor;
        };
        static Queue<CmdRecvFifo_t> fifoCmdReceiveUart = new Queue<CmdRecvFifo_t>();
        private static System.Timers.Timer processTimer;

        #region events
        public delegate void ReceivedResults(Constants.CMD_ID cmdId, Constants.Service service);
        public event ReceivedResults receivedResult;

        public delegate void ReceivedCheckToolInfo(int version);
        public event ReceivedCheckToolInfo receivedCheckToolInfo;

        public delegate void ErrorReport(string errorContent);
        public event ErrorReport errorReport;
        #endregion

        public byte frm_sequence = 0;

        public string ComName = "";
        SerialPort P; // Khai báo 1 Object SerialPort mới.

        private static System.Timers.Timer ComCheckTimer;
        bool DisplayDisconnect = true;
        private void ComTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!P.IsOpen)
                {
                    couterReconnect++;
                    if (couterReconnect >= 100)
                    {
                        couterReconnect = 0;
                        if (DisplayDisconnect) { 
                            DisplayDisconnect = false;
                            P.DiscardInBuffer();
                            P.DiscardOutBuffer();
                            MessageBox.Show("Mất kết nối cổng COM\r\nXin vui lòng kiểm tra lại", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (errorReport != null) errorReport("Disconnected");
                        }
                        return;
                    }
                    if (couterReconnect == 1 || couterReconnect % 10 == 0)
                    {
                        P.PortName = Properties.Settings.Default.PortName;
                        P.BaudRate = int.Parse(Properties.Settings.Default.BaudRate);
                        P.DataBits = int.Parse(Properties.Settings.Default.Datasize);
                        P.StopBits = UartProcess_checkStopbit(Properties.Settings.Default.Stopbit);
                        P.Parity = UartProcess_checkParity(Properties.Settings.Default.Parity);
                        P.Open();
                        
                        SetTimer(10);
                        if (errorReport != null) errorReport("Connected");
                    }
                }
                else
                {
                    DisplayDisconnect = true;
                }
            }
            catch
            {
                //couterReconnect = 0;
            }
        }

        private static System.Timers.Timer timerTimeOut;
        private void SetTimerTimeOut(int ms)
        {
            // Create a timer with a two second interval.
            if (ms <= 0) ms = 1;
            if (timerTimeOut != null) timerTimeOut.Stop();
            timerTimeOut = new System.Timers.Timer(ms);
            // Hook up the Elapsed event for the timer. 
            timerTimeOut.Elapsed += OnTimerTimeOutEvent;
            timerTimeOut.AutoReset = false;
            timerTimeOut.Enabled = true;
        }
        private void StopTimerTimeOut()
        {
            if (timerTimeOut != null) timerTimeOut.Stop();
        }
        private void OnTimerTimeOutEvent(Object source, ElapsedEventArgs e)
        {
            
        }

        public enum eErrorCode
        {
            ERR_CHECK_XOR = 0x01,
            ERR_CMD_NOT_SUPPORT = 0x02,
            ERR_PARAMETER_INVALID = 0x03,
            ERR_RESEND_REQ = 0x04
        }

        public UartProcess()
        {
            P = new SerialPort();
            P.ReadTimeout = 1000;
            P.DataBits = 8;
            P.Parity = Parity.None;
            P.StopBits = StopBits.One;
            P.DataReceived += SerialDataReceivedEventFunction;
            SetTimer(1);
        }

        private Parity UartProcess_checkParity(string parity)
        {
            Parity parity_result;

            switch (parity)
            {
                case "Even":
                    parity_result = Parity.Even;
                    break;

                case "Mark":
                    parity_result = Parity.Mark;
                    break;

                case "None":
                    parity_result = Parity.None;
                    break;

                case "Old":
                    parity_result = Parity.Odd;
                    break;

                default:
                    parity_result = Parity.None;
                    break;
            }

            return parity_result;
        }

        public StopBits UartProcess_checkStopbit(string stopbit)
        {
            StopBits stopbit_result;

            switch (stopbit)
            {
                case "One":
                    stopbit_result = StopBits.One;
                    break;

                case "OnePointFive":
                    stopbit_result = StopBits.OnePointFive;
                    break;

                case "Two":
                    stopbit_result = StopBits.Two;
                    break;

                default:
                    stopbit_result = StopBits.One;
                    break;
            }

            return stopbit_result;
        }

        public void Open()
        {
            SetComTimer(10);
            try
            {
                if (Properties.Settings.Default.PortName == "")
                {
                    if (errorReport != null) errorReport("Bạn chưa chọn cổng COM!");
                }
                P.PortName = Properties.Settings.Default.PortName;
                P.BaudRate = int.Parse(Properties.Settings.Default.BaudRate);
                P.DataBits = int.Parse(Properties.Settings.Default.Datasize);
                P.StopBits = UartProcess_checkStopbit(Properties.Settings.Default.Stopbit);
                P.Parity = UartProcess_checkParity(Properties.Settings.Default.Parity);
                P.Open();
                SetTimer(1);
                P.DiscardInBuffer();
                P.DiscardOutBuffer();
                if (errorReport != null) errorReport("Connected");
            }
            catch
            {
                if (Properties.Settings.Default.PortName != "" && errorReport != null)
                {
                    errorReport("Không thể kết nối đến " + Properties.Settings.Default.PortName);
                    errorReport("Disconnected");
                }
            }
        }

        public void Close()
        {
            ComCheckTimer.Stop();
            P.Close();
        }
        int UartRxCurrentLength = 0;
        int UartRxPacketLength = 0;
        int UartRxDataStep = 0;
        static bool IsRunningUartRecvProcess = false;
        byte[] UartRxCommandData = new byte[100];
        
        /**
         * @func   SerialDataReceivedEventFunction
         * @brief  Event nhận dữ liệu từ UART
         * @param  None
         * @retval None
         */
        byte PacketCheckXor = 0;
        private void SerialDataReceivedEventFunction(object obj, SerialDataReceivedEventArgs e)
        {
            int NumberOfByteReCeiver;
            byte ReadSerialData;
            //System.Threading.Thread.Sleep(1000);
            NumberOfByteReCeiver = P.BytesToRead;
            byte[] buf = new byte[NumberOfByteReCeiver];
            
            P.Read(buf, 0, NumberOfByteReCeiver);

            for (int i = 0; i < NumberOfByteReCeiver; i++)
            {
                ReadSerialData = buf[i];
                switch (UartRxDataStep)
                {
                    case 0:
                        if (ReadSerialData == 0xB1)
                        {
                            UartRxDataStep = 1;
                            UartRxCurrentLength = 0;
                            PacketCheckXor = 0xFF;
                        }
                        else
                        {
                            UartRxDataStep = 0;
                        }
                        break;

                    case 1:
                        UartRxPacketLength = ReadSerialData;
                        if (UartRxPacketLength > 100)
                        {
                            UartRxDataStep = 0;
                        }
                        else
                        {
                            UartRxDataStep = 2;
                        }
                        break;

                    case 2:
                        if (UartRxCurrentLength < UartRxPacketLength - 1)
                        {
                            UartRxCommandData[UartRxCurrentLength] = ReadSerialData;
                            
                            if (UartRxCurrentLength > 0)
                            {
                                /* Calculate check xor */
                                PacketCheckXor ^= ReadSerialData;
                            }

                            if (++UartRxCurrentLength == UartRxPacketLength - 1)
                            {
                                UartRxDataStep = 3;
                            }
                        }
                        else
                        {
                            UartRxDataStep = 0;
                        }
                        break;
                    case 3:
                        if (PacketCheckXor == ReadSerialData)
                        {
                            CmdRecvFifo_t cmdFifo = new CmdRecvFifo_t() { Time = "", UartData = new byte[UartRxCurrentLength] };
                            cmdFifo.Time = DateTime.Now.ToString("hh:mm:ss.fff ");
                            cmdFifo.PacketLength = (byte)UartRxPacketLength;
                            cmdFifo.CheckXor = (byte)PacketCheckXor;
                            Commons.memcopy(UartRxCommandData, cmdFifo.UartData, 0, (byte)(UartRxCurrentLength));
                            fifoCmdReceiveUart.Enqueue(cmdFifo);
                        }

                        UartRxCurrentLength = 0;
                        UartRxDataStep = 0;
                        break;
                    default:
                        break;
                }

            }
            //if (NumberOfByteReCeiver != 0) formMain.SetText(FormMain.statusTransit.RECIEVE, buf);   //Display message
        }
        
        /**
         * @func   UartSendUartErr
         * @brief  Gửi bản tin Uart có sự cố ( 0x80 ) từ Host -> Coor
         * @param  UartPacketCommand : Mảng payload của bản tin nhân được
         * @retval None
         */
        private void UartRecvProcess(byte[] UartPacketCommand)
        {
            Constants.Service srv = new Constants.Service();

            try
            {
                int startIndex = 1;
                byte UartPacketID = UartPacketCommand[startIndex++];
                byte UartPacketType = UartPacketCommand[startIndex++];

                StopTimerTimeOut(); //Nhận lệnh bất kỳ coi như đã gửi thành công

                switch (UartPacketID)
                {
                    case (int)Constants.CMD_ID.CMD_ID_INFOR:
                        Log.LOG_INFO("======== [RECV] Get Tool Information");
                        if (receivedCheckToolInfo != null) receivedCheckToolInfo(UartPacketCommand[startIndex] + (UartPacketCommand[startIndex + 1] << 8));
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_BUTTON:
                        srv.cmd_button_state.epoint = UartPacketCommand[startIndex++];
                        srv.cmd_button_state.state = UartPacketCommand[startIndex++];
                        Log.LOG_INFO("======== [RECV] Command button : button_id = " + srv.cmd_button_state.epoint.ToString());
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_BUTTON, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_BUZZER:
                        srv.cmd_buzzer_state.state = UartPacketCommand[startIndex++];
                        Log.LOG_INFO("======== [RECV] Command buzzer : " + "\tbuzzer_state = " + srv.cmd_buzzer_state.state.ToString());
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_BUZZER, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_LED:
                        srv.cmd_led_indicator.numID = UartPacketCommand[startIndex++];
                        srv.cmd_led_indicator.color = UartPacketCommand[startIndex++];
                        srv.cmd_led_indicator.level = UartPacketCommand[startIndex++];
                        //srv.cmd_led_indicator.interval = UartPacketCommand[startIndex++];
                        //srv.cmd_led_indicator.counter = UartPacketCommand[startIndex++];
                        //srv.cmd_led_indicator.laststate = UartPacketCommand[startIndex++];
                        Log.LOG_INFO("======== [RECV] Command led " + srv.cmd_led_indicator.numID.ToString());
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_LED, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_HUM_SENSOR:
                        srv.cmd_humi_get.value = (short)((UartPacketCommand[startIndex] << 8) + UartPacketCommand[startIndex + 1]);
                        Log.LOG_INFO("======== [RECV] Command humidity sensor : " + "\tValue = " + srv.cmd_humi_get.value.ToString());
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_HUM_SENSOR, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_TEMP_SENSOR:
                        srv.cmd_temp_get.value = (short)((UartPacketCommand[startIndex] << 8) + UartPacketCommand[startIndex + 1]);
                        Log.LOG_INFO("======== [RECV] Command temperature sensor : " + "\tValue = " + srv.cmd_temp_get.value.ToString());
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_TEMP_SENSOR, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_LIGHT_SENSOR:
                        srv.cmd_light_get.value = (short)((UartPacketCommand[startIndex] << 8) + UartPacketCommand[startIndex + 1]);
                        Log.LOG_INFO("======== [RECV] Command light sensor : " + "\tValue = " + srv.cmd_light_get.value.ToString());
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_LIGHT_SENSOR, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_LCD:
                        srv.cmd_lcd_display.text = new char[UartPacketCommand.Length - 3];
                        Array.Copy(UartPacketCommand, 3, srv.cmd_lcd_display.text, 0, UartPacketCommand.Length - 3);
                        Log.LOG_INFO("======== [RECV] Command text lcd : " + "\tText = " + srv.cmd_lcd_display.text);
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_LCD, srv);
                        break;

                    case (int)Constants.CMD_ID.CMD_ID_TRACKING_PERFORMANCE:
                        srv.cmd_tracking_performance.totalcount = (int)((UartPacketCommand[startIndex] << 24) + (UartPacketCommand[startIndex + 1] << 16) + (UartPacketCommand[startIndex + 2] << 8) + UartPacketCommand[startIndex + 3]);
                        srv.cmd_tracking_performance.ramused = (int)((UartPacketCommand[startIndex + 4] << 24) + (UartPacketCommand[startIndex + 5] << 16) + (UartPacketCommand[startIndex + 6] << 8) + UartPacketCommand[startIndex + 7]);
                        srv.cmd_tracking_performance.ticktime = (int)((UartPacketCommand[startIndex + 8] << 24) + (UartPacketCommand[startIndex + 9] << 16) + (UartPacketCommand[startIndex + 10] << 8) + UartPacketCommand[startIndex + 11]);
                        //Log.LOG_INFO("======== [RECV] Command tracking performance : " + "\tText = " + srv.cmd_lcd_display.text);
                        if (receivedResult != null) receivedResult(Constants.CMD_ID.CMD_ID_TRACKING_PERFORMANCE, srv);
                        break;

                    default:
                        break;
                }
            }
            catch
            {
            }
        }

        /**
         * @func   SetTimer
         * @brief  Gọi sự kiện timer sau ms
         * @param  
         * @retval None
         */
        private void SetTimer(int ms)
        {
            // Create a timer with a two second interval.
            if (ms <= 0) ms = 1;
            if (processTimer != null) processTimer.Stop();
            processTimer = new System.Timers.Timer(ms);
            // Hook up the Elapsed event for the timer. 
            processTimer.Elapsed += OnTimedEvent;
            processTimer.AutoReset = true;
            processTimer.Enabled = true;
        }

        private void SetComTimer(int ms)
        {
            // Create a timer with a two second interval.
            if (ms <= 0) ms = 1;
            if (ComCheckTimer != null) processTimer.Stop();
            ComCheckTimer = new System.Timers.Timer(ms);
            // Hook up the Elapsed event for the timer. 
            ComCheckTimer.Elapsed += ComTimer_Tick;
            ComCheckTimer.AutoReset = true;
            ComCheckTimer.Enabled = true;
        }

        /**
         * @func   OnTimedEvent
         * @brief  Định kỳ vào kiểm tra xem có lệnh nào cần xử lý không?
         * @param  
         * @retval None
         */
        private int couterReconnect = 0;
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (P.IsOpen && IsRunningUartRecvProcess == false && fifoCmdReceiveUart.Count != 0)
                {
                    processTimer.Enabled = false;
                    CmdRecvFifo_t cmdRecv = fifoCmdReceiveUart.Dequeue();
                    //byte[] temp = new byte[cmdRecv.PacketLength + 3];
                    //for (int i = 0; i < cmdRecv.PacketLength + 2; i++)
                    //{
                    //    if (i == 0) temp[0] = 0x4C;
                    //    else if (i == 1) temp[1] = 0x4D;
                    //    else if (i == 2) temp[2] = cmdRecv.PacketLength;
                    //    else temp[i] = cmdRecv.UartData[i - 3];
                    //}
                    //temp[cmdRecv.PacketLength + 2] = cmdRecv.CheckXor;
                    Log.LOG_INFO(false, Commons.HexToStringWithSpace(cmdRecv.UartData));

                    IsRunningUartRecvProcess = true;

                    UartRecvProcess(cmdRecv.UartData);
                    //SetTimer(fifoCmdReceiveUart.Count);
                    IsRunningUartRecvProcess = false;
                }
                SetTimer(fifoCmdReceiveUart.Count);
            }
            catch
            {
                SetTimer(100);
                IsRunningUartRecvProcess = false;
            }
        }

        /**
         * @func   UartSend
         * @brief  Gửi dữ liệu qua UART và đồng thời hiện thị lên GUI
         * @param  buf : mảng dữ liệu cần gửi
         *          offset : vị trí bắt đầu trong mảng
         *          len : số phần tử cần gửi
         * @retval None
         */
        byte[] bufPrevious = new byte[100];
        int lenPrevious = 0;
        public void UartSend(byte[] buf, int offset, int len)
        {
            try
            {
                P.Write(buf, offset, len);
                //Lưu lại tạm thời
                Commons.memcopy(buf, bufPrevious, (byte)offset, (byte)len);
                lenPrevious = len;
                //

                SetTimerTimeOut(1000);
                //Log log = new Log();
                Log.LOG_INFO(true, Commons.HexToStringWithSpace(buf, len));
            }
            catch
            {
                if (Properties.Settings.Default.PortName == "")
                    MessageBox.Show("Bạn chưa chọn cổng COM!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(Properties.Settings.Default.PortName + " không thể kết nối!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /**
          * @func   UartSendVersionCoorRequest
          * @brief  Gửi bản tin Uart hỏi thông tin về version của Coor từ Host -> Coor
          * @param  None
          * @retval None
        */
        public void UartSend_cmdInformationGet()
        {
            byte len = 6;
            Packet packet = new Packet();
            packet.Push(0xB1);
            packet.Push(len); //Length
            packet.Push(0x00);  //Option

            packet.Push((byte)Constants.CMD_ID.CMD_ID_INFOR);
            packet.Push((byte)Constants.CMD_TYPE.CMD_TYPE_GET);
            packet.Push((byte)frm_sequence++);

            Crc crc = new Crc(CrcStdParams.StandartParameters[CrcAlgorithms.Crc16CcittFalse]);
            byte xor = crc.ComputeXor(packet.GetBuffer(), 2, len);
            packet.Push((byte)xor);

            UartSend(packet.GetBuffer(), 0, (int)packet.Length());
            Log.LOG_INFO("Send information device request");
        }

        /**
         * @func   UartSendStartCmdSet
         * @brief  Gửi bản tin yêu cầu Tool Test bắt đầu quá trình test
         * @param  None
         * @retval None
         */
        public void UartSend_CmdBuzzerSet(byte buzzer_level)
        {
            byte len = 6;
            Packet packet = new Packet();
            packet.Push(0xB1);
            packet.Push(len); //Length
            packet.Push(0x00); //Option

            packet.Push((byte)Constants.CMD_ID.CMD_ID_BUZZER);
            packet.Push((byte)Constants.CMD_TYPE.CMD_TYPE_SET);
            packet.Push((byte)buzzer_level);
            packet.Push((byte)frm_sequence++);

            Crc crc = new Crc(CrcStdParams.StandartParameters[CrcAlgorithms.Crc16CcittFalse]);
            byte xor = crc.ComputeXor(packet.GetBuffer(), 2, len);
            packet.Push((byte)xor);

            UartSend(packet.GetBuffer(), 0, (int)packet.Length());
            Log.LOG_INFO("Send buzzer level");
        }

        /**
         * @func   UartSendStartTouchCmdSet
         * @brief  Sau khi nạp code thì App sẽ gửi bản tin này yêu cầu tiến hành test
         * @param  None
         * @retval None
         */
        public void UartSend_CmdLedSet(byte led_id, byte led_color, byte led_num_blink, byte led_interval, byte led_last_state)
        {
            byte len = 10;
            Packet packet = new Packet();
            packet.Push(0xB1);
            packet.Push(len); //Length
            packet.Push(0x00);  //Option

            packet.Push((byte)Constants.CMD_ID.CMD_ID_LED);
            packet.Push((byte)Constants.CMD_TYPE.CMD_TYPE_SET);
            packet.Push((byte)led_id);
            packet.Push((byte)led_color);
            packet.Push((byte)led_num_blink);
            packet.Push((byte)led_interval);
            packet.Push((byte)led_last_state);
            packet.Push((byte)frm_sequence++);

            Crc crc = new Crc(CrcStdParams.StandartParameters[CrcAlgorithms.Crc16CcittFalse]);
            byte xor = crc.ComputeXor(packet.GetBuffer(), 2, len);
            packet.Push((byte)xor);

            UartSend(packet.GetBuffer(), 0, (int)packet.Length());
            Log.LOG_INFO("Send led set" + led_id.ToString());
        }

        public void UartSend_CmdButtonSet(byte button_id, byte button_state)
        {
            byte len = 7;
            Packet packet = new Packet();
            packet.Push(0xB1);
            packet.Push(len); //Length
            packet.Push(0x00);  //Option

            packet.Push((byte)Constants.CMD_ID.CMD_ID_BUTTON);
            packet.Push((byte)Constants.CMD_TYPE.CMD_TYPE_SET);
            packet.Push((byte)button_id);
            packet.Push((byte)button_state);
            packet.Push((byte)frm_sequence++);

            Crc crc = new Crc(CrcStdParams.StandartParameters[CrcAlgorithms.Crc16CcittFalse]);
            byte xor = crc.ComputeXor(packet.GetBuffer(), 2, len);
            packet.Push((byte)xor);

            UartSend(packet.GetBuffer(), 0, (int)packet.Length());
            Log.LOG_INFO("Send button id = " + button_id.ToString());
        }

        public void UartSend_CmdLcdSet(string text)
        {
            char[] textArr = text.ToCharArray();

            byte len = Convert.ToByte(text.Length);

            Packet packet = new Packet();
            packet.Push(0xB1);
            packet.Push(Convert.ToByte(len + 5)); //Length
            packet.Push(0x00);  //Option

            packet.Push((byte)Constants.CMD_ID.CMD_ID_LCD);
            packet.Push((byte)Constants.CMD_TYPE.CMD_TYPE_SET);

            for (byte i = 0; i < len; i++ )
            {
                packet.Push(Convert.ToByte(textArr[i])); //String
            }

            packet.Push((byte)frm_sequence++);

            Crc crc = new Crc(CrcStdParams.StandartParameters[CrcAlgorithms.Crc16CcittFalse]);
            byte xor = crc.ComputeXor(packet.GetBuffer(), 2, len + 5);
            packet.Push((byte)xor);

            UartSend(packet.GetBuffer(), 0, (int)packet.Length());
            Log.LOG_INFO("Send Reset Touch PcbId = " + text);
        }
    }
}
