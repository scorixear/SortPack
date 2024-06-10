using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;
using System.Numerics;

namespace SortPack.Infrastructure.NonComparison;

public class BucketSort<TS> : NumberSortAlgorithm where TS : SortAlgorithm
{
    private readonly TS _sortAlgorithm;
    public BucketSort()
    {
        _sortAlgorithm = (TS)Activator.CreateInstance(typeof(TS), new object[] { })!;
    }

    public BucketSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
        _sortAlgorithm = (TS)Activator.CreateInstance(typeof(TS), new object[] { statisticCounter })!;
    }

    public IList<T> Sort<T>(IList<T> collection, int bucketSize) where T : IComparable<T>, IDivisionOperators<T, T, T>, IAdditionOperators<T, T, T>
    {
        if (typeof(T) != typeof(byte)
            && typeof(T) != typeof(sbyte)
            && typeof(T) != typeof(short)
            && typeof(T) != typeof(ushort)
            && typeof(T) != typeof(int))
        {
            throw new NotSupportedException(
                "Type not supported. Array indices must be of type byte, sbyte, short, ushort or int.");
        }

        List<T> result = [.. collection];
        return SortInPlace(result, bucketSize);
    }

    public Task<IList<T>> SortAsync<T>(IList<T> collection, int bucketSize) where T : IComparable<T>, IDivisionOperators<T, T, T>, IAdditionOperators<T, T, T>
    {
        return Task.Run(() => Sort(collection, bucketSize));
    }

    public Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection, int bucketSize) where T : IComparable<T>, IDivisionOperators<T, T, T>, IAdditionOperators<T, T, T>
    {
        return Task.Run(() => SortInPlace(collection, bucketSize));
    }

    public override IList<T> SortInPlace<T>(IList<T> collection)
    {
        return SortInPlace(collection, 10);
    }

    public IList<T> SortInPlace<T>(IList<T> collection, int bucketSize) where T : IComparable<T>, IDivisionOperators<T, T, T>, IAdditionOperators<T, T, T>
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        T maxValue = collection[0];
        foreach (T value in collection)
        {
            if (Comparer<T>.Default.Compare(value, maxValue) > 0)
            {
                maxValue = value;
            }
        }
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        int numberOfBuckets = collection.Count / bucketSize;
        if (numberOfBuckets == 0)
        {
            numberOfBuckets = 1;
        }

        T x = (maxValue / (T)Convert.ChangeType(numberOfBuckets, typeof(T))) + (T)Convert.ChangeType(1, typeof(T));
        List<T>[] buckets = new List<T>[numberOfBuckets];

        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }

        foreach (T value in collection)
        {
            int bucketIndex = (int)Convert.ChangeType(value / x, typeof(int));
            buckets[bucketIndex].Add(value);
        }
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        foreach (List<T> bucket in buckets)
        {
            _sortAlgorithm.SortInPlace(bucket);
        }

        StatisticCounter?.IncrementReadOperations(_sortAlgorithm.StatisticCounter?.ReadOperations ?? 0);
        StatisticCounter?.IncrementWriteOperations(_sortAlgorithm.StatisticCounter?.WriteOperations ?? 0);
        StatisticCounter?.IncrementCompareOperations(_sortAlgorithm.StatisticCounter?.CompareOperations ?? 0);

        int index = 0;
        foreach (List<T> bucket in buckets)
        {
            foreach (T value in bucket)
            {
                collection[index++] = value;
            }
        }
        StatisticCounter?.IncrementWriteOperations((ulong)collection.Count);

        return collection;
    }
}
