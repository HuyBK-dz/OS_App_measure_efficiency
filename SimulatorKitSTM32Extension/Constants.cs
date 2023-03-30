using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SimulatorKitSTM32Extension
{
    //Class dùng để kiểm tra 1 tiến trình có đang chạy hay không
    public static class ProcessExtensions
    {
        //Kiểm tra 1 process có đang chạy hay không
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }

    public class Constants
    {
        public enum DGV_Device
        {
            STT,
            NAME,
            DEVICE_1_VALUE,
            DEVICE_1_STATE,
            DEVICE_2_VALUE,
            DEVICE_2_STATE,
            DEVICE_3_VALUE,
            DEVICE_3_STATE,
            DEVICE_4_VALUE,
            DEVICE_4_STATE,
            DEVICE_5_VALUE,
            DEVICE_5_STATE,
            DEVICE_6_VALUE,
            DEVICE_6_STATE
        };

        public enum BUTTON_ID
        {
            BUTTON_BOARD_ID = 0x00,
            BUTTON_LEFT_ID = 0x01,
            BUTTON_RIGHT_ID = 0x02,
            BUTTON_DOWN_ID = 0x03,
            BUTTON_UP_ID = 0x04
        }

        public enum BUTTON_STATE
        {
            BUTTON_1_PRESS = 0x00,
            BUTTON_2_PRESS = 0x01,
            BUTTON_HOLD = 0x02,
            BUTTON_RELEASE = 0x03,
            BUTTON_EDGE_RISING = 0x04,
            BUTTON_EDGE_FAILING = 0x05
        }

        public enum LED_ID
        {
            LED_BOARD_ID = 0x00,
            LED_1_ID = 0x01,
            LED_2_ID = 0x02,
        }

        public enum LED_COLOR
        {
            LED_COLOR_RED = 0x00,
            LED_COLOR_GREEN = 0x01,
            LED_COLOR_BLUE = 0x02,
            LED_COLOR_WHITE = 0x03,
            LED_COLOR_BLACK = 0x04,
            LED_COLOR_YELLOW = 0x05
        }

        /**
        * Command ID use for Lumi
        * */
        public enum CMD_ID
        {
            CMD_ID_INFOR = 0x00,
            CMD_ID_LED = 0x01,
            CMD_ID_BUZZER = 0x04,
            CMD_ID_BUTTON = 0x82,
            CMD_ID_TEMP_SENSOR = 0x84,
            CMD_ID_HUM_SENSOR = 0x85,
            CMD_ID_LIGHT_SENSOR = 0x86,
            CMD_ID_LCD = 0x87,
            CMD_ID_TRACKING_PERFORMANCE = 0x90
        }

        public enum CMD_TYPE
        {
            CMD_TYPE_GET = 0x00,
            CMD_TYPE_SET = 0x02,
            CMD_TYPE_RES = 0x01
        }

        public struct cmd_common_t
        {
            public byte cmdid;
            public byte type;
        }

        public struct cmd_button_state_t
        {
            public byte cmdid;
            public byte type;
            public byte epoint;
            public byte state;
        };

        public struct cmd_buzzer_state_t
        {
            public byte cmdid;
            public byte type;
            public byte state;
        };

        public struct cmd_lcd_display_t
        {
            public byte cmdid;
            public byte type;
            public char[] text;
        };

        public struct cmd_temperature_t
        {
            public byte cmdid;
            public byte type;
            public short value;
        };

        public struct cmd_humidity_t
        {
            public byte cmdid;
            public byte type;
            public short value;
        };

        public struct cmd_light_t
        {
            public byte cmdid;
            public byte type;
            public short value;
        };

        public struct cmd_led_indicator
        {
            public byte cmdid;
            public byte type;
            public byte numID;
            public byte color;
            public byte level;
            //public byte counter;
            //public byte interval;
            //public byte laststate;
            //public byte reserved;
        };

        public struct cmd_setup_config_t
        {
            public byte cmdid;
            public byte type;
            public byte mode;
            public byte[] data;
        };

        public struct cmd_tracking_performance_t
        {
            public byte cmdid;
            public byte type;
            public int totalcount;
            public int ramused;
            public int ticktime;
        };

        // The Service struct will hold the Address, the Port and the Protocol
        public struct Service
        {
            public cmd_common_t cmd_common;
            public cmd_button_state_t cmd_button_state;
            public cmd_buzzer_state_t cmd_buzzer_state;
            public cmd_led_indicator cmd_led_indicator;
            public cmd_lcd_display_t cmd_lcd_display;
            public cmd_temperature_t cmd_temp_get;
            public cmd_humidity_t cmd_humi_get;
            public cmd_light_t cmd_light_get;
            public cmd_setup_config_t cmd_setup_config;
            public cmd_tracking_performance_t cmd_tracking_performance;
        }
    }
}
