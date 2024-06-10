using SortPack.Domain;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.UnitTests;

public abstract class RecursiveNumberSortAlgorithmTestBase<T> : RecursiveNumberSortAlgorithmTestBaseNoSetup<T> where T : IRecursiveNumberSortAlgorithm
{
    [SetUp]
    public void SetUp()
    {
        StatisticCounter = new StatisticCounter();
        Sut = (T)typeof(T).GetConstructors()[1].Invoke([StatisticCounter]);
    }
}
