namespace SortPack.Domain.Interfaces;

public interface ISortAlgorithm
{
    IList<T> Sort<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;

    Task<IList<T>> SortAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;

    IList<T> SortInPlace<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;

    Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection, CancellationToken? cancellationToken = null) where T : IComparable<T>;
}
