using System;
using NUnit.Framework;
using BloomFilter;

namespace BloomFilterTest
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            var hashes = new Func<string, uint>[]{StringHashFirstHalf,StringHashLastHalf};
            var size = Convert.ToInt32(Math.Pow(2, 16));

            IBloomFilter<string> bloomFilter = new SimpleBloomFilter<string>(size, hashes);
            bloomFilter.Add("haochi");
            bloomFilter.Add("chen");

            Assert.AreEqual(bloomFilter.Query("haochi"), Existence.MAYBE);
            Assert.AreEqual(bloomFilter.Query("chen"), Existence.MAYBE);
            Assert.AreEqual(bloomFilter.Query("orlando"), Existence.NO);
            Assert.AreEqual(bloomFilter.Query("bloom"), Existence.NO);
        }

        private uint StringHashFirstHalf(string item)
        {
            var hash = (ushort) (item.GetHashCode() >> 16);
            return hash;
        }

        private uint StringHashLastHalf(string item){
            var hash = (ushort) (item.GetHashCode() & 0xffff);
            return hash;
        }
    }
}

