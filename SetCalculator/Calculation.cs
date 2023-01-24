using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCalculator
{
    public class Calculation
    {
        Arguments arg = new Arguments();
        
        SetExpression expr = new SetExpression();
        
        string[] setOperators = { "intersection", "itr", "union", "symetr", "symmetric", "difer", "difference", "supplement", "supl" };
        
        bool IsOperator(string str)
        {
            if (setOperators.Contains(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        Set<object> DoOperation(string operation, Set<object> set1, Set<object> set2)
        {
            Set<object> result = new Set<object>();
            switch (operation)
            {
                case "supplement":
                case "supl":
                    result = Set<object>.Supplement(set1, set2);
                    break;

                case "union":
                    result = Set<object>.Union(set1, set2);
                    break;

                case "symmetric":
                case "sym":
                    result = Set<object>.SymmetricDifference(set1, set2);
                    break;

                case "difference":
                case "difer":
                    result = Set<object>.Difference(set1, set2);
                    break;

                case "intersection":
                case "itr":
                    result = Set<object>.Intersection(set1, set2);
                    break;
            }
            return result;
        }
        
        public string GetResult(string arguments, string expression)
        {
            if (expression == string.Empty || arguments == string.Empty)
            {
                return "Ошибка! Поля для ввода множеств и/или выражения пусты!";
            }
            if (!arg.IsRightInput(arguments))
            {
                return "Ошибка! Неверно введено поле для записи множеств!";
            }
            arg.ReadArguments(arguments);
            Set<object> universum = new Set<object>();
            if (!arg.HasUniversum())
            {
                universum = arg.GetUniversum();
                universum.SetName("U");
                arg.AddArgument(universum);
            }
            else
            {
                universum = arg.Universum();
            }
            if (!expr.IsRightInput(expression, arg))
            {
                return "Ошибка! Неверно введено поле для записи выражения!";
            }
            expr.ReadExpression(expression);
            Stack<Set<object>> setOperands = new Stack<Set<object>>();
            try
            {
                Set<object> result = new Set<object>();
                foreach (var operand in expr.Expression)
                {
                    if (!IsOperator(operand))
                    {
                        setOperands.Push(arg.GetArgument(operand));
                    }
                    else
                    {
                        if (operand == "supplement" || operand == "supl")
                        {
                            Set<object> set = setOperands.Pop();
                            setOperands.Push(DoOperation(operand, universum, set));
                        }
                        else
                        {
                            Set<object> set2 = setOperands.Pop();
                            Set<object> set1 = setOperands.Pop();
                            setOperands.Push(DoOperation(operand, set1, set2));
                        }
                    }
                }
                result = setOperands.Pop();
                if (result != null)
                {
                    return result.GetSet();
                }
                else
                {
                    return "Ошибка! Невозможно получить выражение!";
                }
            }
            catch
            {
                return "Ошибка! Неверны введены поля для записи множеств и/или выражения!";
            }
        }
    }
}
