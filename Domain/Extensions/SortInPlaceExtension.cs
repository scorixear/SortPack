using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Extensions
{
    public static class SortInPlaceExtension
    {
        public static void SortInPlaceWith<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm) where T : IComparable<T>
        {
            sortAlgorithm.SortInPlace(collection);
        }

        public static async Task SortInPlaceWithAsync<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm) where T : IComparable<T>
        {
            await sortAlgorithm.SortInPlaceAsync(collection);
        }
    }
}
