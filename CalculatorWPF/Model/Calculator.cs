using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorWPF.Model
{
    internal class Calculator
    {
        //Перевод в обратную польскую запись
        private static List<string> ConvertToReversePolish(string str)
        {
            var stack = new Stack<string>();

            var num = new Regex(@"\d");

            var tempStr = new List<string>();

            for (var i = 0; i < str.Length; i++)
            {
                var ch = "";
                ch += str[i];

                //Запись числа, в случае если оно больше 1-го символа
                if (char.IsDigit(str[i]) && i < str.Length - 1)
                {
                    while (i + 1 < str.Length && (char.IsDigit(str[i + 1]) || str[i + 1] == ','))
                    {
                        ch += str[++i];
                    }
                }

                //Запись числа в массив
                if (num.IsMatch(ch))
                {
                    tempStr.Add(ch);
                }
                else
                {
                    //Работа с символами
                    if (stack.Count > 0)
                    {
                        if (ch == ")")
                        {
                            while (stack.Peek() != "(")
                            {
                                tempStr.Add(stack.Pop());
                            }
                            stack.Pop();
                        }
                        else if (ch == "(")
                        {
                            stack.Push(ch);
                        }
                        else if (CheckPriority(ch) <= CheckPriority(stack.Peek()))
                        {
                            while (stack.Count > 0 && CheckPriority(ch) <= CheckPriority(stack.Peek()))
                            {
                                tempStr.Add(stack.Pop());
                            }
                            stack.Push(ch);
                        }
                        else
                        {
                            stack.Push(ch);
                        }
                    }
                    else
                    {
                        stack.Push(ch);
                    }
                }
            }

            //Запись оставшихся символов из стека в массив
            if (stack.Count > 0)
            {
                while (stack.Count != 0)
                {
                    tempStr.Add(stack.Pop());
                }
            }

            return tempStr;
        }

        public double Calculate(string inputStr)
        {
            double result;
            var str = ConvertToReversePolish(inputStr);

            if (str.Count() == 1)
            {
                result = Convert.ToDouble(str[0]);
            }
            else
            {
                result = 0.0;

                var num = new Regex(@"\d");

                var stack = new Stack<string>();
                if (str.Count > 1)
                {
                    foreach (var ch in str)
                    {
                        if (num.IsMatch(ch))
                        {
                            stack.Push(ch);
                        }
                        else
                        {
                            var num1 = Convert.ToDouble(stack.Pop());
                            var num2 = Convert.ToDouble(stack.Pop());

                            switch (ch)
                            {
                                case "+":
                                    result = num1 + num2;
                                    break;
                                case "-":
                                    result = num2 - num1;
                                    break;
                                case "*":
                                    result = num1 * num2;
                                    break;
                                case "/":
                                    result = num2 / num1;
                                    break;
                            }

                            stack.Push(result.ToString());
                        }
                    }
                }
            }

            return result;
        }

        //Проверка приоритета
        private static int CheckPriority(string ch)
        {
            var priorityCh = 0;

            switch (ch)
            {
                case "(":
                    priorityCh = 0;
                    break;
                case "+":
                case "-":
                    priorityCh = 1;
                    break;
                case "*":
                case "/":
                    priorityCh = 2;
                    break;
            }

            return priorityCh;
        }
    }
}
