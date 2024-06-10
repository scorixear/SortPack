using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Impractical;

public class CycleSort : SortAlgorithm
{
    public CycleSort()
    {
    }
    public CycleSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }
    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        int n = collection.Count;
        for (int cycleStart = 0; cycleStart < n - 1; cycleStart++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            T item = collection[cycleStart];
            StatisticCounter?.IncrementReadOperations();
            int pos = cycleStart;
            for (int i = cycleStart + 1; i < n; i++)
            {
                if (LessThan(collection[i], item))
                {
                    pos++;
                }
            }
            StatisticCounter?.IncrementReadOperations((ulong)(n - cycleStart - 1));
            StatisticCounter?.IncrementCompareOperations((ulong)(n - cycleStart - 1));
            if (pos == cycleStart)
            {
                continue;
            }
            StatisticCounter?.IncrementReadOperations();
            while (Equal(collection[pos], item))
            {
                pos++;
                StatisticCounter?.IncrementReadOperations();
            }

            if (pos != cycleStart)
            {
                (item, collection[pos]) = (collection[pos], item);
                StatisticCounter?.IncrementWriteOperations();
                StatisticCounter?.IncrementReadOperations();
            }

            while (pos != cycleStart)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                pos = cycleStart;
                for (int i = cycleStart + 1; i < n; i++)
                {
                    if (LessThan(collection[i], item))
                    {
                        pos++;
                    }
                }
                StatisticCounter?.IncrementReadOperations((ulong)(n - cycleStart - 1));
                StatisticCounter?.IncrementCompareOperations((ulong)(n - cycleStart - 1));
                StatisticCounter?.IncrementReadOperations();
                while (Equal(collection[pos], item))
                {
                    pos++;
                    StatisticCounter?.IncrementReadOperations();
                }
                (item, collection[pos]) = (collection[pos], item);
                StatisticCounter?.IncrementWriteOperations();
                StatisticCounter?.IncrementReadOperations();
            }
        }
        return collection;
    }
}
