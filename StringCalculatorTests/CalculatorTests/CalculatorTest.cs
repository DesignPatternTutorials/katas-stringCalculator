using StringCalculatorApp;

namespace StringCalculatorTests.CalculatorTests;

public class CalculatorTest
{
    [Fact]
    public void Calculator_With_EmptyString_Ruturns_0()
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

    [Theory]
    [InlineData("1,2", 3, null)]
    [InlineData("1,2", 3, "")]
    [InlineData("1|1|1", 3, "|")]
    [InlineData("2&2&1", 5, "&")]
    [InlineData("2\n2\n1", 5, "\n")]
    public void Calculator_With_MultipleNumbers_Returns_Sum(string input, int sum, string delimeter)
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        sut.Delimiter = delimeter;
        var result = sut.Add(input);

        // Assert
        Assert.Equal(sum, result);
    }

    [Fact]
    public void Calculator_WithNoDelimeter_Should_Return_DefaultDelimeter()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act

        // Assert
        Assert.Equal(",", sut.Delimiter);
    }

    [Fact]
    public void Calculator_WithDelimeter_Should_Return_Delimeter()
    {
        // Arrange
        var sut = new StringCalculator();

        // Act
        sut.Delimiter = "|";

        // Assert
        Assert.Equal("|", sut.Delimiter);
    }

}