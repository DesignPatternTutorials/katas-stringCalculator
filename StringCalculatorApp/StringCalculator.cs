using System.Xml.Schema;

namespace StringCalculatorApp;

public class StringCalculator
{
    public string Delimiter
    {
        get => field;
        set => field = string.IsNullOrEmpty(value) ? "," : value;
    } = ",";

    public IEnumerable<string> Delimiters
    {
        get => field;
        set => field = value;
    } = new List<string>();

    /// <summary>
    /// Very simple calculater, sum all numbers in a string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        { return 0; }

        var numbers = Delimiters.Any()
            ? value.Split(Delimiters.ToArray(), StringSplitOptions.None)
            : value.Split(Delimiter, StringSplitOptions.None);

        var sum = numbers.Sum(m => int.Parse(m));

        return sum;
    }
}