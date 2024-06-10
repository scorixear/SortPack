using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Runtime.InteropServices;

namespace SortPack.Infrastructure.NonComparison;

// ReSharper disable once InconsistentNaming
public sealed class RadixLSDSort : NumberSortAlgorithm
{
    public RadixLSDSort()
    {
    }
    public RadixLSDSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }


    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        int n = collection.Count;
        T[] output = new T[n];
        int maxDigits = Marshal.SizeOf<T>();
        int bucketCount = 256;

        for (int digit = 0; digit < maxDigits; digit++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            int[] count = new int[bucketCount];

            for (int i = 0; i < n; i++)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                byte byteValue = GetByteValue(collection[i], digit);
                count[byteValue]++;
            }
            StatisticCounter?.IncrementReadOperations((ulong)n);

            for (int i = 1; i < bucketCount; i++)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                T value = collection[i];

                byte byteValue = GetByteValue(value, digit);
                output[--count[byteValue]] = value;
            }
            StatisticCounter?.IncrementReadOperations((ulong)n);

            for (int i = 0; i < n; i++)
            {
                collection[i] = output[i];
            }
            StatisticCounter?.IncrementWriteOperations((ulong)n);
        }
        return collection;
    }
}
