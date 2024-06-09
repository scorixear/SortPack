namespace SortPack.Domain.Interfaces
{
    public interface IRecursiveSortAlgorithm
    {
        IList<T> RecursiveSort<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
        Task<IList<T>> RecursiveSortAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
        IList<T> RecursiveSortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
        Task<IList<T>> RecursiveSortInPlaceAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
    }
}
