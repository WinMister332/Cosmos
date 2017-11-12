/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Threading
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Text;
using Cosmos.CPU.x86.Threading.Misc;
namespace Cosmos.CPU.x86.Threading
{
    public class Process
    {
        public readonly uint ID;
        public readonly string Name;
        public readonly uint PageDirectory;
        public readonly IList<Thread> Threads;
        public readonly uint[] shm_mapping;
        public readonly IList<Stream> Files;

        public uint HeapCurrent;
        public uint HeapEndAddress;
        public readonly uint HeapStartAddress;

        public Process(string aName, uint aDirectory)
        {
            Name = aName;
            ID = GetNewProcessID();
            PageDirectory = aDirectory;

            Files = new IList<Stream>();
            Threads = new IList<Thread>();

            // TODO: Should be a random address
            HeapStartAddress = 0xA0000000;
            HeapCurrent = HeapStartAddress;
            HeapEndAddress = HeapStartAddress;

            shm_mapping = new uint[SHM.LIMIT_TO_PROCESS];
        }

        public void SetEnvironment()
        {
            Paging.SwitchDirectory(PageDirectory);
        }

        static uint _pid = 0;
        static uint GetNewProcessID()
        {
            return _pid++;
        }
    }
}
