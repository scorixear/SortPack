﻿using SortPack.Domain;
using SortPack.Domain.Interfaces;

namespace SortPack.Infrastructure.UnitTests;

public abstract class SortAlgorithmTestBase<T> : SortAlgorithmTestBaseNoSetup<T> where T : ISortAlgorithm
{
    [SetUp]
    public void SetUp()
    {
        StatisticCounter = new StatisticCounter();
        Sut = (T)typeof(T).GetConstructors()[1].Invoke([StatisticCounter]);
    }
}
