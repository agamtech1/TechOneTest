using System;
using System.Collections.Generic;

namespace NumberToWords;

public static class NumberToWordsConverter
{
    // 0 to 19 because they are unique words
    private static readonly string[] Units =
    {
        "ZERO","ONE","TWO","THREE","FOUR","FIVE","SIX","SEVEN","EIGHT","NINE",
        "TEN","ELEVEN","TWELVE","THIRTEEN","FOURTEEN","FIFTEEN","SIXTEEN","SEVENTEEN","EIGHTEEN","NINETEEN"
    };

    // index with tens digit
    private static readonly string[] Tens =
    {
        "ZERO","TEN","TWENTY","THIRTY","FORTY","FIFTY","SIXTY","SEVENTY","EIGHTY","NINETY"
    };

    // Bigger scales
    private static readonly (long Value, string Name)[] Scales =
    {
        // (1_000_000_000_000L, "TRILLION"),
        (1_000_000_000L,     "BILLION"),
        (1_000_000L,         "MILLION"),
        (1_000L,             "THOUSAND")
    };

    // Main entry point

    public static string MoneyToWords(decimal amount)
    {
        // basic validation even though this is not possible through the UI 
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be non-negative.");

        // Round to cents deterministically
        amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);

        long dollars = (long)amount;
        int cents = (int)((amount - dollars) * 100m);

        string dollarsWords = dollars == 0 ? "ZERO" : IntToWords(dollars);
        dollarsWords += dollars == 1 ? " DOLLAR" : " DOLLARS"; // Singular or plural

        if (cents == 0) return dollarsWords;  // No cents ? end here and return the dollars part

        string centsWords = TwoDigitToWords(cents) + (cents == 1 ? " CENT" : " CENTS"); // Singular or plural
        return $"{dollarsWords} AND {centsWords}";
    }

    // helper methods

    private static string IntToWords(long n)
    {
        if (n == 0) return "ZERO";

        var parts = new List<string>();

        // bigger scales
        foreach (var (value, name) in Scales)
        {
            if (n >= value)
            {
                var count = n / value;              
                parts.Add($"{HundredsToWords((int)count)} {name}");
                n %= value;
            }
        }

        // Final 0 to 999 
        if (n > 0) parts.Add(HundredsToWords((int)n));

        return string.Join(" ", parts);
    }

    private static string HundredsToWords(int n)
    {
        var parts = new List<string>();

        int hundreds = n / 100;
        int remainder = n % 100;

        if (hundreds > 0)
        {
            parts.Add($"{Units[hundreds]} HUNDRED");
            if (remainder > 0) parts.Add("AND");       // AND used only if there is a remainder
        }

        if (remainder > 0) parts.Add(TwoDigitToWords(remainder));

        return string.Join(" ", parts);
    }

    // 0 to 99 with hyphen for 21–99 non-multiples of 10
    private static string TwoDigitToWords(int n)
    {
        if (n < 20) return Units[n];

        int t = n / 10;            // 2 to 9
        int u = n % 10;            // 0 to 9

        return u == 0 ? Tens[t] : $"{Tens[t]}-{Units[u]}";
    }
}

