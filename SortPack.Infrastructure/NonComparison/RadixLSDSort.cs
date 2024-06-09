using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace SortPack.Infrastructure.NonComparison
{
    public sealed class RadixLSDSort : NumberSortAlgorithm
    {
        public RadixLSDSort()
        {
        }
        public RadixLSDSort(IStatisticCounter statisticCounter) : base(statisticCounter)
        {
        }


        protected override IList<T> SortInPlace<T>(IList<T> collection)
        {
            int n = collection.Count;
            T[] output = new T[n];
            int maxDigits = Marshal.SizeOf<T>();
            int bucketCount = 256;

            for (int digit = 0; digit < maxDigits; digit++)
            {
                int[] count = new int[bucketCount];

                for (int i = 0; i < n; i++)
                {
                    int byteValue = GetByteValue(collection[i], digit);
                    StatisticCounter?.IncrementReadOperations();
                    count[byteValue]++;
                }

                for (int i = 1; i < bucketCount; i++)
                {
                    count[i] += count[i - 1];
                }

                for (int i = n - 1; i >= 0; i--)
                {
                    T value = collection[i];
                    StatisticCounter?.IncrementReadOperations();
                    int byteValue = GetByteValue(value, digit);
                    output[--count[byteValue]] = value;
                }

                for (int i = 0; i < n; i++)
                {
                    collection[i] = output[i];
                }
                StatisticCounter?.IncrementWriteOperations((ulong)n);
            }
            return collection;
        }
    }
}
