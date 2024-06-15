using SortPack.Domain.Abstractions;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.Advanced.SortingNetworks;

public class SortingNetwork : SortAlgorithm
{
    public IList<IList<(ComparisonType comparisonType, int indexA, int indexB)>> ComparisonChain { get; }
    public SortingNetwork(IList<IList<(ComparisonType comparisonType, int indexA, int indexB)>> comparisonChain)
    {
        ComparisonChain = comparisonChain;
    }

    public SortingNetwork(IList<IList<(ComparisonType comparisonType, int indexA, int indexB)>> comparisonChain, IStatisticCounter statisticCounter) : base(statisticCounter)
    {
        ComparisonChain = comparisonChain;
    }


    public override IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null)
    {
        foreach (IList<(ComparisonType comparisonType, int indexA, int indexB)> comparisonBlock in ComparisonChain)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            foreach ((ComparisonType type, int indexA, int indexB) in comparisonBlock)
            {
                if (indexA < 0 || indexA >= collection.Count || indexB < 0 || indexB >= collection.Count)
                {
                    continue;
                }
                T valueA = collection[indexA];
                T valueB = collection[indexB];
                StatisticCounter?.IncrementReadOperations(2);
                switch (type)
                {
                    case ComparisonType.Less:
                        if (LessThan(valueA, valueB))
                        {
                            Swap(collection, indexA, valueA, indexB, valueB);
                        }
                        break;
                    case ComparisonType.LessOrEqual:
                        if (LessThanOrEqual(valueA, valueB))
                        {
                            Swap(collection, indexA, valueA, indexB, valueB);
                        }
                        break;
                    default:
                    case ComparisonType.Greater:
                        if (GreaterThan(valueA, valueB))
                        {
                            Swap(collection, indexA, valueA, indexB, valueB);
                        }
                        break;
                    case ComparisonType.GreaterOrEqual:
                        if (GreaterThanOrEqual(valueA, valueB))
                        {
                            Swap(collection, indexA, valueA, indexB, valueB);
                        }
                        break;
                }
            }
        }
        return collection;
    }

    public async Task<IList<T>> SortWithTasksAsync<T>(IList<T> collection, CancellationToken? cancellationToken) where T : IComparable<T>
    {
        return await SortInPlaceWithTasksAsync([.. collection], cancellationToken);
    }

    public async Task<IList<T>> SortInPlaceWithTasksAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        foreach (IList<(ComparisonType comparisonType, int indexA, int indexB)> comparisonBlock in ComparisonChain)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            List<Task> compareTasks = [];
            foreach ((ComparisonType type, int indexA, int indexB) in comparisonBlock)
            {
                cancellationToken?.ThrowIfCancellationRequested();
                if (indexA < 0 || indexA >= collection.Count || indexB < 0 || indexB >= collection.Count)
                {
                    continue;
                }
                StatisticCounter?.IncrementReadOperations(2);
                compareTasks.Add(Task.Run(() =>
                {
                    T valueA = collection[indexA];
                    T valueB = collection[indexB];
                    switch (type)
                    {
                        case ComparisonType.Less:
                            if (LessThan(valueA, valueB))
                            {
                                Swap(collection, indexA, valueA, indexB, valueB);
                            }
                            break;
                        case ComparisonType.LessOrEqual:
                            if (LessThanOrEqual(valueA, valueB))
                            {
                                Swap(collection, indexA, valueA, indexB, valueB);
                            }
                            break;
                        default:
                        case ComparisonType.Greater:
                            if (GreaterThan(valueA, valueB))
                            {
                                Swap(collection, indexA, valueA, indexB, valueB);
                            }
                            break;
                        case ComparisonType.GreaterOrEqual:
                            if (GreaterThanOrEqual(valueA, valueB))
                            {
                                Swap(collection, indexA, valueA, indexB, valueB);
                            }
                            break;
                    }
                }));
            }
            await Task.WhenAll(compareTasks);
        }
        return collection;
    }
}
