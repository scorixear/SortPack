using FluentAssertions;
using SortPack.Domain.Interfaces;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests
{
    public abstract class NumberSortAlgorithmTestBaseNoSetup<T> where T : INumberSortAlgorithm
    {
        protected T Sut { get; set; }
        protected IStatisticCounter StatisticCounter { get; set; }
        public abstract void SortInPlace_Uneven_WhenCalled_SortsCollection();

        public abstract void SortInPlace_Even_WhenCalled_SortsCollection();

        public abstract void SortInPlace_EmptyList_DoNothing();

        [Test]
        [TestCase(1000ul, TestName = "1-000")]
        [TestCase(10_000ul, TestName = "10-000")]
        [TestCase(100_000ul, TestName = "100-000")]
        [TestCase(1_000_000ul, TestName = "1-000-000")]
        [TestCase(10_000_000ul, TestName = "10-000-000")]
        public async Task MassiveList_Test(ulong numberOfValues)
        {
            try
            {
                List<ulong> expected = [];
                for (ulong i = 0; i < numberOfValues; i++)
                {
                    expected.Add(i);
                }
                List<ulong> shuffled = expected.ToList();
                shuffled.Reverse();

                await TimeoutHandler.HandleActionWithoutCancellationToken(3000, () =>
                {
                    Sut.SortInPlace(shuffled);
                });

                foreach (var (actual, expect) in shuffled.Zip(expected))
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
}
