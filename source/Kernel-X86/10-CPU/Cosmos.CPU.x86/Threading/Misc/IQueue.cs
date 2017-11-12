/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          IQueue
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.CPU.x86.Threading.Misc
{
    public class IQueue<T>
    {
        T[] _items;
        int _size;
        int _capacity;

        public IQueue(int capacity = 1)
        {
            _items = new T[capacity];
            _size = 0;
            _capacity = capacity;
        }

        public void Enqueue(T item)
        {
            if (_capacity <= _size)
            {
                var _new = new T[_size + 1];
                Array.Copy(_items, _new, _size);
                _items = _new;
                _capacity++;
            }
            _items[_size++] = item;
        }

        public T Dequeue()
        {
            var res = _items[0];
            for (int i = 1; i < _size; i++)
                _items[i - 1] = _items[i];
            _size--;
            return res;
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        public void Clear()
        {
            _size = 0;
        }
    }
}
