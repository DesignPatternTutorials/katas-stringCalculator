# String Calculator — TDD Kata

A small **Test-Driven Development (TDD)** kata designed to practise writing simple code, growing requirements incrementally, and refactoring safely.

The goal is not to build a complicated calculator. The goal is to practise the **TDD cycle**:

> **Red → Green → Refactor**

Write a failing test, make it pass with the simplest implementation possible, then improve the code without changing its behaviour.

---

## The Challenge

Create a `StringCalculator` that accepts a string containing numbers and returns their sum.

Start with the simplest possible requirement.

### Requirement 1 — Empty input

An empty string should return `0`.

```text
Input:
""

Output:
0
```

### Requirement 2 — One number

A single number should return that number.

```text
Input:
"5"

Output:
5
```

### Requirement 3 — Two numbers

Two numbers separated by a comma should be added together.

```text
Input:
"1,2"

Output:
3
```

### Requirement 4 — Multiple numbers

The calculator should support an arbitrary number of comma-separated values.

```text
Input:
"1,2,3"

Output:
6
```

---

## Extend the Calculator

Once the basic functionality is working, introduce the following requirements **one at a time**.

### 5. Newlines as separators

Newlines should be accepted as separators in addition to commas.

```text
Input:
"1\n2,3"

Output:
6
```

---

### 6. Custom separators

Allow the caller to specify a custom separator.

The format is:

```text
"//[separator]\n[numbers]"
```

For example:

```text
Input:
"//;\n1;2"

Output:
3
```

Another example:

```text
"//|\n1|2|3"

Output:
6
```

---

### 7. Multiple separators

Support multiple custom separators.

For example:

```text
Input:
"//[*][%]\n1*2%3"

Output:
6
```

The separators may be different lengths.

For example:

```text
Input:
"//[***][%%]\n1***2%%3"

Output:
6
```

---

### 8. Negative numbers

Negative numbers should not be accepted.

For example:

```text
Input:
"1,-2,3"
```

The calculator should throw an exception containing the negative number.

If multiple negative numbers are supplied, **all negative numbers should be reported**.

For example:

```text
Input:
"1,-2,-5"
```

The error should identify:

```text
-2, -5
```

---

### 9. Arbitrary numbers

The calculator should support an arbitrary number of values.

For example:

```text
Input:
"1,2,3,4,5,6,7,8,9,10"

Output:
55
```

The implementation should not be limited to a fixed number of values.

---

## TDD Rules

Work through the requirements **in order**.

For each requirement:

1. Write a failing test.
2. Make the test pass.
3. Refactor.
4. Run the complete test suite.
5. Move to the next requirement.

Try not to implement future requirements before their tests require them.

### The Golden Rule

**Don't write production code unless a failing test requires it.**

---

## Suggested Project Structure

```text
StringCalculator/
│
├── StringCalculator.sln
│
├── StringCalculator/
│   └── StringCalculator.cs
│
└── StringCalculator.Tests/
    └── StringCalculatorTests.cs
```

---

## What This Kata Practises

This exercise deliberately starts very small and gradually introduces complexity.

It provides practice with:

* Unit testing
* TDD
* xUnit
* Arrange / Act / Assert
* Incremental development
* Refactoring
* String parsing
* Collections
* Exception handling
* Parameterised tests
* Test naming
* Edge cases
* Separating parsing from business logic

---

## Stretch Goals

Once the core requirements are complete, consider extending the kata.

Possible challenges:

* Ignore numbers greater than `1000`
* Support decimal numbers
* Support whitespace
* Support quoted separators
* Allow spaces around numbers
* Return useful error messages for malformed input
* Separate parsing from calculation
* Add performance tests
* Create property-based tests

---

## The Point of the Exercise

The finished calculator isn't the interesting part.

The interesting part is **how you get there**.

Try to keep each change small enough that you can clearly see:

```text
FAIL
 ↓
PASS
 ↓
REFACTOR
 ↓
NEXT TEST
```

Don't try to design the final solution at the beginning.

Let the tests drive the design.
