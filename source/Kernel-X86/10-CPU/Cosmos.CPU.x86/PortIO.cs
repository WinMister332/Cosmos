/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          I/O Port In out Functions
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using XSharp;

namespace Cosmos.CPU.x86
{
    public static class PortIO
    {
        /// <summary>
        /// Read 8 bit from IO/Port
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        public static byte In8(uint aAddress)
        {
            // Load address into EDX
            XS.Set(XSRegisters.EDX, XSRegisters.EBP, sourceDisplacement: 0x08, sourceIsIndirect: true);
            // Set EAX = 0x00000000
            XS.Xor(XSRegisters.EAX, XSRegisters.EAX);
            // Read 8 byte And put result into EAX (al)
            XS.LiteralCode("in al, dx");
            // Push EAX
            XS.Push(XSRegisters.EAX);
            return 0x0;
        }

        /// <summary>
        /// Write 8 bit from IO/Port
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        public static void Out8(uint aAddress, byte aValue)
        {
            // Load address into EDX
            XS.Set(XSRegisters.EDX, XSRegisters.EBP, sourceDisplacement: 0xC, sourceIsIndirect: true);
            // Load value into EAX
            XS.Set(XSRegisters.EAX, XSRegisters.EBP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Write 8 byte
            XS.LiteralCode("out dx, al");
        }

        /// <summary>
        /// Read 16 bit from IO/Port
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        public static ushort In16(uint aAddress)
        {
            // Load address into EDX
            XS.Set(XSRegisters.EDX, XSRegisters.EBP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // set EAX = 0x00000000
            XS.Xor(XSRegisters.EAX, XSRegisters.EAX);
            // Read 16 byte And put result into EAX (ax)
            XS.LiteralCode("in ax, dx");
            // Push EAX
            XS.Push(XSRegisters.EAX);

            return 0x0;
        }

        /// <summary>
        /// Write 16 bit from IO/Port
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        public static void Out16(uint aAddress, ushort aValue)
        {
            // Load address into EDX
            XS.Set(XSRegisters.EDX, XSRegisters.EBP, sourceDisplacement: 0xC, sourceIsIndirect: true);
            // Load value into EAX
            XS.Set(XSRegisters.EAX, XSRegisters.EBP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Write 16 byte
            XS.LiteralCode("out dx, ax");
        }

        /// <summary>
        /// Read 32 bit from IO/Port
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        public static uint In32(uint aAddress)
        {
            // Load address into EDX
            XS.Set(XSRegisters.EDX, XSRegisters.EBP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Set EAX = 0x00000000
            XS.Xor(XSRegisters.EAX, XSRegisters.EAX);
            // Read 16 byte And put result into EAX (ax)
            XS.LiteralCode("in eax, dx");
            // Push EAX
            XS.Push(XSRegisters.EAX);

            return 0x0;
        }

        public static void Out32(uint aAddress, uint aValue)
        {
            // Load address into EDX
            XS.Set(XSRegisters.EDX, XSRegisters.EBP, sourceDisplacement: 0xC, sourceIsIndirect: true);
            // Load value into EAX
            XS.Set(XSRegisters.EAX, XSRegisters.EBP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Write 16 byte
            XS.LiteralCode("out dx, eax");
        }

        public static void Read16(uint aAddress, UInt16[] xData)
        {
            for (int i = 0; i < xData.Length; i++)
            {
                xData[i] = In16(aAddress);
            }
        }

        public static void Read16(uint aAddress, byte[] xData)
        {
            for (int i = 0; i < xData.Length; i += 2)
            {
                var aData = In16(aAddress);
                xData[i] = (byte)(aData & 0xFF);
                xData[i + 1] = (byte)(aData >> 8);
            }
        }

        public static void Read16(uint aAddress, byte[] xData, int size)
        {
            Read16(aAddress, xData);

            for (int i = xData.Length; i < size; i += 2)
                In16(aAddress);
        }

        public static void Write16(uint aAddress, byte[] xData)
        {
            for (int i = 0; i < xData.Length; i += 2)
            {
                Out16(aAddress, (ushort)(xData[i + 1] << 8 | xData[i]));
            }
        }

        public static void Wait()
        {
            Out8(0x80, 0x22);
        }

    }
}
