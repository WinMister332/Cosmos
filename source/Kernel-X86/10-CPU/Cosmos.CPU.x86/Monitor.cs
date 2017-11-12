/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Monitor
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.CPU.x86
{
    public static class Monitor
    {
        internal static void AcquireLock(ref uint aLock)
        {
            while (Native.AtomicExchange(ref aLock, 1) != 0)
                Threading.Task.Switch();
        }

        internal static void ReleaseLock(ref uint aLock)
        {
            aLock = 0;
        }
    }
}
