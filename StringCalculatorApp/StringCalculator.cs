namespace StringCalculatorApp;

public class StringCalculator
{

    public int Add(string value)
    {
        if (string.IsNullOrEmpty(value))
        { return 0; }

        var numbers = value.Split(',');

        var sum = numbers.Sum(m => int.Parse(m));

        return sum;
    }
}