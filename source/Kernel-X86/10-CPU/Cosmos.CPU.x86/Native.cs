/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Navive
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using XSharp;
namespace Cosmos.CPU.x86
{
    public static class Native
    {
        /// <summary>
        /// Clear Interrupts
        /// </summary>
       // [NoException]
        //[Assembly(false)]
        public static void Cli()
        {
            //new Cli();
            XS.LiteralCode("cli");
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");
        }

        /// <summary>
        /// Enable Interrupts
        /// </summary>
        //  [NoException]
        // [Assembly(false)]
        public static void Sti()
        {
            // new Sti();
            XS.LiteralCode("sti");
            // new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");
        }

        /// <summary>
        /// Halt The Processor
        /// </summary>
       // [NoException]
        //[Assembly(false)]
        public static void Hlt()
        {
            //new Literal("hlt");
            XS.LiteralCode("hlt");
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");
        }

        // [NoException]
        //[Assembly(false)]
        public static uint GetDataOffset(this string aStr)
        {
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            //new Add { DestinationReg = Register.EAX, SourceRef = "0x10" };
            XS.Set(XSRegisters.EAX, "0x10");
            //new Ret { Offset = 0x4 };
            XS.LiteralCode("ret 0x4");

            return 0;
        }

        // [NoException]
        // [Assembly(false)]
        public static uint GetDataOffset(this Array aArray)
        {
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            //new Add { DestinationReg = Register.EAX, SourceRef = "0x10" };
            XS.Set(XSRegisters.EAX, "0x10");
            //new Ret { Offset = 0x4 };
            XS.LiteralCode("ret 0x4");

            return 0;
        }

        /// <summary>
        /// Get Invokable method address from Action Delegate
        /// </summary>
        /// <param name="aDelegate"></param>
        /// <returns></returns>
        // [NoException]
        // [Assembly(false)]
        public static uint InvokableAddress(this Delegate aDelegate)
        {
            // Compiler.cs : ProcessDelegate(MethodBase xMethod);
            // [aDelegate + 0xC] := Address Field
            // new Mov
            //{
            //    DestinationReg = Register.EAX,
            ///    SourceReg = Register.ESP,
            //   SourceDisplacement = 0x4,
            //   SourceIndirect = true
            // };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);

            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.EAX, SourceDisplacement = 0xC, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.EAX, sourceDisplacement: 0xC, sourceIsIndirect: true);
            //new Ret { Offset = 0x4 };
            XS.LiteralCode("ret 0x4");

            return 0;
        }

        //   [NoException]
        //  [Assembly(false)]
        public static uint GetStackPointer()
        {
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP);
            // new Add { DestinationReg = Register.EAX, SourceRef = "0x4" };
            XS.Add(XSRegisters.EAX, 0x4);
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");

            return 0;
        }

        /// <summary>
        /// End of kernel offset
        /// </summary>
        /// <returns></returns>
        // [NoException]
        //  [Assembly(false)]
        public static uint EndOfKernel()
        {
            // Just put Compiler_End location into return value
            //new Mov { DestinationReg = Register.EAX, SourceRef = Helper.Compiler_End };
            XS.Set(XSRegisters.EAX, "Compiler_End");
            // new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");

            return 0; // just for c# error
        }

        //[NoException]
        // [Assembly(false)]
        public static uint GlobalVarStart()
        {
            //new Mov { DestinationReg = Register.EAX, SourceRef = Helper.GC_Root_Start };
            XS.Set(XSRegisters.EAX, "__GCRS__");
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");

            return 0;
        }

        //[NoException]
        //[Assembly(false)]
        public static uint GlobalVarEnd()
        {
            //new Mov { DestinationReg = Register.EAX, SourceRef = Helper.GC_Root_End };
            XS.Set(XSRegisters.EAX, "__GCRE__");
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");

            return 0;
        }

        // [NoException]
        // [Assembly(false)]
        public static uint CR2Register()
        {
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.CR2 };
            XS.Set(XSRegisters.EAX, XSRegisters.CR2);
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");

            return 0;
        }

        // [NoException]
        // [Assembly(false)]
        public static uint AtomicExchange(ref uint aLock, uint val)
        {
            //new Mov { DestinationReg = Register.EBX, SourceReg = Register.ESP, SourceDisplacement = 0x8, SourceIndirect = true };
            XS.Set(XSRegisters.EBX, XSRegisters.ESP, sourceDisplacement: 0x8, sourceIsIndirect: true);
            //new Mov { DestinationReg = Register.EAX, SourceReg = Register.ESP, SourceDisplacement = 0x4, SourceIndirect = true };
            XS.Set(XSRegisters.EAX, XSRegisters.ESP, sourceDisplacement: 0x4, sourceIsIndirect: true);
            //new Literal("xchg dword [EBX], EAX");
            XS.LiteralCode("xchg dword [EBX], EAX");
            //new Ret { Offset = 0x8 };
            XS.LiteralCode("ret 0x8");

            return 0;
        }
    }
}
