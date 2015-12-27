using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Utilities
{
    public static class CalcUtils
    {
        private static void CalcAndPush(Stack<double> nums, Stack<char> ops)
        {
            if (nums.Count < 2 || ops.Count < 1)
                throw new Exception("Expression Struct Error");
            double b = nums.Pop();
            double a = nums.Pop();
            switch (ops.Pop())
            {
                case '+':
                    nums.Push(a + b);
                    break;
                case '-':
                    nums.Push(a - b);
                    break;
                case '*':
                    nums.Push(a * b);
                    break;
                case '/':
                    nums.Push(a / b);
                    break;
                case '^':
                    nums.Push(Math.Pow(a, b));
                    break;
            }
        }

        private static short GetPriority(char c)
        {
            if (c == '(')
                return 0;
            if (c == '+' || c == '-')
                return 1;
            if (c == '*' || c == '/')
                return 2;
            if (c == '^')
                return 3;
            else
                throw new Exception("Unknown Operator");
        }

        public static double CalculateExpression(string expression)
        {
            expression = expression.Replace(" ", "");
            if (expression == "")
                throw new Exception("Expression Empty");
            Stack<double> Numbers = new Stack<double>();
            Stack<char> Operators = new Stack<char>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (Char.IsDigit(expression[i]))
                {
                    string s = "";
                    while (i < expression.Length && (Char.IsDigit(expression[i]) || expression[i] == '.'))
                        s += expression[i++];
                    i--;
                    Numbers.Push(double.Parse(s));
                    continue;
                }
                switch (expression[i])
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                        while (Operators.Count() > 0 && GetPriority(Operators.First()) >= GetPriority(expression[i]))
                            CalcAndPush(Numbers, Operators);
                        Operators.Push(expression[i]);
                        break;
                    case '(':
                        Operators.Push('(');
                        break;
                    case ')':
                        while (Operators.First() != '(')
                            CalcAndPush(Numbers, Operators);
                        Operators.Pop();
                        break;
                    default:
                        throw new Exception("Unknown Operator");
                }
            }
            while (Operators.Count() != 0)
                CalcAndPush(Numbers, Operators);
            if (Numbers.Count() != 1)
                throw new Exception("Expression Struct Error");
            else
                return Numbers.First();
        }
    }
}