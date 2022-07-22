using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Types
{
    public class Gene<T>
    {
        public uint id { get; set; }
        public T value { get; set; }

        public Gene(uint id, T value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
