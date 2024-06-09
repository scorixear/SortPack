using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.Advanced;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests.Algorithms.Advanced
{
    [TestFixture]
    public class HeapSortTest : SortAlgorithmTestBase<HeapSort>
    {
        [Test]
        public override void SortInPlace_Uneven_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1 };

            // Act
            Sut.SortInPlace(collection);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(12);
                StatisticCounter.WriteOperations.Should().Be(6);
                StatisticCounter.CompareOperations.Should().Be(3);

            });
        }

        [Test]
        public override void SortInPlace_Even_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1, 0 };

            // Act
            Sut.SortInPlace(collection);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(20);
                StatisticCounter.WriteOperations.Should().Be(8);
                StatisticCounter.CompareOperations.Should().Be(6);

            });
        }

        [Test]
        public override void SortInPlace_EmptyList_DoNothing()
        {
            // Arrange
            var collection = new List<int>();

            // Act
            Sut.SortInPlace(collection);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                StatisticCounter.ReadOperations.Should().Be(0);
                StatisticCounter.WriteOperations.Should().Be(0);
                StatisticCounter.CompareOperations.Should().Be(0);
            });
        }

        [Test]
        public void RecursiveSortInPlace_Uneven_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1 };
            CancellationTokenSource source = new();

            // Act
            (Sut as HeapSort)?.RecursiveSortInPlace(collection, source.Token);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(12);
                StatisticCounter.WriteOperations.Should().Be(6);
                StatisticCounter.CompareOperations.Should().Be(3);

            });
        }

        [Test]
        public void RecursiveSortInPlace_Even_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1, 0 };
            CancellationTokenSource source = new();

            // Act
            (Sut as HeapSort)?.RecursiveSortInPlace(collection, source.Token);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(20);
                StatisticCounter.WriteOperations.Should().Be(8);
                StatisticCounter.CompareOperations.Should().Be(6);

            });
        }

        [Test]
        public void RecursiveSortInPlace_EmptyList_DoNothing()
        {
            // Arrange
            var collection = new List<int>();
            CancellationTokenSource source = new();

            // Act
            (Sut as HeapSort)?.RecursiveSortInPlace(collection, source.Token);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                StatisticCounter.ReadOperations.Should().Be(0);
                StatisticCounter.WriteOperations.Should().Be(0);
                StatisticCounter.CompareOperations.Should().Be(0);
            });
        }

        [Test]
        [NonParallelizable]
        [TestCase(1_000, TestName = "1-000")]
        [TestCase(10_000, TestName = "10-000")]
        [TestCase(100_000, TestName = "100-000")]
        [TestCase(1_000_000, TestName = "1000-000")]
        [TestCase(10_000_000, TestName = "10-000-000")]
        public async Task MassiveList_RecursiveTest(int numberOfValues)
        {
            try
            {
                List<int> expected = [];
                for (int i = 0; i < numberOfValues; i++)
                {
                    expected.Add(i);
                }
                List<int> shuffled = expected.ToList();
                shuffled.Reverse();

                await TimeoutHandler.HandleActionWithCancellationToken(3000, (cancellationToken) =>
                {
                    (Sut as HeapSort)?.RecursiveSortInPlace(shuffled, cancellationToken);
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
