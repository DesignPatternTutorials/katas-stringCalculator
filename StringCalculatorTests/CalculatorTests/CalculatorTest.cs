using StringCalculatorApp;

namespace StringCalculatorTests.CalculatorTests;

public class CalculatorTest
{
    [Fact]
    public void Calculator_With_EmptyString_Returns_0()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.Add(string.Empty);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Calculator_With_SingleNumber_Returns_Number()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.Add("1");

        // Assert
        Assert.Equal(1, result);
    }

    // NOTE: there's no more UseDelimiter(delimiter) call to pass an invalid value
    // to — delimiters are declared inline in the input string now, not configured
    // on the instance beforehand. The closest equivalent bad input is a "//" header
    // with an empty delimiter before the newline, e.g. "//\n1,2". Whether that
    // should throw or just fall back to treating "" as a (no-op) delimiter is a
    // design decision your Add doesn't currently make explicitly — the empty
    // pattern from a blank bracket group would produce a regex that matches
    // everywhere, which isn't right. Flagging this as a gap rather than guessing
    // the intended behavior:
    // [Fact]
    // public void Calculator_With_EmptyDeclaredDelimiter_Throws() { ... }

    [Theory]
    [InlineData("//|\n1|1|1", 3)]
    [InlineData("//&\n2&2&1", 5)]
    [InlineData("2\n2\n1", 5)] // newline is a default separator, no header needed
    public void Calculator_With_MultipleNumbers_Returns_Sum(string input, int sum)
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.Add(input);

        // Assert
        Assert.Equal(sum, result);
    }

    [Theory]
    [InlineData("//[£][$]\n2£2$2", 6)]
    public void Calculator_With_MultipleDelimiters_Returns_Sum(string input, int sum)
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.Add(input);

        // Assert
        Assert.Equal(sum, result);
    }

    [Fact]
    public void Calculator_WithNoDelimiterDeclared_Uses_CommaByDefault()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.Add("2,2,2");

        // Assert — there's no exposed `Delimiter` field to check directly anymore
        // (the calculator is stateless), so this asserts the default behavior
        // instead of an internal implementation detail.
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculator_WithDeclaredDelimiter_Uses_ThatDelimiter()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        var result = sut.Add("//|\n2|3");

        // Assert — again, testing observable behavior rather than an internal
        // `Delimiters` list, since that list no longer exists.
        Assert.Equal(5, result);
    }

    [Fact]
    public void Calculator_MultipleDelimitersDeclaredInline_Returns_Sum()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act — equivalent to your old "chain AddDelimiter(',').AddDelimiter('|')"
        // test, expressed as one inline declaration instead of a builder chain.
        var result = sut.Add("//[,][|]\n2,2|2");

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public void Calculator_Should_NotAllowNegativeNumbers_Throws_Exception()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        Action action = () => sut.Add("-2,-1");

        // Assert — message now lists the offending numbers (per kata requirement 8),
        // and the exception type is ArgumentException rather than a bare Exception.
        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("-2", exception.Message);
        Assert.Contains("-1", exception.Message);
    }

    [Fact]
    public void Calculator_Should_NotAllowTextValues_Throws_Exception()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        Action action = () => sut.Add("2,x");

        // Assert — thrown as FormatException now, message wraps the bad token in
        // quotes ('x' is not a valid number!) rather than bare text.
        var exception = Assert.Throws<FormatException>(action);
        Assert.Contains("x", exception.Message);
    }
}