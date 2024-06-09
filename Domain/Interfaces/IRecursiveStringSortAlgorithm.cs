namespace SortPack.Domain.Interfaces
{
    public interface IRecursiveStringSortAlgorithm : IStringSortAlgorithm
    {
        IList<string> RecursiveSort(IList<string> collection, CancellationToken? cancellationToken = null);
        Task<IList<string>> RecursiveSortAsync(IList<string> collection, CancellationToken? cancellationToken = null);
        IList<string> RecursiveSortInPlace(IList<string> collection, CancellationToken? cancellationToken = null);
        Task<IList<string>> RecursiveSortInPlaceAsync(IList<string> collection, CancellationToken? cancellationToken = null);
    }
}
