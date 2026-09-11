using System.Text.RegularExpressions;

namespace StringCalculatorApp;

/// <summary>
/// A simple calculator that parses a delimited string of numbers and returns their sum.
/// Supports the default comma/newline separators as well as custom separators declared
/// inline in the input string (e.g. "//;\n1;2"), and throws on negative input.
/// </summary>
public class StringCalculator
{
    private const string DefaultDelimiterPattern = "[,\n]";

    /// <summary>
    /// Very simple calculator: sums all numbers in a delimited string.
    /// </summary>
    /// <param name="value">
    /// A delimited string of numbers, optionally prefixed with a custom delimiter
    /// declaration in the form "//[delimiter]\n..." (or "//[d1][d2]...\n..." for
    /// multiple delimiters). Returns 0 if null or empty.
    /// </param>
    /// <returns>The sum of all parsed numbers.</returns>
    /// <exception cref="FormatException">
    /// Thrown when any entry cannot be parsed as an integer.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when one or more parsed numbers are negative. The message lists every
    /// negative number found, comma-separated.
    /// </exception>
    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return 0;
        }

        var (delimiterPattern, numbersSection) = ExtractDelimiters(value);

        var numbersAsString = Regex.Split(numbersSection, delimiterPattern);

        var numbers = numbersAsString
            .Where(m => !string.IsNullOrEmpty(m))
            .Select(m =>
            {
                if (int.TryParse(m, out int result))
                {
                    return result;
                }

                throw new FormatException($"'{m}' is not a valid number!");
            })
            .ToList();

        var negatives = numbers.Where(n => n < 0).ToList();
        if (negatives.Count > 0)
        {
            throw new ArgumentException(
                $"Negatives not allowed: {string.Join(", ", negatives)}");
        }

        return numbers.Sum();
    }

    /// <summary>
    /// Checks the input for a "//[delimiter(s)]\n" header. If present, builds a regex
    /// pattern matching any of the declared delimiters (escaped, so special regex
    /// characters in a custom delimiter are treated literally) and returns the
    /// remaining numbers section. If absent, returns the default comma/newline pattern
    /// and the original string unchanged.
    /// </summary>
    private static (string pattern, string numbersSection) ExtractDelimiters(string value)
    {
        if (!value.StartsWith("//"))
        {
            return (DefaultDelimiterPattern, value);
        }

        var newlineIndex = value.IndexOf('\n');
        if (newlineIndex == -1)
        {
            // Malformed header with no terminating newline — treat the whole thing
            // as numbers using the default delimiters rather than guessing.
            return (DefaultDelimiterPattern, value);
        }

        var header = value[2..newlineIndex];
        var numbersSection = value[(newlineIndex + 1)..];

        // Bracketed form supports multiple and/or multi-character delimiters:
        // "//[*][%%]\n..." -> delimiters "*" and "%%"
        var bracketMatches = Regex.Matches(header, @"\[(.*?)\]");

        var delimiters = bracketMatches.Count > 0
            ? bracketMatches.Select(m => m.Groups[1].Value).ToList()
            : [header]; // Simple form: "//;\n..." -> single delimiter ";"

        var pattern = string.Join("|", delimiters.Select(Regex.Escape));

        return (pattern, numbersSection);
    }
}