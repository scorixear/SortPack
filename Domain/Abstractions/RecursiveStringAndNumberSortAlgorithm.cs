using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Abstractions
{
    public abstract class RecursiveStringAndNumberSortAlgorithm : RecursiveNumberSortAlgorithm, IStringSortAlgorithm, IRecursiveStringSortAlgorithm
    {
        protected RecursiveStringAndNumberSortAlgorithm() { }
        protected RecursiveStringAndNumberSortAlgorithm(IStatisticCounter statisticCounter) : base(statisticCounter) { }

        public IList<string> RecursiveSort(IList<string> collection, CancellationToken? cancellationToken = null)
        {
            List<string> result = [.. collection];
            return RecursiveSortInPlace(result, cancellationToken);
        }
        public Task<IList<string>> RecursiveSortAsync(IList<string> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSort(collection, cancellationToken));
        }
        public abstract IList<string> RecursiveSortInPlace(IList<string> collection, CancellationToken? cancellationToken = null);
        public Task<IList<string>> RecursiveSortInPlaceAsync(IList<string> collection, CancellationToken? cancellationToken = null)
        {
            return Task.Run(() => RecursiveSortInPlace(collection, cancellationToken));
        }

        public IList<string> Sort(IList<string> collection)
        {
            List<string> result = [.. collection];
            return SortInPlace(result);
        }

        public Task<IList<string>> SortAsync(IList<string> collection)
        {
            return Task.Run(() => Sort(collection));
        }

        public abstract IList<string> SortInPlace(IList<string> collection);

        public Task<IList<string>> SortInPlaceAsync(IList<string> collection)
        {
            return Task.Run(() => SortInPlace(collection));
        }
    }
}
