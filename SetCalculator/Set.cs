using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SetCalculator
{
    public class Set<T> : IEnumerable<T>
    {
        List<T> items = new List<T>();
        
        string name = "-";
        
        public int Count => items.Count;
        
        public void SetName(string newName)
        {
            name = newName;
        }
        
        public string GetName()
        {
            return name;
        }
        
        public void Clear()
        {
            items.Clear();
        }
        
        public bool Contains(T obj)
        {
            if (obj.GetType() == "Привет".GetType())
            {
                string newObj = (obj as string);
                foreach (var item in items)
                {
                    if (item.GetType() != GetType())
                    {
                        if (item.ToString() == newObj.ToString())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                Set<T> newObj = (obj as Set<T>);
                foreach (var item in items)
                {
                    if (item.GetType() == GetType())
                    {
                        Set<T> newItem = (item as Set<T>);
                        foreach (var elem in newObj)
                        {
                            if (!newItem.Contains(elem))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                return false;
            }
        }
        
        public void Add(T item)
        {
            if (item != null)
            {
                if (!Contains(item))
                {
                    items.Add(item);
                }
            }
        }
        
        public string GetSet()
        {
            if (items == null)
                return "Множество не определено!";
            string set = "= { ";
            set += Elements();
            set += " }";
            return set;
        }
        
        public string Elements()
        {
            string set = string.Empty;
            set += Elements(0);
            return set;
        }
        
        private string Elements(int counter)
        {
            string set = string.Empty;
            if (Count == 0)
            {
                return "∅";
            }
            foreach (var item in items)
            {
                Set<T> addingSet = new Set<T>();
                if (item.GetType() == "Привет".GetType())
                {
                    if (counter == 0)
                        set += item;
                    else
                        set += ", " + item;
                    counter++;
                }
                if (item.GetType() == addingSet.GetType())
                {
                    Set<T> newItem = (item as Set<T>);
                    if (counter == 0)
                    {
                        set += "{ ";
                        set += newItem.Elements();
                        set += " }";
                    }
                    else
                    {
                        set += ", { ";
                        set += newItem.Elements();
                        set += " }";
                    }
                    counter++;
                }
            }
            return set;
        }
        
        public static Set<T> Union(Set<T> st1, Set<T> st2)
        {

            var resultSet = new Set<T>();
            foreach (var item in st1.items)
            {
                if (!resultSet.Contains(item))
                {
                    resultSet.Add(item);
                }
            }
            foreach (var item in st2.items)
            {
                if (!resultSet.Contains(item))
                {
                    resultSet.Add(item);
                }
            }
            return resultSet;
        }
        
        public static Set<T> Intersection(Set<T> st1, Set<T> st2)
        {
            var resultSet = new Set<T>();
            if (st1.Count < st2.Count)
            {
                foreach (var item in st1.items)
                {
                    if (st2.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in st2.items)
                {
                    if (st1.Contains(item))
                    {
                        resultSet.Add(item);
                    }
                }
            }
            return resultSet;
        }
        
        public static Set<T> Difference(Set<T> st1, Set<T> st2)
        {
            var resultSet = new Set<T>();
            foreach (var item in st1.items)
            {
                if (!st2.Contains(item))
                {
                    resultSet.Add(item);
                }
            }
            return resultSet;
        }

        public static Set<T> SymmetricDifference(Set<T> st1, Set<T> st2)
        {
            var resultSet = new Set<T>();
            foreach (var item in st1.items)
            {
                if (!st2.Contains(item))
                {
                    resultSet.Add(item);
                }
            }
            foreach (var item in st2.items)
            {
                if (!st1.Contains(item))
                {
                    resultSet.Add(item);
                }
            }
            return resultSet;
        }
        
        public static Set<T> Supplement(Set<T> universum, Set<T> st)
        {
            var resultSet = Difference(universum, st);
            return resultSet;
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
