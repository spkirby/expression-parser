# Expression Parser
A simple expression parser and evaluator written in C#.

It works by parsing the input expression into tokens which are then reordered from infix to postfix notation using the Shunting Yard Algorithm. The tokens are then evaluated and the result is output to the console.

The parser can currently handle:
* Numeric values, in the form `12345` or `12345.67`
* Basic arithmetic operators: `+ - * /`
* Parentheses: `( )`
