using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberToWords;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TechOne_Test.Pages;
public class IndexModel : PageModel
{
    [BindProperty] public string? Amount { get; set; }
    public string? Result { get; set; }
    public string? Error { get; set; }

    // Setting a reasonable maximum amount.
    private const decimal MaxAmount = 999_999_999_999.99m;

    // Regex to validate input format: non-negative number with at most two decimal places
    private static readonly Regex AmountRegex = new(@"^\d+(?:\.\d{1,2})?$", RegexOptions.Compiled);

    // For display in the UI
    public string MaxAmountDisplay => MaxAmount.ToString("N2", CultureInfo.InvariantCulture);      

    public void OnGet() { }

    // Basic input validation and error handling
    public void OnPost()
    {
        if (string.IsNullOrWhiteSpace(Amount)) // null, empty, or whitespace
        {
            Error = "Please enter an amount.";
            return;
        }

        var trimmed = Amount.Trim(); // trim whitespace if any

        if (!AmountRegex.IsMatch(trimmed)) // If it doesn't match the regex pattern
        {
            Error = "Use a non-negative number with at most two decimal places (e.g. 123.45).";
            return;
        }

        if (!decimal.TryParse(trimmed, NumberStyles.Number, CultureInfo.InvariantCulture, out var value)) 
        {
            Error = "Invalid number format.";
            return;
        }

        if (value < 0) // This should not happen due to regex, but just in case
        {
            Error = "Negative amounts are not supported.";
            return;
        }

        var rounded = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
        if (rounded != value)
        {
            Error = "Only two decimal places are allowed.";
            return;
        }

        if (value > MaxAmount) // Exceeds maximum supported amount
        {
            Error = $"Maximum supported amount is {MaxAmount.ToString("N2", CultureInfo.InvariantCulture)}.";
            return;
        }

        try
        {
            Result = NumberToWordsConverter.MoneyToWords(value); // finally if all checks passed then go to conversion
        }
        catch (NotImplementedException)
        {
            Error = "Conversion not implemented yet."; // This was used during development for testing purposes
        }
        catch (Exception ex) // Catch-all for any unexpected errors
        {
            Error = $"Unexpected error: {ex.Message}";
        }
    }
}
