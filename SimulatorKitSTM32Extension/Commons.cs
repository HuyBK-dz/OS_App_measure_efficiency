using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulatorKitSTM32Extension
{
    static class Commons
    {
        /**
         * @func    RemoveWhitespace
         * @brief   Xóa toàn bộ khoảng trắng của xâu
         * @param   input : Xâu cần xóa
         * @retval  string : Xâu đã xử lý
         */
        public static string RemoveWhitespace(string input)
        {
            string temp = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsWhiteSpace(input[i]) != true) temp += input[i];
            }
            return temp;
        }

        /**
         * @func    
         * @brief   Đảo ngược xâu
         * @param   input : Xâu cần xóa
         * @retval  string : Xâu đã xử lý
         */
        public static string SwapString(string input)
        {
            string temp = "";
            for (int i = input.Length - 1; i >= 0; i--)
            {
                temp += input[i];
            }
            return temp;
        }
        /**
         * @func   swapArr
         * @brief  Đảo chiều đầu cuối các phần tử trong mảng
         * @param  arr : Mảng cần đảo
         * @retval Mảng đã đảo
         */
        public static byte[] SwapArr(byte[] arr)
        {
            byte[] temp = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                temp[i] = arr[arr.Length - i - 1];
            }
            return temp;
        }
        /**
         * @func   hexStringToArrayByte
         * @brief  None
         * @param   str : Xâu cần chuyển đổi
         *          lenth: Số byte dữ liệu cần lấy (2 char = 1 byte)
         * @retval Mảng byte với lenth phần tử
         */
        public static byte[] HexStringToArrayByte(string str, int lenth)
        {
            byte[] arrByte = new byte[lenth];
            int len = 0;
            if (str.Length % 2 == 0) len = str.Length / 2;
            else { len = str.Length / 2 + 1; str = "0" + str; }
            for (int i = len - 1; i >= 0; i--)
            {
                arrByte[lenth - len + i] = (byte)int.Parse(str.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                if (lenth - len + i <= 0) break;
            }
            return arrByte;
        }
        /**
         * @func   hexStringToArrayByte
         * @brief  None
         * @param  str : Xâu cần chuyển đổi
         * @retval Mảng byte tương ứng
         */
        public static byte[] HexStringToArrayByte(string str)
        {
            int len = 0;
            if (str.Length % 2 == 0) len = str.Length / 2;
            else { len = str.Length / 2 + 1; str = "0" + str; }
            byte[] arrByte = new byte[len];
            for (int i = 0; i < len; i++)
            {
                arrByte[i] = (byte)int.Parse(str.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return arrByte;
        }

        public static string HexToString(byte[] arrHex)
        {
            string str = "";
            for (int i = 0; i < arrHex.Length; i++)
            {
                str += arrHex[i].ToString("X2");
            }
            return str;
        }

        public static string HexToString(byte[] arrHex, int len)
        {
            if (len > arrHex.Length) return "";
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str += arrHex[i].ToString("X2");
            }
            return str;
        }

        public static string HexToStringWithSpace(byte[] arrHex)
        {
            string str = "";
            for (int i = 0; i < arrHex.Length; i++)
            {
                str += arrHex[i].ToString("X2") + " ";
            }
            return str;
        }

        public static string HexToStringWithSpace(byte[] arrHex, int len)
        {
            if (len > arrHex.Length) return "";
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str += arrHex[i].ToString("X2") + " ";
            }
            return str;
        }

        /**
         * @func    memcopy
         * @brief   Copy 2 bảng byte
         * @param   src : bảng gốc
         *          des: bảng đích
         * @retval  true : copy ok
         *          false : copy lỗi
         */
        public static bool memcopy(byte[] src, byte[] des, byte offset, byte len)
        {
            //if( des == null ) des = new byte[len];
            if (len + offset > src.Length) return false;
            for (int i = offset; i < len + offset; i++)
            {
                des[i - offset] = src[i];
            }
            return true;
        }

        /**
         * @func   compareArr
         * @brief  So sánh các phần tử của 2 mảng
         * @param  a, b là 2 mảng cần so sánh
         * @retval true : 2 mảng giống nhau
         *          false : 2 mảng khác nhau
         */
        public static bool compareArr(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }

        /**
         * @func   swapArr
         * @brief  Đảo chiều đầu cuối các phần tử trong mảng
         * @param  arr : Mảng cần đảo
         * @retval Mảng đã đảo
         */
        public static byte[] swapArr(byte[] arr)
        {
            byte[] temp = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                temp[i] = arr[arr.Length - i - 1];
            }
            return temp;
        }
    }
}
