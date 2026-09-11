namespace StringCalculatorApp;

/// <summary>
/// A simple calculator that parses a delimited string of numbers and returns their sum.
/// Supports configurable delimiters and throws on invalid or negative input.
/// </summary>
public class StringCalculator
{
    bool _customDelimiter = false;

    /// <summary>
    /// The default delimiter used to split numbers when no custom delimiter has been set.
    /// </summary>
    public readonly string Delimiter = ",";

    /// <summary>
    /// The list of custom delimiters currently in use, when <see cref="_customDelimiter"/> is true.
    /// </summary>
    public List<string> Delimiters = [];

    /// <summary>
    /// Replaces any existing custom delimiters with a single new delimiter.
    /// </summary>
    /// <param name="delimeter">The delimiter to use for splitting input strings.</param>
    /// <returns>The current <see cref="StringCalculator"/> instance, for chaining.</returns>
    public StringCalculator UseDelimiter(string delimeter)
    {
        Delimiters = [];
        AddDelimiter(delimeter);

        return this;
    }

    /// <summary>
    /// Adds an additional custom delimiter to the list of delimiters used for splitting input strings.
    /// </summary>
    /// <param name="delimeter">The delimiter to add. Cannot be null or empty.</param>
    /// <returns>The current <see cref="StringCalculator"/> instance, for chaining.</returns>
    /// <exception cref="Exception">Thrown when <paramref name="delimeter"/> is null or empty.</exception>
    public StringCalculator AddDelimiter(string delimeter)
    {
        if (string.IsNullOrEmpty(delimeter))
        { throw new Exception("Invalid Delimiter!"); }

        _customDelimiter = true;
        Delimiters.Add(delimeter);

        return this;
    }

    /// <summary>
    /// Clears any custom delimiters and reverts to using the default <see cref="Delimiter"/>.
    /// </summary>
    /// <returns>The current <see cref="StringCalculator"/> instance, for chaining.</returns>
    public StringCalculator ResetDelimiter()
    {
        _customDelimiter = false;
        Delimiters = [];

        return this;
    }

    /// <summary>
    /// Very simple calculator, sum all numbers in a string.
    /// </summary>
    /// <param name="value">
    /// A delimited string of numbers. Returns 0 if null or empty.
    /// </param>
    /// <returns>The sum of all parsed numbers.</returns>
    /// <exception cref="Exception">
    /// Thrown when any entry in <paramref name="value"/> cannot be parsed as an integer,
    /// or when any parsed number is negative.
    /// </exception>
    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        { return 0; }

        var numbersAsString = _customDelimiter
                    ? value.Split(Delimiters.ToArray(), StringSplitOptions.None)
                    : value.Split(Delimiter, StringSplitOptions.None);


        var numbers = numbersAsString
            .Select(m =>
            {
                if (int.TryParse(m, out int result))
                { return result; }

                throw new Exception($"{m} is not a valid number!");
            }).ToList();


        if (numbers.Any(m => m < 0))
        {
            throw new Exception("Negative numbers are not allowed!");
        }

        return numbers.Sum(m => m);
    }
}