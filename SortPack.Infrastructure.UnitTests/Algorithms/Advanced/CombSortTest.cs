using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Domain;
using SortPack.Infrastructure.Advanced;

namespace SortPack.Infrastructure.UnitTests.Algorithms.Advanced
{
    [TestFixture]
    public class CombSortTest : SortAlgorithmTestBaseNoSetup<CombSort>
    {
        [SetUp]
        public void SetUp()
        {
            StatisticCounter = new StatisticCounter();
            Sut = new CombSort(StatisticCounter);
        }

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
                StatisticCounter.ReadOperations.Should().Be(8);
                StatisticCounter.WriteOperations.Should().Be(2);
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
                StatisticCounter.ReadOperations.Should().Be(22);
                StatisticCounter.WriteOperations.Should().Be(4);
                StatisticCounter.CompareOperations.Should().Be(9);
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
    }
}
