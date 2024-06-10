using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.NonComparison;

public class CountingSort : NumberSortAlgorithm
{
    public CountingSort()
    {
    }

    public CountingSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        T max = collection[0];
        T min = max;
        StatisticCounter?.IncrementReadOperations();
        for (int i = 1; i < collection.Count; i++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            T value = collection[i];
            if (Comparer<T>.Default.Compare(value, max) > 0)
            {
                max = value;
            }
            if (Comparer<T>.Default.Compare(value, min) < 0)
            {
                min = value;
            }
        }
        StatisticCounter?.IncrementCompareOperations(((ulong)collection.Count * 2) - 2);
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count - 1);

        ulong range = Convert.ToUInt64(max - min) + 1;
        int[] count = new int[range];
        foreach (T value in collection)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            count[Convert.ToUInt64(value - min)]++;
        }
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        for (int i = 1; i < count.Length; i++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            count[i] += count[i - 1];
        }
        T[] result = new T[collection.Count];
        for (int i = collection.Count - 1; i >= 0; i--)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            T value = collection[i];
            result[--count[Convert.ToUInt64(value - min)]] = value;
        }
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        for (int i = 0; i < collection.Count; i++)
        {
            collection[i] = result[i];
        }
        StatisticCounter?.IncrementWriteOperations((ulong)collection.Count);

        return collection;
    }
}
