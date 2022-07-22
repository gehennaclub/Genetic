using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Types
{
    public class Child<T>
    {
        private Random random { get; set; }
        private Dna<T> heredity { get; set; }
        private List<T> choices { get; set; }
        private List<T> target { get; set; }
        public Dna<T> dna { get; set; }
        public uint mutations { get; set; }
        public uint mutations_maximum { get; set; }

        public Child(Dna<T> heredity, List<T> choices, List<T> target, uint mutations_maximum)
        {
            this.heredity = heredity;
            this.choices = choices;
            this.target = target;
            this.mutations_maximum = mutations_maximum;

            dna = copy_dna(heredity);
            random = new Random();
            mutations = 0;
        }

        private Dna<T> copy_dna(Dna<T> dna)
        {
            List<Gene<T>> genes = new List<Gene<T>>();

            foreach (Gene<T> gene in dna.key)
            {
                genes.Add(gene);
            }

            return (new Dna<T>(genes, dna.score.score_current, dna.mutations));
        }

        public void mutate()
        {
            while (dna.score.score_current <= dna.score.score_default && mutations < mutations_maximum)
            {
                for (int i = 0; i < target.Count; i++)
                {
                    mutation(i);
                }
                mutations++;
            }
        }

        private void mutation(int index)
        {
            T choice = choices[random.Next(choices.Count)];
            Gene<T> backup = new Gene<T>(dna.key[index].id, choice);

            dna.key[index].value = choice;
            dna.mutations.Add(dna.key[index]);

            dna.score.score_current = score();
        }

        private bool evaluate(int index)
        {
            return ((object)dna.key[index].value == (object)target[index]);
        }

        private uint score()
        {
            uint result = 0;

            for (int i = 0; i < target.Count; i++)
            {
                if (evaluate(i) == true)
                {
                    result++;
                }
            }

            return (result);
        }
    }
}
