using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SimulatorKitSTM32Extension
{
    public enum CrcAlgorithms
    {
        Undefined,
        Crc8,
        Crc8Cdma2000,
        Crc8Darc,
        Crc8DvbS2,
        Crc8Ebu,
        Crc8ICode,
        Crc8Itu,
        Crc8Maxim,
        Crc8Rohc,
        Crc8Wcdma,
        Crc16CcittFalse,
        Crc16Arc,
        Crc16AugCcitt,
        Crc16Buypass,
        Crc16Cdma2000,
        Crc16Dds110,
        Crc16DectR,
        Crc16DectX,
        Crc16Dnp,
        Crc16En13757,
        Crc16Genibus,
        Crc16Maxim,
        Crc16Mcrf4Xx,
        Crc16Riello,
        Crc16T10Dif,
        Crc16Teledisk,
        Crc16Tms37157,
        Crc16Usb,
        CrcA,
        Crc16Kermit,
        Crc16Modbus,
        Crc16X25,
        Crc16Xmodem,
        Crc32,
        Crc32Bzip2,
        Crc32C,
        Crc32D,
        Crc32Jamcrc,
        Crc32Mpeg2,
        Crc32Posix,
        Crc32Q,
        Crc32Xfer,
        Crc40Gsm,
        Crc64,
        Crc64We,
        Crc64Xz,
        Crc24,
        Crc24FlexrayA,
        Crc24FlexrayB,
        Crc31Philips,
        Crc10,
        Crc10Cdma2000,
        Crc11,
        Crc123Gpp,
        Crc12Cdma2000,
        Crc12Dect,
        Crc13Bbc,
        Crc14Darc,
        Crc15,
        Crc15Mpt1327
    }

    public class Crc : HashAlgorithm
    {
        private readonly ulong _mask;

        private readonly ulong[] _table = new ulong[256];

        private ulong _currentValue;

        public Crc(Parameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            Parameters = parameters;

            _mask = ulong.MaxValue >> (64 - HashSize);

            Init();
        }

        public override bool CanTransformMultipleBlocks
        {
            get
            {
                return base.CanTransformMultipleBlocks;
            }
        }

        public override int HashSize { get { return Parameters.HashSize; } }

        public Parameters Parameters { get; private set; }

        public UInt64[] GetTable()
        {
            var res = new UInt64[_table.Length];
            Array.Copy(_table, res, _table.Length);
            return res;
        }

        public override void Initialize()
        {
            _currentValue = Parameters.RefOut ? CrcHelper.ReverseBits(Parameters.Init, HashSize) : Parameters.Init;
        }

        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            _currentValue = ComputeCrc(_currentValue, array, ibStart, cbSize);
        }

        protected override byte[] HashFinal()
        {
            return CrcHelper.ToBigEndianBytes(_currentValue ^ Parameters.XorOut);
        }

        private void Init()
        {
            CreateTable();
            Initialize();
        }

        #region Main functions

        public ulong ComputeCrc(ulong init, byte[] data, int offset, int length)
        {
            ulong crc = Parameters.Init;

            if (Parameters.RefOut)
            {
                for (int i = offset; i < offset + length; i++)
                {
                    crc = (_table[(crc ^ data[i]) & 0xFF] ^ (crc >> 8));
                    crc &= _mask;
                }
            }
            else
            {
                int toRight = (HashSize - 8);
                toRight = toRight < 0 ? 0 : toRight;
                for (int i = offset; i < offset + length; i++)
                {
                    crc = (_table[((crc >> toRight) ^ data[i]) & 0xFF] ^ (crc << 8));
                    crc &= _mask;
                }
            }
            //if (Parameters.Name == "CRC-16/X-25") return ~crc;
            return crc;
        }

        public byte ComputeXor(byte[] data, int offset, int length)
        {
            byte xor = 0xFF;

            for (int i = offset; i <= length; i++)
            {
                xor ^= data[i];
            }

            return xor;
        }

        //PhuongNP
        public byte[] crc16X25(byte[] data, int offset, int length)
        {
            //Crc c = new Crc(CrcStdParams.StandartParameters[CrcAlgorithms.Crc16X25]);
            byte[] crc = new byte[2];
            ulong temp = ~ComputeCrc(0xFFFF, data, 0, length);
            crc[1] = (byte)((int)(temp) & 0xff);
            crc[0] = (byte)(((int)(temp) >> 8) & 0xff);
            return crc;
        }
        //

        private void CreateTable()
        {
            for (int i = 0; i < _table.Length; i++)
                _table[i] = CreateTableEntry(i);
        }

        private ulong CreateTableEntry(int index)
        {
            ulong r = (ulong)index;

            if (Parameters.RefIn)
                r = CrcHelper.ReverseBits(r, HashSize);
            else if (HashSize > 8)
                r <<= (HashSize - 8);

            ulong lastBit = (1ul << (HashSize - 1));

            for (int i = 0; i < 8; i++)
            {
                if ((r & lastBit) != 0)
                    r = ((r << 1) ^ Parameters.Poly);
                else
                    r <<= 1;
            }

            if (Parameters.RefIn)
                r = CrcHelper.ReverseBits(r, HashSize);

            return r & _mask;
        }

        #endregion Main functions

        #region Test functions

        /// <summary>
        /// Проверить алгоритмы на корректность
        /// </summary>
        public static CheckResult[] CheckAll()
        {
            var parameters = CrcStdParams.StandartParameters;

            var result = new List<CheckResult>();
            foreach (var parameter in parameters)
            {
                Crc crc = new Crc(parameter.Value);

                result.Add(new CheckResult()
                {
                    Parameter = parameter.Value,
                    Table = crc.GetTable()
                });
            }

            return result.ToArray();
        }

        public bool IsRight()
        {
            byte[] bytes = Encoding.ASCII.GetBytes("123456789");

            var hashBytes = ComputeHash(bytes, 0, bytes.Length);

            var hash = CrcHelper.FromBigEndian(hashBytes, HashSize);

            if (hash != Parameters.Check)
                throw new Exception("Algo check failure!");

            return hash == Parameters.Check;
        }

        public class CheckResult
        {
            public Parameters Parameter { get; set; }

            public ulong[] Table { get; set; }
        }

        #endregion Test functions
    }

    public class Parameters
    {
        public Parameters(string name, int hashSize, ulong poly, ulong init, bool refIn, bool refOut, ulong xorOut, ulong check)
        {
            Name = name;
            Check = check;
            Init = init;
            Poly = poly;
            RefIn = refIn;
            RefOut = refOut;
            XorOut = xorOut;
            HashSize = hashSize;
        }

        /// <summary>
        /// This field is not strictly part of the definition, and, in
        /// the event of an inconsistency between this field and the other
        /// field, the other fields take precedence.This field is a check
        /// value that can be used as a weak validator of implementations of
        /// the algorithm.The field contains the checksum obtained when the
        /// ASCII string "123456789" is fed through the specified algorithm
        /// (i.e. 313233... (hexadecimal)).
        /// </summary>
        public ulong Check { get; private set; }

        /// <summary>
        /// This is hash size.
        /// </summary>
        public int HashSize { get; private set; }

        /// <summary>
        /// This parameter specifies the initial value of the register
        /// when the algorithm starts.This is the value that is to be assigned
        /// to the register in the direct table algorithm. In the table
        /// algorithm, we may think of the register always commencing with the
        /// value zero, and this value being XORed into the register after the
        /// N'th bit iteration. This parameter should be specified as a
        /// hexadecimal number.
        /// </summary>
        public ulong Init { get; private set; }

        /// <summary>
        /// This is a name given to the algorithm. A string value.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// This parameter is the poly. This is a binary value that
        /// should be specified as a hexadecimal number.The top bit of the
        /// poly should be omitted.For example, if the poly is 10110, you
        /// should specify 06. An important aspect of this parameter is that it
        /// represents the unreflected poly; the bottom bit of this parameter
        /// is always the LSB of the divisor during the division regardless of
        /// whether the algorithm being modelled is reflected.
        /// </summary>
        public ulong Poly { get; private set; }

        /// <summary>
        /// This is a boolean parameter. If it is FALSE, input bytes are
        /// processed with bit 7 being treated as the most significant bit
        /// (MSB) and bit 0 being treated as the least significant bit.If this
        /// parameter is FALSE, each byte is reflected before being processed.
        /// </summary>
        public bool RefIn { get; private set; }

        /// <summary>
        /// This is a boolean parameter. If it is set to FALSE, the
        /// final value in the register is fed into the XOROUT stage directly,
        /// otherwise, if this parameter is TRUE, the final register value is
        /// reflected first.
        /// </summary>
        public bool RefOut { get; private set; }

        /// <summary>
        /// This is an W-bit value that should be specified as a
        /// hexadecimal number.It is XORed to the final register value (after
        /// the REFOUT) stage before the value is returned as the official
        /// checksum.
        /// </summary>
        public ulong XorOut { get; private set; }
    }

    public static class CrcStdParams
    {
        public static readonly Dictionary<CrcAlgorithms, Parameters> StandartParameters = new Dictionary
            <CrcAlgorithms, Parameters>()
        {
            //CRC-8
            {CrcAlgorithms.Crc8           , new Parameters("CRC-8", 8, 0x7, 0x0, false, false, 0x0, 0xF4)},
            {CrcAlgorithms.Crc8Cdma2000   , new Parameters("CRC-8/CDMA2000", 8, 0x9B, 0xFF, false, false, 0x0, 0xDA)},
            {CrcAlgorithms.Crc8Darc       , new Parameters("CRC-8/DARC", 8, 0x39, 0x0, true, true, 0x0, 0x15)},
            {CrcAlgorithms.Crc8DvbS2      , new Parameters("CRC-8/DVB-S2", 8, 0xD5, 0x0, false, false, 0x0, 0xBC)},
            {CrcAlgorithms.Crc8Ebu        , new Parameters("CRC-8/EBU", 8, 0x1D, 0xFF, true, true, 0x0, 0x97)},
            {CrcAlgorithms.Crc8ICode      , new Parameters("CRC-8/I-CODE", 8, 0x1D, 0xFD, false, false, 0x0, 0x7E)},
            {CrcAlgorithms.Crc8Itu        , new Parameters("CRC-8/ITU", 8, 0x7, 0x0, false, false, 0x55, 0xA1)},
            {CrcAlgorithms.Crc8Maxim      , new Parameters("CRC-8/MAXIM", 8, 0x31, 0x0, true, true, 0x0, 0xA1)},
            {CrcAlgorithms.Crc8Rohc       , new Parameters("CRC-8/ROHC", 8, 0x7, 0xFF, true, true, 0x0, 0xD0)},
            {CrcAlgorithms.Crc8Wcdma      , new Parameters("CRC-8/WCDMA", 8, 0x9B, 0x0, true, true, 0x0, 0x25)},

            //CRC-10
            {CrcAlgorithms.Crc10          , new Parameters("CRC-10", 10, 0x233, 0x0, false, false, 0x0, 0x199)},
            {CrcAlgorithms.Crc10Cdma2000  , new Parameters("CRC-10/CDMA2000", 10, 0x3D9, 0x3FF, false, false, 0x0, 0x233)},

            //CRC-11
            {CrcAlgorithms.Crc11          , new Parameters("CRC-11", 11, 0x385, 0x1A, false, false, 0x0, 0x5A3)},

            //CRC-12
            {CrcAlgorithms.Crc123Gpp      , new Parameters("CRC-12/3GPP", 12, 0x80F, 0x0, false, true, 0x0, 0xDAF)},
            {CrcAlgorithms.Crc12Cdma2000  , new Parameters("CRC-12/CDMA2000", 12, 0xF13, 0xFFF, false, false, 0x0, 0xD4D)},
            {CrcAlgorithms.Crc12Dect      , new Parameters("CRC-12/DECT", 12, 0x80F, 0x0, false, false, 0x0, 0xF5B)},

            //CRC-13
            {CrcAlgorithms.Crc13Bbc       , new Parameters("CRC-13/BBC", 13, 0x1CF5, 0x0, false, false, 0x0, 0x4FA)},

            //CRC-14
            {CrcAlgorithms.Crc14Darc      , new Parameters("CRC-14/DARC", 14, 0x805, 0x0, true, true, 0x0, 0x82D)},

            //CRC-15
            {CrcAlgorithms.Crc15          , new Parameters("CRC-15", 15, 0x4599, 0x0, false, false, 0x0, 0x59E)},
            {CrcAlgorithms.Crc15Mpt1327   , new Parameters("CRC-15/MPT1327", 15, 0x6815, 0x0, false, false, 0x1, 0x2566)},

            //CRC-16
            {CrcAlgorithms.Crc16CcittFalse, new Parameters("CRC-16/CCITT-FALSE", 16, 0x1021, 0xFFFF, false, false, 0x0, 0x29B1)},
            {CrcAlgorithms.Crc16Arc       , new Parameters("CRC-16/ARC", 16, 0x8005, 0x0, true, true, 0x0, 0xBB3D)},
            {CrcAlgorithms.Crc16AugCcitt  , new Parameters("CRC-16/AUG-CCITT", 16, 0x1021, 0x1D0F, false, false, 0x0, 0xE5CC)},
            {CrcAlgorithms.Crc16Buypass   , new Parameters("CRC-16/BUYPASS", 16, 0x8005, 0x0, false, false, 0x0, 0xFEE8)},
            {CrcAlgorithms.Crc16Cdma2000  , new Parameters("CRC-16/CDMA2000", 16, 0xC867, 0xFFFF, false, false, 0x0, 0x4C06)},
            {CrcAlgorithms.Crc16Dds110    , new Parameters("CRC-16/DDS-110", 16, 0x8005, 0x800D, false, false, 0x0, 0x9ECF)},
            {CrcAlgorithms.Crc16DectR     , new Parameters("CRC-16/DECT-R", 16, 0x589, 0x0, false, false, 0x1, 0x7E)},
            {CrcAlgorithms.Crc16DectX     , new Parameters("CRC-16/DECT-X", 16, 0x589, 0x0, false, false, 0x0, 0x7F)},
            {CrcAlgorithms.Crc16Dnp       , new Parameters("CRC-16/DNP", 16, 0x3D65, 0x0, true, true, 0xFFFF, 0xEA82)},
            {CrcAlgorithms.Crc16En13757   , new Parameters("CRC-16/EN-13757", 16, 0x3D65, 0x0, false, false, 0xFFFF, 0xC2B7)},
            {CrcAlgorithms.Crc16Genibus   , new Parameters("CRC-16/GENIBUS", 16, 0x1021, 0xFFFF, false, false, 0xFFFF, 0xD64E)},
            {CrcAlgorithms.Crc16Maxim     , new Parameters("CRC-16/MAXIM", 16, 0x8005, 0x0, true, true, 0xFFFF, 0x44C2)},
            {CrcAlgorithms.Crc16Mcrf4Xx   , new Parameters("CRC-16/MCRF4XX", 16, 0x1021, 0xFFFF, true, true, 0x0, 0x6F91)},
            {CrcAlgorithms.Crc16Riello    , new Parameters("CRC-16/RIELLO", 16, 0x1021, 0xB2AA, true, true, 0x0, 0x63D0)},
            {CrcAlgorithms.Crc16T10Dif    , new Parameters("CRC-16/T10-DIF", 16, 0x8BB7, 0x0, false, false, 0x0, 0xD0DB)},
            {CrcAlgorithms.Crc16Teledisk  , new Parameters("CRC-16/TELEDISK", 16, 0xA097, 0x0, false, false, 0x0, 0xFB3)},
            {CrcAlgorithms.Crc16Tms37157  , new Parameters("CRC-16/TMS37157", 16, 0x1021, 0x89EC, true, true, 0x0, 0x26B1)},
            {CrcAlgorithms.Crc16Usb       , new Parameters("CRC-16/USB", 16, 0x8005, 0xFFFF, true, true, 0xFFFF, 0xB4C8)},
            {CrcAlgorithms.CrcA           , new Parameters("CRC-A", 16, 0x1021, 0xC6C6, true, true, 0x0, 0xBF05)},
            {CrcAlgorithms.Crc16Kermit    , new Parameters("CRC-16/KERMIT", 16, 0x1021, 0x0, true, true, 0x0, 0x2189)},
            {CrcAlgorithms.Crc16Modbus    , new Parameters("CRC-16/MODBUS", 16, 0x8005, 0xFFFF, true, true, 0x0, 0x4B37)},
            {CrcAlgorithms.Crc16X25       , new Parameters("CRC-16/X-25", 16, 0x1021, 0xFFFF, true, true, 0xFFFF, 0x906E)},
            {CrcAlgorithms.Crc16Xmodem    , new Parameters("CRC-16/XMODEM", 16, 0x1021, 0x0, false, false, 0x0, 0x31C3)},

            //CRC-24
            {CrcAlgorithms.Crc24          , new Parameters("CRC-24", 24, 0x864CFB, 0xB704CE, false, false, 0x0, 0x21CF02)},
            {CrcAlgorithms.Crc24FlexrayA  , new Parameters("CRC-24/FLEXRAY-A", 24, 0x5D6DCB, 0xFEDCBA, false, false, 0x0, 0x7979BD)},
            {CrcAlgorithms.Crc24FlexrayB  , new Parameters("CRC-24/FLEXRAY-B", 24, 0x5D6DCB, 0xABCDEF, false, false, 0x0, 0x1F23B8)},

            //CRC-31
            {CrcAlgorithms.Crc31Philips   , new Parameters("CRC-31/PHILIPS", 31, 0x4C11DB7, 0x7FFFFFFF, false, false, 0x7FFFFFFF, 0xCE9E46C)},

            //CRC-32
            {CrcAlgorithms.Crc32          , new Parameters("CRC-32", 32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xCBF43926)},
            {CrcAlgorithms.Crc32Bzip2     , new Parameters("CRC-32/BZIP2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0xFFFFFFFF, 0xFC891918)},
            {CrcAlgorithms.Crc32C         , new Parameters("CRC-32C", 32, 0x1EDC6F41, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0xE3069283)},
            {CrcAlgorithms.Crc32D         , new Parameters("CRC-32D", 32, 0xA833982B, 0xFFFFFFFF, true, true, 0xFFFFFFFF, 0x87315576)},
            {CrcAlgorithms.Crc32Jamcrc    , new Parameters("CRC-32/JAMCRC", 32, 0x04C11DB7, 0xFFFFFFFF, true, true, 0x00000000, 0x340BC6D9)},
            {CrcAlgorithms.Crc32Mpeg2     , new Parameters("CRC-32/MPEG-2", 32, 0x04C11DB7, 0xFFFFFFFF, false, false, 0x00000000, 0x0376E6E7)},
            {CrcAlgorithms.Crc32Posix     , new Parameters("CRC-32/POSIX", 32, 0x04C11DB7, 0x00000000, false, false, 0xFFFFFFFF, 0x765E7680)},
            {CrcAlgorithms.Crc32Q         , new Parameters("CRC-32Q", 32, 0x814141AB, 0x00000000, false, false, 0x00000000, 0x3010BF7F)},
            {CrcAlgorithms.Crc32Xfer      , new Parameters("CRC-32/XFER", 32, 0x000000AF, 0x00000000, false, false, 0x00000000, 0xBD0BE338)},

            //CRC-40
            {CrcAlgorithms.Crc40Gsm       , new Parameters("CRC-40/GSM", 40, 0x4820009, 0x0, false, false, 0xFFFFFFFFFF, 0xD4164FC646)},

            //CRC-64
            {CrcAlgorithms.Crc64          , new Parameters("CRC-64",64, 0x42F0E1EBA9EA3693, 0x00000000, false, false, 0x00000000, 0x6C40DF5F0B497347)},
            {CrcAlgorithms.Crc64We        , new Parameters("CRC-64/WE", 64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, false, false, 0xFFFFFFFFFFFFFFFF,0x62EC59E3F1A4F00A)},
            {CrcAlgorithms.Crc64Xz        , new Parameters("CRC-64/XZ", 64, 0x42F0E1EBA9EA3693, 0xFFFFFFFFFFFFFFFF, true, true, 0xFFFFFFFFFFFFFFFF,0x995DC9BBDF1939FA)}
        };
    }

    public static class CrcHelper
    {
        #region internal

        #region reverseBits

        /*internal static byte ReverseBits(byte b)
        {
            byte newValue = 0;

            for (int i = 7; i >= 0; i--)
            {
                newValue |= (byte)((b & 1) << i);
                b >>= 1;
            }
            return newValue;
        }

        internal static ushort ReverseBits(ushort us)
        {
            ushort newValue = 0;

            for (int i = 15; i >= 0; i--)
            {
                newValue |= (ushort)((us & 1) << i);
                us >>= 1;
            }
            return newValue;
        }

        internal static uint ReverseBits(uint ui)
        {
            uint newValue = 0;

            for (int i = 31; i >= 0; i--)
            {
                newValue |= (ui & 1) << i;
                ui >>= 1;
            }
            return newValue;
        }*/

        internal static ulong ReverseBits(ulong ul, int valueLength)
        {
            ulong newValue = 0;

            for (int i = valueLength - 1; i >= 0; i--)
            {
                newValue |= (ul & 1) << i;
                ul >>= 1;
            }

            return newValue;
        }

        #endregion reverseBits

        #region ToBigEndian

        internal static byte[] ToBigEndianBytes(UInt32 value)
        {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        internal static byte[] ToBigEndianBytes(UInt16 value)
        {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        internal static byte[] ToBigEndianBytes(UInt64 value)
        {
            var result = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        #endregion ToBigEndian

        #region FromBigEndian

        internal static UInt16 FromBigToUInt16(byte[] buffer, int start)
        {
            return (ushort)(buffer[start] << 8 | buffer[start + 1]);
        }

        internal static UInt32 FromBigToUInt32(byte[] buffer, int start)
        {
            return (uint)(buffer[start] << 24 | buffer[start + 1] << 16 | buffer[start + 2] << 8 | buffer[start + 3]);
        }

        internal static UInt64 FromBigToUInt64(byte[] buffer, int start)
        {
            ulong result = 0;
            for (int i = 0; i < 8; i++)
            {
                result |= ((ulong)buffer[i]) << (64 - 8 * (i + 1));
            }

            return result;
        }

        #endregion FromBigEndian

        #endregion internal

        #region public

        /// <summary>
        /// Use this method for convert hash from byte array to UInt16 value.
        /// </summary>
        public static ushort ToUInt16(byte[] hash)
        {
            return FromBigToUInt16(hash, 0);
        }

        /// <summary>
        /// Use this method for convert hash from byte array to UInt32 value.
        /// </summary>
        public static uint ToUInt32(byte[] hash)
        {
            return FromBigToUInt32(hash, 0);
        }

        #endregion public

        public static ulong FromBigEndian(byte[] hashBytes, int hashSize)
        {
            ulong result = 0;
            for (int i = 0; i < 8; i++)
            {
                result |= ((ulong)hashBytes[i]) << (64 - 8 * (i + 1));
            }

            return result;
        }
    }
}
