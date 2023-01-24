using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetCalculator
{
    public class SetExpression
    {
        List<string> variables = new List<string>();
        
        string[] setOperators = { "intersection", "itr", "union", "symetr", "symmetric", "difer", "difference", "supplement", "supl", "(", ")" };
        
        public List<string> Expression => variables;

        public bool IsRightInput(string row, Arguments arg)
        {
            List<string> operands = new List<string>(row.Split(' '));
            try 
            {
                int counter = 0;
                foreach (var elem in operands)
                {
                    string element = elem;
                    if (element.Length == 0)
                        continue;
                    while (element.Length != 0 && element[0] == ' ' && element[element.Length - 1] == ' ')
                    {
                        element = element.Trim();
                    }
                    if (IsOperator(element))
                    {
                        if (element == "(")
                        {
                            counter++;
                        }
                        if (element == ")")
                        {
                            counter--;
                        }
                    }
                    else
                    {
                        if (element[0] == '(' || element[element.Length - 1] == ')')
                        {
                            int i = 0;
                            while (i < element.Length)
                            {
                                if (element[i] == '(')
                                {
                                    counter++;
                                }
                                if (element[i] == ')')
                                {
                                    counter--;
                                }
                                i++;
                            }
                            while (element[0] == '(' && element[element.Length - 1] == ')')
                            {
                                element = element.Trim('(');
                                element = element.Trim(')');
                            }
                        }
                    }
                    if (!IsOperator(element) && !arg.GetNames().Contains(element))
                    {
                        return false;
                    }
                }
                if (counter != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

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

        int GetPriority(string operation)
        {
            int priority;
            switch (operation)
            {
                case "(":
                    priority = 0;
                    break;
                case ")":
                    priority = 1;
                    break;
                case "supplement":
                case "supl":
                    priority = 4;
                    break;
                default:
                    priority = 3;
                    break;
            }
            return priority;
        }

        public void ReadExpression(string rowExpression)
        {
            Stack<string> operationStack = new Stack<string>();
            string operand = string.Empty;
            try
            {
                for (int i = 0; i < rowExpression.Length; i++)
                {
                    if (rowExpression[i] == ' ' || IsOperator(operand) || i == rowExpression.Length - 1)
                    {
                        if (i == rowExpression.Length - 1)
                            operand += rowExpression[i];
                        if (!IsOperator(operand))
                        {
                            if (operand != string.Empty)
                                variables.Add(operand);
                            else
                                continue;
                        }
                        else
                        {
                            if (operand == "(")
                            {
                                operationStack.Push("(");
                            }
                            else
                            {
                                if (operand == ")")
                                {
                                    operand = operationStack.Pop();
                                    while (operand != "(")
                                    {
                                        variables.Add(operand);
                                        operand = operationStack.Pop();
                                    }
                                }
                                else
                                {
                                    if (operationStack.Count > 0)
                                    {
                                        if (GetPriority(operand) < GetPriority(operationStack.Peek()))
                                        {
                                            variables.Add(operationStack.Peek());
                                            if (operationStack.Peek() == "supplement" || operationStack.Peek() == "supl")
                                            {
                                                operationStack.Pop();
                                            }
                                        }
                                    }
                                    operationStack.Push(operand);
                                }
                            }
                        }
                        operand = string.Empty;
                    }
                    else
                    {
                        if (rowExpression[i] == ')' || rowExpression[i] == '(')
                        {
                            if (!IsOperator(operand))
                            {
                                if (operand != string.Empty)
                                {
                                    variables.Add(operand);
                                    operand = string.Empty;
                                }
                                operand += rowExpression[i];
                                i--;
                            }
                        }
                        else
                        {
                            operand += rowExpression[i];
                        }
                    }
                }
                while (operationStack.Count > 0)
                {
                    variables.Add(operationStack.Pop());
                }
            }
            catch
            {
                return;
            }
        }

        public void WriteExpression()
        {
            foreach (var elem in variables)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
        }
    }
}
