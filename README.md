# Expression Parser
A simple expression parser and evaluator written in C#.

<sub>Created February 2017. Last Updated August 2021.</sub>

## About
The program parses the input expression into tokens which are then reordered from infix to postfix notation using the shunting yard algorithm. The tokens are then evaluated and the result is output to the console.

## Supported Forms
Integers and decimal values in standard form (e.g. `12345` or `123.456`) are supported. Scientific form/E-notation (e.g. `1.2e6`) is not yet supported.

## Supported Operators
In descending order of precedence:
Operator       | Symbol | Example
-------------- | ------ | -------
Parentheses    | `()`   | `(10 + 6)`
Negation       | `-`    | `-16`
Exponentiation | `^`    | `2 ^ 4`
Multiplication | `*`    | `8 * 2`
Division       | `/`    | `48 / 3`
Addition       | `+`    | `9 + 7`
Subtraction    | `-`    | `25 - 9`
