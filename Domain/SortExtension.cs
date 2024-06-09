using SortPack.Domain.Interfaces;

namespace SortPack.Domain
{
    public static class SortExtension
    {
        public static IList<T> SortWith<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm) where T : IComparable<T>
        {
            return sortAlgorithm.Sort(collection);
        }

        public static async Task<IList<T>> SortWithAsync<T>(this IList<T> collection, ISortAlgorithm sortAlgorithm) where T : IComparable<T>
        {
            return await sortAlgorithm.SortAsync(collection);
        }
    }
}
