using System;
using Xunit;
using NumberToWords;

namespace NumberToWords.Tests;

public class MoneyToWordsTests
{
    [Theory]
    // Singular and Plural Dollars
    [InlineData(0, "ZERO DOLLARS")]
    [InlineData(1, "ONE DOLLAR")]
    [InlineData(2, "TWO DOLLARS")]

    //Hyphen 
    [InlineData(21, "TWENTY-ONE DOLLARS")]

    // no AND when == hundred
    [InlineData(100, "ONE HUNDRED DOLLARS")]

    // AND required
    [InlineData(101, "ONE HUNDRED AND ONE DOLLARS")]     
    [InlineData(115, "ONE HUNDRED AND FIFTEEN DOLLARS")]
    [InlineData(999, "NINE HUNDRED AND NINETY-NINE DOLLARS")]
    [InlineData(1000, "ONE THOUSAND DOLLARS")]
    [InlineData(1001, "ONE THOUSAND ONE DOLLARS")]
    [InlineData(1_000_000, "ONE MILLION DOLLARS")]
    [InlineData(1_000_001, "ONE MILLION ONE DOLLARS")]
    [InlineData(1_000_000_000, "ONE BILLION DOLLARS")]
    public void Whole_Dollars_Format_As_Expected(decimal input, string expected)
        => Assert.Equal(expected, NumberToWordsConverter.MoneyToWords(input));

    // Cents
    [Theory]
    [InlineData(0.01, "ZERO DOLLARS AND ONE CENT")]
    [InlineData(0.10, "ZERO DOLLARS AND TEN CENTS")]
    [InlineData(1.01, "ONE DOLLAR AND ONE CENT")]
    [InlineData(2.01, "TWO DOLLARS AND ONE CENT")]
    [InlineData(2.10, "TWO DOLLARS AND TEN CENTS")]
    [InlineData(123.45, "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS")]
    public void Dollars_And_Cents_Format_As_Expected(decimal input, string expected)
        => Assert.Equal(expected, NumberToWordsConverter.MoneyToWords(input));

    // --- Errors ---
    [Fact]
    public void Negative_Amounts_Throw()
        => Assert.Throws<ArgumentOutOfRangeException>(() => NumberToWordsConverter.MoneyToWords(-0.01m));
}
