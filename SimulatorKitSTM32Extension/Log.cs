using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SimulatorKitSTM32Extension
{
    static class Log
    {
        private static String PathLogFile = "Log.txt";

        public static void LOG_INFO(bool dir, String str)
        {
            try
            {
                if (!File.Exists(PathLogFile)) //Nếu không tồn tại thì tạo mới
                {
                    StreamWriter sw = File.CreateText(PathLogFile);
                    sw.Close();
                }
                //File quá lớn thì xóa
                long length = new System.IO.FileInfo(PathLogFile).Length;
                if (length > 100000) File.Delete(PathLogFile);

                StreamWriter f = File.AppendText(PathLogFile);
                if (dir) str = "[ >> ] " + str;
                else str = "[ << ] " + str;
                f.WriteLine("[" + DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss.fff ") + "]" + str);
                f.Close();
            }
            catch
            { }
        }

        public static void LOG_INFO(String str)
        {
            try
            {
                if (!File.Exists(PathLogFile)) //Nếu không tồn tại thì tạo mới
                {
                    StreamWriter sw = File.CreateText(PathLogFile);
                    sw.Close();
                }
                //File quá lớn thì xóa
                long length = new System.IO.FileInfo(PathLogFile).Length;
                if (length > 100000) File.Delete(PathLogFile);

                StreamWriter f = File.AppendText(PathLogFile);
            
                f.WriteLine("[" + DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss.fff ") + "]" + str);
                f.Close();
            }
            catch
            { }
        }
    }
}
