using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Tools
{
    public class Tools<T>
    {
        public static string display_dna(Types.Child<T> child)
        {
            string buffer = "";

            foreach (Types.Gene<T> gene in child.dna.key)
            {
                buffer += $"{gene.value} -> ";
            }

            return (buffer);
        }
    }
}
