using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Joke;

public class BogoSort : SortAlgorithm
{
    public BogoSort() : base()
    {
    }

    public BogoSort(IStatisticCounter statisticCounter) : base(statisticCounter)
    {
    }

    public IList<T> Sort<T>(IList<T> collection, Random random, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        return SortInPlace(new List<T>(collection), random, cancellationToken);
    }

    public Task<IList<T>> SortAsync<T>(IList<T> collection, Random random, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        return Task.Run(() => Sort(collection, random, cancellationToken), cancellationToken ?? CancellationToken.None);
    }

    public IList<T> SortInPlace<T>(IList<T> collection, Random random, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        if (collection.Count < 2)
        {
            return collection;
        }

        while (!IsSorted(collection))
        {
            cancellationToken?.ThrowIfCancellationRequested();
            Shuffle(collection, random);
        }

        return collection;
    }

    public Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection, Random random, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        return Task.Run(() => SortInPlace(collection, random, cancellationToken), cancellationToken ?? CancellationToken.None);
    }

    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        return SortInPlace(collection, new Random(), cancellationToken);
    }

    private bool IsSorted<T>(IList<T> collection) where T : IComparable<T>
    {
        T prev = collection[0];
        StatisticCounter?.IncrementReadOperations();
        for (int i = 1; i < collection.Count; i++)
        {
            T next = collection[i];
            StatisticCounter?.IncrementReadOperations();
            StatisticCounter?.IncrementCompareOperations();
            if (LessThan(next, prev))
            {
                return false;
            }
            prev = next;
        }

        return true;
    }

    private void Shuffle<T>(IList<T> collection, Random random) where T : IComparable<T>
    {
        for (int i = 0; i < collection.Count; i++)
        {
            int j = random.Next(i, collection.Count);
            Swap(collection, i, j);
        }
    }
}
