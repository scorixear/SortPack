using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.DivideAndConquer;

namespace SortPack.Infrastructure.UnitTests.Algorithms.DivideAndConquer
{
    [TestFixture]
    public class QuickSortTest : RecursiveSortAlgorithmTestBase<QuickSort>
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
        public override void RecursiveSortInPlace_Uneven_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1 };
            CancellationTokenSource source = new();

            // Act
            Sut.RecursiveSortInPlace(collection, source.Token);

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
        public override void RecursiveSortInPlace_Even_WhenCalled_SortsCollection()
        {
            // Arrange
            var collection = new List<int> { 3, 2, 1, 0 };
            CancellationTokenSource source = new();

            // Act
            Sut.RecursiveSortInPlace(collection, source.Token);

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
        public override void RecursiveSortInPlace_EmptyList_DoNothing()
        {
            // Arrange
            var collection = new List<int>();
            CancellationTokenSource source = new();

            // Act
            Sut.RecursiveSortInPlace(collection, source.Token);

            // Assert
            AssertMultiple.Multiple(() =>
            {
                StatisticCounter.ReadOperations.Should().Be(0);
                StatisticCounter.WriteOperations.Should().Be(0);
                StatisticCounter.CompareOperations.Should().Be(0);
            });
        }
    }
}
