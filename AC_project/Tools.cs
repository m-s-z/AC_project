using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_project
{
    class Tools
    {

        static List<int> SortedIndicies<T>(List<T> list) where T:IComparable
        {
            // Create list of (index, value) pairs
            List<Tuple<int, T>> list_idx = new List<Tuple<int, T>>();
            for (int i = 0; i < list.Count; i++)
            {
                list_idx.Add(new Tuple<int, T>(i, list[i]));
            }

            // Sort list idx by its second element − the value :
            for (int i = 1; i < list_idx.Count; i++) 
            {
                int j = i;
                while( j > 0 && ( list_idx[j].Item2.CompareTo(list_idx[j-1].Item2) > 0 ))
                {
                    Tuple<int, T> temp = list_idx[j];
                    list_idx[j] = list_idx[j - 1];
                    list_idx[j - 1] = temp;
                    j--;
                }
            }

            List<int> indicies = new List<int>();
            for (int i = 0; i < list_idx.Count; i++)
            {
                indicies.Add(list_idx[i].Item1);
            }
            return indicies;
        }

        static Stack<T> ListToStack<T>(List<T> list)
        {
            // XD
            return new Stack<T>(list);
        }
    }
}
