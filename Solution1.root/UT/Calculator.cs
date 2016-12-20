using System;
using System.Collections.Generic;
using System.Text;

namespace UT
{
    public class Calculator
    {
        public static double Eval(string expression)
        {
            Int32 ignore;
            return Eval(expression, 0, out ignore);
        }

        private static double Eval(string expression, Int32 level, out Int32 currentPos)
        {
            // grammar :
            // expressionLvl0::= expressionLvl1 (('+'|'-') expressionLvl1)*
            // expressionLvl1::= expressionLvl2 (('*'|'/') expressionLvl2)*
            // expressionLvl2::= value (('^') value)*
            // value::= number
            // number::= '+'|'-'|'' [0-9]([0-9])* (.([0-9])*) E('+'|'-'|'')[0-9]+
            string[] OperatorsTypes = new string[] { "+-", "/*", "^" };
            double result;
            Int32 cumulativeCurrentPos;

            if (level >= OperatorsTypes.Length)
                result = value(expression, out currentPos);
            else
            {
                result = Eval(expression, level + 1, out currentPos);
                cumulativeCurrentPos = currentPos;
                while (currentPos < expression.Length && OperatorsTypes[level].IndexOf(expression[currentPos]) != -1)
                {

                    char Operation = expression[currentPos];
                    expression = expression.Substring(currentPos + 1);
                    double value2 = Eval(expression, level + 1, out currentPos);
                    cumulativeCurrentPos += currentPos + 1;
                    switch (Operation)
                    {
                        case '+':
                            result += value2;
                            break;
                        case '-':
                            result -= value2;
                            break;
                        case '*':
                            result *= value2;
                            break;
                        case '/':
                            result /= value2;
                            break;
                        case '^':
                            result = Math.Pow(result, value2);
                            break;
                    }
                }
                currentPos = cumulativeCurrentPos;
            }

            return result;
        }
        private static double value(string expression, out Int32 currentPos)
        {
            // value::= number
            // number::=  '+'|'-'|'' [0-9]([0-9])* (.([0-9])*) E('+'|'-'|'')[0-9]+
            const string numberFirstChar = "0123456789+-.";
            const string numberInnerChar = "0123456789.Ee";
            const string numberTerminator = "+-*/^";
            currentPos = 0;

            //skip and count the spaces 
            while (currentPos < expression.Length && expression[currentPos] == ' ')
            { 
                currentPos++; 
            }

            if (numberFirstChar.IndexOf(expression[currentPos]) != -1)
            {
                Int32 numberStart = currentPos;
                do
                {
                    currentPos++;
                }
                while (currentPos < expression.Length && ((numberInnerChar.IndexOf(expression[currentPos]) != -1) ||
                     ((expression[currentPos] == '+' || expression[currentPos] == '-') &&
                     (expression[currentPos - 1] == 'E' || expression[currentPos - 1] == 'e'))));
                //skip and count the spaces 
                while (currentPos < expression.Length && expression[currentPos] == ' ')
                { currentPos++; }

                if (currentPos < expression.Length && (numberTerminator.IndexOf(expression[currentPos]) == -1))
                {
                    throw new ApplicationException("Unexpected character");
                }
                return (double.Parse(expression.Substring(numberStart, currentPos - numberStart)));

            }
            else
                throw new ApplicationException("Number Expected");
        }
    }
}
