/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Memory
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;
using XSharp;

namespace Cosmos.CPU.x86
{
    public static class xMemory
    {
        /// <summary>
        /// Read 32 bit Memory at given address :)
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        // [NoException]
        // [Assembly(false)]
        public static uint Read32(uint aAddress)
        {
            // Load address into EAX
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            // Read memory into EAX
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.EAX, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.EAX, sourceIsIndirect: true);
            // Return
            //new Ret { Offset = 0x4 };
            XS.LiteralCode("ret 0x04");
            // XS.re
            // XS.InterruptReturn()

            return 0; // For c# error --> Don't make any sense for compiler
        }

        /// <summary>
        /// Read 16 bit Memory at given address :)
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        // [NoException]
        // [Assembly(false)]
        public static ushort Read16(uint aAddress)
        {
            // Load address into EAX
            // new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x04, sourceIsIndirect: true);
            // Read memory into EAX
            // new Movzx { DestinationReg = Register.EAX, SourceReg = Register.EAX, SourceIndirect = true, Size = 16 };
            //   XS.Set(XSRegisters.EAX, XSRegisters.EAX, sourceIsIndirect: true, size: 16);
            XS.Set(XSRegisters.EAX, XSRegisters.EAX, sourceIsIndirect: true, size: XSRegisters.RegisterSize.Short16);
            // Return

            //new Ret { Offset = 0x4 };
            XS.LiteralCode("ret 0x04");

            return 0; // For c# error --> Don't make any sense for compiler
        }

        /// <summary>
        /// Read 8 bit Memory at given address :)
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        // [NoException]
        // [Assembly(false)]
        public static byte Read8(uint aAddress)
        {
            // Load address into EAX
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            // Read memory into EAX
            // new Movzx { DestinationReg = Register.EAX, SourceReg = Register.EAX, SourceIndirect = true, Size = 8 };
            XS.Set(XSRegisters.EAX, XSRegisters.EAX, sourceIsIndirect: true, size: XSRegisters.RegisterSize.Byte8);
            // Return
            //new Ret { Offset = 0x4 };
            XS.LiteralCode("ret 0x4");

            return 0; // For c# error --> Don't make any sense for compiler
        }

        /// <summary>
        /// Write 32 bit Memory at given address :)
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        ///  [NoException]
        // [Assembly(false)]
        public static void Write32(uint aAddress, uint Value)
        {
            // Load address into EAX
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x8, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Load Value into EDX
            //new Mov { DestinationReg = Register.EBX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EBX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            // Save value at mem Location
            // new Mov { DestinationReg = Register.EAX, SourceReg = Register.EBX, DestinationIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.EBX, destinationIsIndirect: true);
            // Return
            //  new Ret { Offset = 0x8 };
            XS.LiteralCode("ret 0x8");
        }

        /// <summary>
        /// Write 16 bit Memory at given address :)
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        // [NoException]
        //  [Assembly(false)]
        public static void Write16(uint aAddress, ushort Value)
        {
            // Load address into EAX
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x8, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Load Value into EDX
            // new Mov { DestinationReg = Register.EBX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EBX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            // Save value at mem Location
            // new Mov { DestinationReg = Register.EAX, SourceReg = Register.BX, DestinationIndirect = true, Size = 16 };
            XS.Set(XSRegisters.EAX, XSRegisters.BX, destinationIsIndirect: true, size: XSRegisters.RegisterSize.Short16);
            // Return
            //  new Ret { Offset = 0x8 };
            XS.LiteralCode("ret 0x8");
        }

        /// <summary>
        /// Write 8 bit Memory at given address :)
        /// </summary>
        /// <param name="aAddress">Address of memory</param>
        /// <returns></returns>
        //[NoException]
        // [Assembly(false)]
        public static void Write8(uint aAddress, byte Value)
        {
            // Load address into EAX
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x8, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // Load Value into EDX
            // new Mov { DestinationReg = Register.EBX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EBX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            // Save value at mem Location
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.BL, DestinationIndirect = true, Size = 8 };
            XS.Set(XSRegisters.EAX, XSRegisters.BL, destinationIsIndirect: true, size: XSRegisters.RegisterSize.Byte8);
            // Return
            // new Ret { Offset = 0x8 };
            XS.LiteralCode("ret 0x8");
        }

        //  [NoException]
        //[Assembly(false)]
        public static void FastCopy(uint aDest, uint aSrc, uint aLen)
        {
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            //  new Mov { DestinationReg = Register.ESI, SourceReg = Register.ESP, SourceDisplacement = 0x8, SourceIndirect = true };
            XS.Set(XSRegisters.ESI, XSRegisters.ESP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            // new Mov { DestinationReg = Register.EDI, SourceReg = Register.ESP, SourceDisplacement = 0xC, SourceIndirect = true };

            // new Mov { DestinationReg = Register.ECX, SourceReg = Register.EAX };
            XS.Set(XSRegisters.ECX, XSRegisters.EAX);
            //new Shr { DestinationReg = Register.ECX, SourceRef = "0x2" };
            XS.ShiftRight(XSRegisters.ECX, 0x2);
            //new Literal("rep movsd");
            XS.LiteralCode("rep movsd");
            // new Mov { DestinationReg = Register.ECX, SourceReg = Register.EAX };
            XS.Set(XSRegisters.ECX, XSRegisters.EAX);
            //new And { DestinationReg = Register.ECX, SourceRef = "0x3" };
            XS.And(XSRegisters.EAX, 0x3);
            //new Literal("rep movsb");
            XS.LiteralCode("rep movsb");

            // Return
            //new Ret { Offset = 0xC };
            XS.LiteralCode("ret 0xC");
        }

        //[NoException]
        // [Assembly(false)]
        public static unsafe void FastClear(uint Address, uint Length)
        {
            //new Mov { DestinationReg = Register.EBX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EBX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            //new Mov { DestinationReg = Register.EDI, SourceReg = Register.ESP, SourceDisplacement = 0x8, SourceIndirect = true };
            XS.Set(XSRegisters.EDI, XSRegisters.ESP, sourceDisplacement: 0x8, sourceIsIndirect: true);

            //new Xor { DestinationReg = Register.EAX, SourceReg = Register.EAX };
            XS.Xor(XSRegisters.EAX, XSRegisters.EAX);
            //new Mov { DestinationReg = Register.ECX, SourceReg = Register.EBX };
            XS.Set(XSRegisters.ECX, XSRegisters.EBX);
            //new Shr { DestinationReg = Register.ECX, SourceRef = "0x2" };
            XS.ShiftRight(XSRegisters.ECX, 0x2);
            //new Literal("rep stosd");// Copy EAX to EDI
            XS.LiteralCode("rep stosd");
            //new Mov { DestinationReg = Register.ECX, SourceReg = Register.EBX };
            XS.Set(XSRegisters.ECX, XSRegisters.EBX);
            // new And { DestinationReg = Register.ECX, SourceRef = "0x3" };// Modulo by 4
            XS.And(XSRegisters.ECX, 0x3);
            //new Literal("rep stosb");
            XS.LiteralCode("rep stosb");

            // Return
            //new Ret { Offset = 0x8 };
            XS.LiteralCode("ret 0x8");
        }
    }
}
