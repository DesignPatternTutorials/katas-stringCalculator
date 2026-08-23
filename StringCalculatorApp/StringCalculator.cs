namespace StringCalculatorApp;

public class StringCalculator
{

    /// <summary>
    /// Very simple calculater, sum all numbers in a string.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        { return 0; }

        var numbers = value.Split(',');

        var sum = numbers.Sum(m => int.Parse(m));

        return sum;
    }
}