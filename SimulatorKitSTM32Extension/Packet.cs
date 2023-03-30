using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulatorKitSTM32Extension
{
    class Packet
    {
        private byte[] m_pbyBuffer;
        private UInt32 m_dwLength;
        private UInt32 m_dwCount;
        public Packet()
        {
            m_pbyBuffer = null;
            m_dwLength = 0;
            m_dwCount = 0;
        }
        /// @fn     Packet
        /// @brief  Constructor
        /// @param  DWORD dwLength
        /// @return None
        public Packet(UInt32 dwLength)
        {
            m_pbyBuffer = null;
            m_dwCount = 0;
            m_dwLength = dwLength;

            if (dwLength > 0)
            {
                m_pbyBuffer = new byte[dwLength];
            }
        }

        /// @fn     Packet
        /// @brief  Constructor
        /// @param  buffer
        /// @return None
        public Packet(byte[] buffer)
        {
            m_pbyBuffer = null;
            m_dwLength = (UInt32)buffer.Length;
            m_dwCount = (UInt32)buffer.Length;
            if (buffer.Length > 0)
            {
                m_pbyBuffer = new byte[m_dwLength];
                for (UInt32 i = 0; i < buffer.Length; i++)
                {
                    m_pbyBuffer[i] = buffer[i];
                }
            }
        }


        /// @fn     Packet
        /// @brief  Copy constructor
        /// @param  PACKET& copied
        /// @return None
        public Packet(Packet copied)
        {
            m_pbyBuffer = new byte[m_dwLength];
            m_pbyBuffer = copied.m_pbyBuffer;
            m_dwLength = copied.m_dwLength;
            m_dwCount = copied.m_dwCount;
        }

        /// @fn     ~Packet
        /// @brief  Destructor
        /// @param  None
        /// @return None
        ~Packet()
        {
            if (m_pbyBuffer != null)
            {
                m_pbyBuffer = null;
            }
        }

        /// @fn     Reset
        /// @brief  Delete buffer, length, count
        /// @param  None
        /// @return None
        public void Reset()
        {
            if (m_pbyBuffer != null)
            {
                m_pbyBuffer = null;
            }
            m_dwLength = 0;
            m_dwCount = 0;
        }

        /// @fn     Push
        /// @brief  Push a byte to buffer
        /// @param  BYTE byData
        /// @return BOOL
        public bool Push(byte byData)
        {
            if (m_dwCount + 1 > m_dwLength)
            {
                m_dwLength = m_dwCount + 1;
                byte[] temp = new byte[m_dwLength];
                for (UInt32 i = 0; i < m_dwCount; i++)
                {
                    temp[i] = m_pbyBuffer[i];
                }
                if (m_pbyBuffer != null)
                {
                    m_pbyBuffer = null;
                }
                m_pbyBuffer = temp;
            }
            m_pbyBuffer[m_dwCount++] = byData;
            return true;
        }

        /// @fn     Push
        /// @brief  Push a buffer with length to
        /// @param  PBYTE pbyBuffer
        /// @param  DWORD dwLength
        /// @return BOOL
        public bool Push(byte[] pbBuffer, UInt32 dwLength)
        {
            if (m_dwCount + dwLength > m_dwLength)
            {
                m_dwLength = m_dwCount + dwLength;
                byte[] temp = new byte[m_dwLength];
                for (UInt32 i = 0; i < m_dwCount; i++)
                {
                    temp[i] = m_pbyBuffer[i];
                }
                if (m_pbyBuffer != null)
                {
                    m_pbyBuffer = null;
                }
                m_pbyBuffer = temp;
            }

            for (UInt32 i = 0; i < dwLength; i++)
            {
                m_pbyBuffer[m_dwCount++] = pbBuffer[i];
            }
            return true;
        }

        /// @func   GetBuffer
        /// @brief  None
        /// @param  None
        /// @retval None
        public byte[] GetBuffer()
        {
            return m_pbyBuffer;
        }

        /// @fn     AtPosition
        /// @brief  Get value at Position
        /// @param  DWORD dwPosition
        /// @return BYTE Value at position.
        /// @return throw exception if wrong position
        public byte AtPosition(UInt32 dwPosition)
        {
            if (dwPosition >= m_dwLength)
            {
                //throw exception
            }
            return m_pbyBuffer[dwPosition];
        }


        /// @fn     Count
        /// @brief  Get count
        /// @param  None
        /// @return DWORD
        public UInt32 Count()
        {
            return m_dwCount;
        }

        /// @fn     Length
        /// @brief  Get length
        /// @param  None
        /// @return DWORD
        public UInt32 Length()
        {
            return m_dwLength;
        }

        /// @fn     IsEmpty
        /// @brief  Check buffer is empty or not
        /// @param  None
        /// @return BOOL
        public bool IsEmpty()
        {
            return (bool)(m_dwCount == 0);
        }

        /// @fn     Printf
        /// @brief  Check buffer is full or not
        /// @param  None
        /// @return BOOL
        public bool IsFull()
        {
            return (bool)(m_dwCount >= m_dwLength);
        }

        /// @fn     ResetPacket
        /// @brief  Delete buffer, set length to new value
        /// @param  DWORD dwLength
        /// @return None
        public void ResetPacket(UInt32 dwLength)
        {
            if (dwLength > 0)
            {
                if (m_pbyBuffer != null)
                {
                    m_pbyBuffer = null;
                }
                m_pbyBuffer = new byte[dwLength];
                m_dwLength = dwLength;
                m_dwCount = 0;
            }
        }


        /// @fn     Edit
        /// @brief  Edit value of buffer at position
        /// @param  BYTE byData
        /// @param  DWORD dwPosition
        /// @return None
        public void Edit(byte byData, UInt32 dwPosition)
        {
            if (dwPosition >= m_dwLength)
            {
                //Throw exception
            }
            m_pbyBuffer[dwPosition] = byData;
        }

        public override string ToString()
        {
            string temp = "";
            for (UInt32 i = 0; i < m_dwLength; i++)
            {
                if (i != 0) temp += " ";
                temp += m_pbyBuffer[i].ToString("X2");
            }
            return temp;
        }
    }
}
