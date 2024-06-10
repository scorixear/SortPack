using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Primitive;

public class InsertionSort : SortAlgorithm
{
    public InsertionSort() { }
    public InsertionSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }

    public override IList<T> SortInPlace<T>(IList<T> collection)
    {
        for (int i = 1; i < collection.Count; i++)
        {
            T key = collection[i];
            StatisticCounter?.IncrementReadOperations();
            int j = i - 1;

            while (j >= 0 && GreaterThan(collection[j], key))
            {
                StatisticCounter?.IncrementReadOperations();
                j--;
            }

            if (j + 1 == i)
            {
                continue;
            }

            // shift elements to the right
            for (int k = i; k > j + 1; k--)
            {
                collection[k] = collection[k - 1];
                StatisticCounter?.IncrementReadOperations();
                StatisticCounter?.IncrementWriteOperations();
            }

            collection[j + 1] = key;
        }
        return collection;
    }
}
