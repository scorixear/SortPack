namespace SortPack.Domain.Interfaces
{
    public interface ISortInPlaceAlgorithm : ISortAlgorithm
    {
        IList<T> SortInPlace<T>(IList<T> collection) where T : IComparable<T>;

        Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection) where T : IComparable<T>;
    }
}
