using SortPack.Domain;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.UnitTests
{
    public abstract class SortInPlaceAlgorithmTestBase<T> : SortInPlaceAlgorithmTestBaseNoSetup<T> where T : ISortInPlaceAlgorithm
    {
        [SetUp]
        public void SetUp()
        {
            StatisticCounter = new StatisticCounter();
            Sut = (T)typeof(T).GetConstructors()[1].Invoke([StatisticCounter]);
        }
    }
}
