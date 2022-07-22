using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Types
{
    public class Dna<T>
    {
        public List<Gene<T>> key { get; set; }
        public List<Gene<T>> mutations { get; set; }
        public Score score { get; set; }

        public Dna(List<Gene<T>> genes, uint previous_score, List<Gene<T>> mutations)
        {
            this.key = genes;
            this.score = new Score(previous_score);
            this.mutations = mutations;
        }
    }
}
