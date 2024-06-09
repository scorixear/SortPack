using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.DivideAndConquer;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests.Algorithms.DivideAndConquer
{
    [TestFixture]
    public class QuickSortTest : SortAlgorithmTestBase<QuickSort>
    {
        [Test]
        public override void SortInPlace_Uneven_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1 };
            var random = new Random(1234);

            // Act
            (Sut as QuickSort)?.SortInPlace(collection, random);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(2);
                StatisticCounter.WriteOperations.Should().Be(2);
                StatisticCounter.CompareOperations.Should().Be(13);

            });
        }

        [Test]
        public override void SortInPlace_Even_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1, 0 };
            var random = new Random(1234);

            // Act
            (Sut as QuickSort)?.SortInPlace(collection, random);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(4);
                StatisticCounter.WriteOperations.Should().Be(4);
                StatisticCounter.CompareOperations.Should().Be(16);

            });
        }

        [Test]
        public override void SortInPlace_EmptyList_DoNothing()
        {
            // Arrange
            var collection = new List<int>();
            var random = new Random(1234);

            // Act
            (Sut as QuickSort)?.SortInPlace(collection, random);

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
            (Sut as QuickSort)?.RecursiveSortInPlace(collection, source.Token);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(11);
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
            (Sut as QuickSort)?.RecursiveSortInPlace(collection, source.Token);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
                StatisticCounter.ReadOperations.Should().Be(19);
                StatisticCounter.WriteOperations.Should().Be(10);
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
            (Sut as QuickSort)?.RecursiveSortInPlace(collection, source.Token);

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
                    (Sut as QuickSort)?.RecursiveSortInPlace(shuffled, cancellationToken);
                });

                shuffled.Should().BeEquivalentTo(expected);
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine($"Test with {numberOfValues} timed out");
            }
        }

    }
}
