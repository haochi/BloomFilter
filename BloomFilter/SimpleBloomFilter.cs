using System;
using System.Collections;
using System.Collections.Generic;

namespace BloomFilter
{
    public class SimpleBloomFilter<T> : IBloomFilter<T>
    {
        private IEnumerable<Func<T, uint>> Hashes;
        private BitArray Bits;

        public SimpleBloomFilter(int size, IEnumerable<Func<T, uint>> hashes)
        {
            Hashes = hashes;
            Bits = new BitArray(size);
        }

        public void Add(T item)
        {
            foreach (var hash in Hashes)
            {
                var position = hash(item);
                Bits.Set((int)position, true);
            }
        }

        public Existence Query(T item)
        {
            foreach (var hash in Hashes)
            {
                var position = hash(item);
                if (!Bits.Get((int) position))
                {
                    return Existence.NO;
                }
            }
            return Existence.MAYBE;
        }
    }
}

