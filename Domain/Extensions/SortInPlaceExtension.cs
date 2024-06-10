using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Extensions;

public static class SortInPlaceExtension
{
    public static IList<T> SortInPlaceWith<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm) where T : IComparable<T>
    {
        return sortAlgorithm.SortInPlace(collection);
    }

    public static async Task<IList<T>> SortInPlaceWithAsync<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm) where T : IComparable<T>
    {
        return await sortAlgorithm.SortInPlaceAsync(collection);
    }
}
