using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Abstractions
{
    public abstract class RecursiveSortAlgorithm : SortAlgorithm, IRecursiveSortAlgorithm
    {
        protected RecursiveSortAlgorithm() { }
        protected RecursiveSortAlgorithm(IStatisticCounter statisticCounter) : base(statisticCounter) { }

        public IList<T> RecursiveSort<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>
        {
            IList<T> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public abstract IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
        public Task<IList<T>> RecursiveSortAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public Task<IList<T>> RecursiveSortInPlaceAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }

    }
}
