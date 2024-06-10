using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Impractical;

public class GravitySort : NumberSortAlgorithm
{
    public GravitySort()
    {
    }

    public GravitySort(IStatisticCounter statisticCounter) : base(statisticCounter)
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
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        bool[,] array = new bool[collection.Count, Convert.ToUInt64(max - min) + 1];
        int[] beadRowCount = new int[Convert.ToUInt64(max - min) + 1];

        foreach (T value in collection)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            for (ulong j = 0; j < Convert.ToUInt64(value - min); j++)
            {
                beadRowCount[j]++;
            }
        }
        StatisticCounter?.IncrementReadOperations((ulong)collection.Count);

        // overwrite rows of array with true on the right for number of beads in each row
        for (int i = 0; i < array.GetLength(1); i++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            for (int j = array.GetLength(0) - 1; j >= array.GetLength(0) - beadRowCount[i]; j--)
            {
                array[j, i] = true;
            }
        }


        // for each column, count number of true values and insert them into the collection
        for (int i = 0; i < array.GetLength(0); i++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            ulong count = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j])
                {
                    count++;
                }
            }
            collection[i] = (T)Convert.ChangeType(count, typeof(T)) + min;
        }
        StatisticCounter?.IncrementWriteOperations((ulong)collection.Count);
        return collection;
    }
}
