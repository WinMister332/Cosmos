/*
* PROJECT:          Aura Systems
* LICENSE:          BSD 3-Clause (LICENSE.md)
* PURPOSE:          IList
* PROGRAMMERS:      Aman Priyadarshi (aman.eureka@gmail.com)
* MOTIFIERS:        John Welsh (djlw78@gmail.com)
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.CPU.x86.Threading.Misc
{
    public class IList<T>
    {
        T[] _items;
        int _size;
        int _capacity;

        public IList(int capacity = 1)
        {
            _items = new T[capacity];
            _size = 0;
            _capacity = capacity;
        }

        public void Add(T item)
        {
            if (_capacity <= _size)
            {
                var _new = new T[_size + _size];
                Array.Copy(_items, _new, _size);
                _items = _new;
                _capacity += _size;
            }
            _items[_size++] = item;
        }

        public T this[int index]
        {
            get
            {
                return _items[index];
            }
            set
            {
                _items[index] = value;
            }
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
