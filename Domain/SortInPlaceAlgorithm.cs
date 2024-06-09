using SortPack.Domain.Interfaces;

namespace SortPack.Domain
{
    public abstract class SortInPlaceAlgorithm : SortAlgorithm, ISortInPlaceAlgorithm
    {
        protected SortInPlaceAlgorithm() { }
        protected SortInPlaceAlgorithm(IStatisticCounter statisticCounter) : base(statisticCounter) { }


        public abstract IList<T> SortInPlace<T>(IList<T> collection) where T : IComparable<T>;

        public Task<IList<T>> SortInPlaceAsync<T>(IList<T> collection) where T : IComparable<T>
        {
            return Task.Run(() => SortInPlace(collection));
        }
    }
}
