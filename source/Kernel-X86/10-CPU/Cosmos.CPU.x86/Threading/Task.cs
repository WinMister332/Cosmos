/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Task
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;
using XSharp;
namespace Cosmos.CPU.x86.Threading
{
    public static class Task
    {
        //[NoException]
        //[Assembly(false)]
        public static void Switch()
        {

            //new Literal("int 0x75");
            XS.LiteralCode("int 0x75");
            //new Ret { Offset = 0x0 };
            XS.LiteralCode("ret 0x0");
        }

        //[NoException]
        // [Assembly(false)]
        //[Plug("__ISR_Handler_75", Architecture.x86)]
        private static void Handler()
        {
            // Clear Interrupts
            // new Cli();
            XS.LiteralCode("cli");

            // Push all the Registers
            // new Pushad();
            XS.LiteralCode("pushad");

            // Push ESP
            //new Push { DestinationReg = Register.ESP };
            XS.Push(XSRegisters.ESP);
            //new Call { DestinationRef = "__Switch_Task__", IsLabel = true };
            XS.Call("__Switch_Task__");

            // Get New task ESP
            //  new Mov { DestinationReg = Register.ESP, SourceReg = Register.EAX };
            XS.Set(XSRegisters.ESP, XSRegisters.EAX);
            // Load Registers
            // new Popad();
            XS.LiteralCode("popad");

            // Enable Interrupts
            // new Sti();
            XS.LiteralCode("sti");

            // Return
            // new Iret();
            XS.LiteralCode("iret");
            //XS.Return();
        }
    }
}
