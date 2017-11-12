/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          Garbage Collector
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.CPU.x86
{
    public static unsafe class GC
    {
        static uint mMemoryUsage;

        static int mAllocatedObjectCount;

        static uint[] mAllocatedObjects;
        static uint[] mAllocatedObjectSize;

        public static uint MemoryUsage
        {
            get
            {
                return mMemoryUsage;
            }
        }

        public static void Init(uint aMaximumObjectCount = 1024)
        {
            mMemoryUsage = 0;
            mAllocatedObjectCount = 0;

            mAllocatedObjects = new uint[aMaximumObjectCount];
            mAllocatedObjectSize = new uint[aMaximumObjectCount];
        }

        public static bool Notify(uint aAddress, uint aLength)
        {
            if (mAllocatedObjectCount == mAllocatedObjects.Length)
                return false;
            // add the object to pool and update counter
            int index = mAllocatedObjectCount;
            mAllocatedObjects[index] = aAddress;
            mAllocatedObjectSize[index] = aLength;
            mAllocatedObjectCount = index + 1;
            mMemoryUsage += aLength;

            return true;
        }

        public static void Collect()
        {
            //needs taking
        }

        public static void Dump()
        {

        }

        private static void MarkObject(uint Address)
        {
            if (Address == 0) return;
            int index = BinarySearch(Address);
            // No such object found.
            if (index == -1) return;

            // mark if not marked
            if ((mAllocatedObjectSize[index] & (1U << 31)) != 0) return;
            mAllocatedObjectSize[index] |= 1U << 31;

            var data = (uint*)Address;
            uint flag = data[1];

            // Check object flag.
            if ((flag & 0x03) != 0x01)
            {
                if ((flag & 0x03) == 0x03)
                {
                    int lastIndex = (int)data[2] + 4;
                    for (int i = 4; i < lastIndex; i++)
                        MarkObject(data[i]);
                }
            }
            return;
        }

        private static int BinarySearch(uint Address)
        {
            int left = 0, right = mAllocatedObjectCount - 1;
            while (left <= right)
            {
                int mid = (left + right) >> 1;
                uint found = mAllocatedObjects[mid];

                if (found == Address)
                    return mid;

                if (mAllocatedObjects[mid] > Address)
                    right = mid - 1;
                else
                    left = mid + 1;
            }

            return -1;
        }

        private static void SortObjects()
        {
            int count = mAllocatedObjectCount;
            for (int i = 1; i < count; i++)
            {
                int j = i - 1;
                uint address = mAllocatedObjects[i], length = mAllocatedObjectSize[i];
                while (j >= 0 && mAllocatedObjects[j] > address)
                    j--;///almost a crawl
                j++;
                int k = i - 1;
                while (k >= j)
                {
                    mAllocatedObjects[k + 1] = mAllocatedObjects[k];
                    mAllocatedObjects[k + 1] = mAllocatedObjectSize[k];
                    k--;
                    mAllocatedObjects[j] = address;
                    mAllocatedObjectSize[j] = length;
                }
            }
        }
    }
}
