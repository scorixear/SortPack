using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Impractical;

public class ExchangeSort : SortAlgorithm
{
    public ExchangeSort() { }
    public ExchangeSort(IStatisticCounter statisticCounter) : base(statisticCounter) { }
    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        for (int i = 0; i < collection.Count - 1; i++)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            for (int j = collection.Count - 1; j > i; j--)
            {
                T a = collection[j];
                T b = collection[i];
                StatisticCounter?.IncrementReadOperations(2);
                if (LessThan(a, b))
                {
                    Swap(collection, j, a, i, b);
                }
            }
        }
        return collection;
    }
}
