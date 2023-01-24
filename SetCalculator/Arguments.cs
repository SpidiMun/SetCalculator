using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetCalculator
{
    public class Arguments
    {
        public Dictionary<string, Set<object>> sets = new Dictionary<string, Set<object>>();
        
        public Dictionary<string, Set<object>>.KeyCollection GetNames()
        {
            return sets.Keys;
        }
        
        public Arguments()
        {

        }
        
        public Arguments(string row)
        {
            ReadArguments(row);
            if (!HasUniversum())
            {
                Set<object> set = GetUniversum();
                set.SetName("U");
                AddArgument(set);
            }
        }
        
        public bool IsRightInput(string row)
        {
            if (row.Length == 0)
            {
                return false;
            }
            List<string> set = new List<string>(row.Split('\n'));
            try
            {
                foreach (var str in set)
                {
                    List<string> checkRow = new List<string>(str.Split('='));
                    if (checkRow[0].Length != 0)
                    {
                        while (checkRow[0][0] == ' ' || checkRow[0][checkRow[0].Length - 1] == ' ')
                        {
                            checkRow[0] = checkRow[0].Trim();
                            if (checkRow[0].Length == 0)
                            {
                                break;
                            }
                        }
                    }
                    if (checkRow[0].Length == 0)
                    {
                        return false;
                    }
                    int i = 0;
                    if (checkRow[1].Length != 0)
                    {
                        while (checkRow[1][0] == ' ' || checkRow[1][checkRow[1].Length - 1] == ' ')
                        {
                            checkRow[1] = checkRow[1].Trim();
                            if (checkRow[1].Length != 0)
                            {
                                break;
                            }
                        }
                    }
                    if (checkRow[1].Length == 0)
                    {
                        return false;
                    }
                    while (checkRow[1][i] != '{')
                    {
                        if (checkRow[1][i] != ' ')
                        {
                            return false;
                        }
                        i++;
                    }
                    if (checkRow[1][checkRow[1].Length - 1] != '}')
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public void AddArgument(Set<object> set)
        {
            sets.Add(set.GetName(), set);
        }
        
        public bool HasUniversum()
        {
            if (sets.ContainsKey("U") || sets.ContainsKey("Universum"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Set<object> Universum()
        {
            if (sets.ContainsKey("U"))
            {
                return sets["U"];
            }
            else
            {
                return sets["Universum"];
            }
        }
        
        public void ReadArguments(string row)
        {
            List<string> set = new List<string>(row.Split('\n'));
            try
            {
                foreach (var elem in set)
                {
                    Set<object> subSet = new Set<object>();
                    List<string> formalSet = new List<string>(elem.Split('='));
                    while (formalSet[0][0] == ' ' || formalSet[0][formalSet[0].Length - 1] == ' ')
                    {
                        formalSet[0] = formalSet[0].Trim();
                    }
                    subSet.SetName(formalSet[0]);
                    while (formalSet[1][0] == ' ' || formalSet[1][formalSet[1].Length - 1] == ' ')
                    {
                        formalSet[1] = formalSet[1].Trim();
                    }
                    string element = "";
                    for (int i = 1; i < formalSet[1].Length - 1; i++)
                    {
                        if (formalSet[1][i] == ',' || formalSet[1][i + 1] == '}')
                        {
                            if (element.Length != 0)
                            {
                                if (formalSet[1][i + 1] == '}')
                                {
                                    element += formalSet[1][i];
                                }
                                while (element[0] == ' ' || element[element.Length - 1] == ' ')
                                {
                                    element = element.Trim();
                                }
                                subSet.Add(element);
                            }
                            element = "";
                        }
                        else
                        {
                            if (formalSet[1][i] == '{')
                            {
                                string formalSubSet = formalSet[1].Remove(0, i);
                                while (formalSubSet[0] == ' ' || formalSubSet[formalSubSet.Length - 1] == ' ')
                                {
                                    formalSubSet = formalSubSet.Trim();
                                }
                                i = GetIndex(formalSet[1]);
                                while (formalSet[1][i] != ',' && i < formalSet[1].Length - 1)
                                {
                                    i++;
                                }
                                subSet.Add(GetSet(formalSubSet));
                            }
                            else
                            {
                                element += formalSet[1][i];
                                element = element.Trim(',');
                                element = element.Trim('}');
                            }
                        }

                    }
                    sets.Add(subSet.GetName(), subSet);
                }
            }
            catch
            {

            }
        }
        
        private Set<object> GetSet(string row)
        {
            Set<object> set = new Set<object>();
            string element = "";
            int i = 1;
            try
            {
                while (row[i] != '}')
                {
                    if (row[i] == ',' || row[i + 1] == '}')
                    {
                        
                        if (row[i + 1] == '}')
                        {
                            element += row[i];
                        }
                        while (element[0] == ' ' || element[element.Length - 1] == ' ')
                        {
                            element = element.Trim();
                        }
                        if (element.Length != 0)
                        {
                            set.Add(element);
                        }
                        element = "";
                    }
                    else
                    {
                        if (row[i] == '{')
                        {
                            string formalSubSet = row.Remove(0, i - 1);
                            while (formalSubSet[0] == ' ' || formalSubSet[formalSubSet.Length - 1] == ' ')
                            {
                                formalSubSet = formalSubSet.Trim();
                            }
                            i = GetIndex(row);
                            while (row[i] != ',' && i < row.Length - 1)
                            {
                                i++;
                            }
                            set.Add(GetSet(formalSubSet));
                        }
                        else
                        {
                            element += row[i];
                            element = element.Replace(",", "");
                            element = element.Replace("}", "");
                        }
                    }
                    i++;
                }
                return set;
            }
            catch
            {
                return null;
            }
        }
        
        private int GetIndex(string row)
        {
            int index = 0;
            while (row[index] != '}')
            {
                index++;
            }
            return index;
        }
        
        public Set<object> GetArgument(string setName)
        {
            int counter = 0;
            foreach (var name in sets.Keys)
            {
                if (setName == name)
                {
                    counter++;
                }
            }
            if (counter != 0)
            {
                return sets[setName];
            }
            else
            {
                return null;
            }
        }
        
        public Set<object> GetUniversum()
        {
            try
            {
                Set<object> universum = sets.First().Value;
                foreach (var set in sets)
                {
                    universum = Set<object>.Union(universum, set.Value);
                }
                return universum;
            }
            catch
            {
                return null;
            }
        }
    }
}
