using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Settings
{
    public class Profile<T>
    {
        public Types.Dna<T> target { get; set; }
        public Types.Dna<T> choices { get; set; }
        public Children children = new Children();

        public class Children
        {
            public Types.Dna<T> default_dna { get; set; }
            public uint count = 100;
            public uint mutations_maximum = 2000;
        }
    }
}
