/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Stream
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.CPU.x86.Threading
{
    public abstract class Stream
    {
        public readonly string FileName;
        public readonly int FileSize;

        public Stream(string aFileName, int aSize)
        {
            FileName = aFileName;
            FileSize = aSize;
        }

        public abstract bool CanSeek();
        public abstract int Position();
        public abstract bool CanRead();
        public abstract bool CanWrite();

        public abstract unsafe int Write(byte* aBuffer, int aCount);
        public abstract unsafe int Read(byte* aBuffer, int aCount);

        public abstract int Write(byte[] aBuffer, int aCount);
        public abstract int Read(byte[] aBuffer, int aCount);
        public abstract int Seek(int val, SEEK pos);

        public abstract bool Close();
    }

    public enum SEEK : int
    {
        SEEK_FROM_ORIGIN = 0,
        SEEK_FROM_CURRENT = 1,
        SEEK_FROM_END = 2,
    }
}
