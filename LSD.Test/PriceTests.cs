using FluentAssertions;
using Xunit;

namespace Lsd.Test
{
    public class PriceTests
    {
        [Theory]
        [InlineData(1, 1, 1, 21, 253)]
        [InlineData(1, 19, 11, 39, 479)]
        public void Constructor(int pounds, int shillings, double pence, int expectedTotalShillings, int expectedTotalPennies)
        {
            var price = new Price(pounds, shillings, pence);

            price.Pounds.Should().Be(pounds);
            price.Shillings.Should().Be(shillings);
            price.Pence.Should().Be(pence);
            price.TotalShillings.Should().Be(expectedTotalShillings);
            price.TotalPennies.Should().Be(expectedTotalPennies);
        }

        [Theory]
        [InlineData(/*guineas*/null, /*pounds*/null, /*sovreigns*/null, /*halfSovreigns*/null, /*crowns*/null, /*halfCrowns*/null, /*florins*/null, /*shillings*/null,
                    /*sixpences*/null, /*threepences*/null, /*pennies*/null, /*halfpennies*/null, /*farthings*/null, /*expectedPounds*/0, /*expectedShillings*/0, /*expectedPence*/0)]
        [InlineData(/*guineas*/null, /*pounds*/null, /*sovreigns*/null, /*halfSovreigns*/null, /*crowns*/null, /*halfCrowns*/null, /*florins*/null, /*shillings*/10,
                    /*sixpences*/null, /*threepences*/null, /*pennies*/16, /*halfpennies*/5, /*farthings*/5, /*expectedPounds*/0, /*expectedShillings*/11, /*expectedPence*/7.75)]
        [InlineData(/*guineas*/null, /*pounds*/2, /*sovreigns*/null, /*halfSovreigns*/null, /*crowns*/null, /*halfCrowns*/null, /*florins*/null, /*shillings*/3,
                    /*sixpences*/null, /*threepences*/null, /*pennies*/6, /*halfpennies*/null, /*farthings*/null, /*expectedPounds*/2, /*expectedShillings*/3, /*expectedPence*/6)]
        [InlineData(/*guineas*/null, /*pounds*/null, /*sovreigns*/null, /*halfSovreigns*/null, /*crowns*/null, /*halfCrowns*/null, /*florins*/null, /*shillings*/20,
                    /*sixpences*/null, /*threepences*/null, /*pennies*/12, /*halfpennies*/null, /*farthings*/null, /*expectedPounds*/1, /*expectedShillings*/1, /*expectedPence*/0)]
        [InlineData(/*guineas*/1, /*pounds*/null, /*sovreigns*/null, /*halfSovreigns*/null, /*crowns*/null, /*halfCrowns*/null, /*florins*/null, /*shillings*/null,
                    /*sixpences*/null, /*threepences*/null, /*pennies*/null, /*halfpennies*/null, /*farthings*/null, /*expectedPounds*/1, /*expectedShillings*/1, /*expectedPence*/0)]
        public void FromCoins(int? guineas, int? pounds, int? sovreigns, int? halfSovreigns, int? crowns, int? halfCrowns, int? florins, int? shillings,
                                int? sixpences, int? threepences, int? pennies, int? halfpennies, int? farthings,
                                int expectedPounds, int expectedShillings, double expectedPence)
        {
            var price = Price.FromCoins(guineas, pounds, sovreigns, halfSovreigns, crowns, halfCrowns, florins, shillings, sixpences, threepences, pennies, halfpennies, farthings);

            price.Pounds.Should().Be(expectedPounds);
            price.Shillings.Should().Be(expectedShillings);
            price.Pence.Should().Be(expectedPence);
        }
    }
}
