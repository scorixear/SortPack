using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Extensions;

public static class SortExtension
{
    public static IList<T> SortWith<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        return sortAlgorithm.Sort(collection, cancellationToken);
    }

    public static async Task<IList<T>> SortWithAsync<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm, CancellationToken? cancellationToken = null) where T : IComparable<T>
    {
        return await sortAlgorithm.SortAsync(collection, cancellationToken);
    }
}
