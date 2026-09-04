namespace StringCalculatorApp;

public class StringCalculator
{
    public string Delimiter
    {
        get => field;
        set => field = string.IsNullOrEmpty(value) ? "," : value;
    } = ",";

    /// <summary>
    /// Very simple calculater, sum all numbers in a string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        { return 0; }

        var numbers = value.Split(Delimiter);

        var sum = numbers.Sum(m => int.Parse(m));

        return sum;
    }
}