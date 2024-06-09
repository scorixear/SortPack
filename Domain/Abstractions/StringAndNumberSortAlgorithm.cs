using SortPack.Domain.Interfaces;

namespace SortPack.Domain.Abstractions
{
    public abstract class StringAndNumberSortAlgorithm : NumberSortAlgorithm, IStringSortAlgorithm
    {
        protected StringAndNumberSortAlgorithm()
        {
        }
        protected StringAndNumberSortAlgorithm(IStatisticCounter statisticCounter) : base(statisticCounter)
        {
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
