using System;

namespace BloomFilter
{
    public interface IBloomFilter<T>
    {
        void Add(T item);
        Existence Query(T item);
    }
}

