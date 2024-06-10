using FluentAssertions;
using FluentAssertions.AssertMultiple;
using SortPack.Infrastructure.Joke;
using SortPack.Infrastructure.UnitTests.NUnit;

namespace SortPack.Infrastructure.UnitTests.Algorithms.Joke;

public class BogoSortTest : SortAlgorithmTestBase<BogoSort>
{
    [Test]
    public override void SortInPlace_Uneven_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1];

        // Act
        TimeoutHandler.HandleActionWithoutCancellationToken(3000, () => Sut?.SortInPlace(collection)).Wait();
        Sut?.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 1, 2, 3 });
        });
    }

    [Test]
    public override void SortInPlace_Even_WhenCalled_SortsCollection()
    {
        // Arrange
        List<int> collection = [3, 2, 1, 0];

        // Act
        TimeoutHandler.HandleActionWithoutCancellationToken(3000, () => Sut?.SortInPlace(collection)).Wait();

        // Assert
        AssertMultiple.Multiple(() =>
        {
            collection.Should().BeEquivalentTo(new List<int> { 0, 1, 2, 3 });
        });
    }

    [Test]
    public override void SortInPlace_EmptyList_DoNothing()
    {
        // Arrange
        List<int> collection = [];

        // Act
        Sut?.SortInPlace(collection);

        // Assert
        AssertMultiple.Multiple(() =>
        {
            StatisticCounter?.ReadOperations.Should().Be(0);
            StatisticCounter?.WriteOperations.Should().Be(0);
            StatisticCounter?.CompareOperations.Should().Be(0);
        });
    }
}
