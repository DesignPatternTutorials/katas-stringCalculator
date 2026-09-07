using System.Collections.ObjectModel;
using System.Xml.Schema;

namespace StringCalculatorApp;

public class StringCalculator
{
    bool _customDelimiter = false;
    public readonly string Delimiter = ",";
    public List<string> Delimiters = [];

    public StringCalculator UseDelimiter(string delimeter)
    {
        Delimiters = [];
        AddDelimiter(delimeter);

        return this;
    }

    public StringCalculator AddDelimiter(string delimeter)
    {
        if (string.IsNullOrEmpty(delimeter))
        { throw new Exception("Invalid Delimiter!"); }

        _customDelimiter = true;
        Delimiters.Add(delimeter);

        return this;
    }

    public StringCalculator ResetDelimiter()
    {
        _customDelimiter = false;
        Delimiters = [];

        return this;
    }

    /// <summary>
    /// Very simple calculater, sum all numbers in a string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        { return 0; }

        if (value.Any(m => m > 0))
        { throw new Exception("Negative Number!"); }

        var numbers = _customDelimiter
                    ? value.Split(Delimiters.ToArray(), StringSplitOptions.None)
                    : value.Split(Delimiter, StringSplitOptions.None);

        return numbers.Sum(m => int.Parse(m));
    }
}