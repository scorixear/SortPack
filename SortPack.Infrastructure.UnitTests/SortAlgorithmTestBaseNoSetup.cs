using FluentAssertions;
using SortPack.Domain.Interfaces;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests;

public abstract class SortAlgorithmTestBaseNoSetup<T> where T : ISortAlgorithm
{
    protected T? Sut { get; set; }
    protected IStatisticCounter? StatisticCounter { get; set; }
    public abstract void SortInPlace_Uneven_WhenCalled_SortsCollection();

    public abstract void SortInPlace_Even_WhenCalled_SortsCollection();

    public abstract void SortInPlace_EmptyList_DoNothing();

    [Test, CancelOnInconclusive]
    [TestCase(1_000, TestName = "1-000")]
    [TestCase(10_000, TestName = "10-000")]
    [TestCase(100_000, TestName = "100-000")]
    [TestCase(1_000_000, TestName = "1-000-000")]
    [TestCase(10_000_000, TestName = "10-000-000")]
    public async Task MassiveList_Test(int numberOfValues)
    {
        try
        {
            List<int> expected = [];
            for (int i = 0; i < numberOfValues; i++)
            {
                expected.Add(i);
            }
            int[] shuffled = [.. expected];
            Random.Shared.Shuffle(shuffled);

            await TimeoutHandler.HandleActionWithoutCancellationToken(3000, () =>
            {
                Sut.SortInPlace(shuffled);
            });

            foreach ((int actual, int expect) in shuffled.Zip(expected))
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
