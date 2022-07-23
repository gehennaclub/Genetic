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
        public uint generation { get; set; }
        public Dna<T> heredity { get; set; }
        private Dna<T> choices { get; set; }
        private Dna<T> target { get; set; }
        public Dna<T> dna { get; set; }
        public uint mutations { get; set; }
        public uint mutations_maximum { get; set; }

        public Child(Dna<T> heredity, uint generation, Dna<T> choices, Dna<T> target, uint mutations_maximum)
        {
            this.heredity = heredity;
            this.choices = choices;
            this.target = target;
            this.mutations_maximum = mutations_maximum;
            this.generation = generation;

            new_dna(heredity);
            
            random = new Random();
            mutations = 0;
        }

        private void new_dna(Dna<T> dna)
        {
            List<Gene<T>> genes = new List<Gene<T>>();
            List<Gene<T>> mutations = new List<Gene<T>>();
            uint score = dna.score.score_current;

            foreach (Gene<T> gene in dna.mutations)
            {
                mutations.Add(gene);
            }
            foreach (Gene<T> gene in dna.key)
            {
                genes.Add(gene);
            }

            this.dna = new Dna<T>(genes, score, mutations);
        }

        public void mutate()
        {
            while (dna.score.score_current <= dna.score.score_default && mutations < mutations_maximum)
            {
                for (uint i = dna.score.score_current; i < target.key.Count && dna.score.score_current <= dna.score.score_default; i++)
                {
                    mutation(i);
                }
                mutations++;
            }
        }

        private void mutation(uint index)
        {
            Gene<T> gene = choices.key[random.Next(0, choices.key.Count)];
            //T backup = dna.key[index].value;

            dna.key[(int)index] = gene;
            dna.mutations.Add(dna.key[(int)index]);

            score();
        }

        private bool evaluate(int index)
        {
            return ((dynamic)dna.key[index].value == (dynamic)target.key[index].value);
        }

        private uint score()
        {
            dna.score.score_current = 0;

            for (int i = 0; i < dna.key.Count; i++)
            {
                if (evaluate(i) == true)
                {
                    dna.score.score_current++;
                }
            }

            return (dna.score.score_current);
        }
    }
}
