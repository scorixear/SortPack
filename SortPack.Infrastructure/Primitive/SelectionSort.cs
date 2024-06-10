using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive;

public class SelectionSort : SortAlgorithm
{
    public SelectionSort()
    {
    }

    public SelectionSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        for (int i = 0; i < collection.Count - 1; i++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            int minIndex = i;
            T key = collection[minIndex];
            StatisticCounter?.IncrementReadOperations();

            for (int j = i + 1; j < collection.Count; j++)
            {
                T other = collection[j];
                StatisticCounter?.IncrementReadOperations();
                if (LessThan(other, key))
                {
                    minIndex = j;
                    key = other;
                }
            }

            if (minIndex != i)
            {
                StatisticCounter?.IncrementReadOperations();
                Swap(collection, i, collection[i], minIndex, key);
            }
        }
        return collection;
    }
}
