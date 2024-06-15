﻿using FluentAssertions;
using SortPack.Domain.Interfaces;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests;

public abstract class RecursiveNumberSortAlgorithmTestBaseNoSetup<T> : NumberSortAlgorithmTestBaseNoSetup<T> where T : IRecursiveNumberSortAlgorithm
{
    public abstract void RecursiveSortInPlace_Uneven_WhenCalled_SortsCollection();
    public abstract void RecursiveSortInPlace_Even_WhenCalled_SortsCollection();

    public abstract void RecursiveSortInPlace_EmptyList_DoNothing();

    [Test, CancelOnInconclusive]
    [NonParallelizable]
    [TestCase(1_000, TestName = "1-000")]
    [TestCase(10_000, TestName = "10-000")]
    [TestCase(100_000, TestName = "100-000")]
    [TestCase(1_000_000, TestName = "1-000-000")]
    [TestCase(10_000_000, TestName = "10-000-000")]
    public async Task MassiveList_RecursiveTest(int numberOfValues)
    {
        try
        {
            List<ulong> expected = [];
            for (int i = 0; i < numberOfValues; i++)
            {
                expected.Add((ulong)i);
            }
            ulong[] shuffled = [.. expected];
            Random.Shared.Shuffle(shuffled);

            await TimeoutHandler.HandleActionWithCancellationToken(3000, (cancellationToken) =>
            {
                Sut.RecursiveSortInPlace(shuffled, cancellationToken);
            });

            foreach ((ulong actual, ulong expect) in shuffled.Zip(expected))
            {
                actual.Should().Be(expect);
            }
        }
        catch (ThreadAbortException)
        {
            Console.WriteLine($"Test with {numberOfValues} timed out");
        }
    }
}
