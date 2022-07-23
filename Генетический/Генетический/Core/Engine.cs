using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Core
{
    public class Engine<T>
    {
        private Settings.Profile<T> profile { get; set; }
        public List<Types.Child<T>> children { get; set; }
        public List<Types.Child<T>> selection { get; set; }
        private enum State
        {
            not_found = 0,
            found = 1
        }
        private State status { get; set; }

        public Engine(Settings.Profile<T>  profile)
        {
            this.profile = profile;
            status = State.not_found;
            children = new List<Types.Child<T>>();
            selection = new List<Types.Child<T>>();
        }

        public void run()
        {
            uint generation = 0;
            Types.Dna<T> parent = null;

            while (status == State.not_found)
            {
                if (selection.Count > 0)
                {
                    parent = copy_dna(selection[selection.Count - 1].dna);
                } else
                {
                    parent = copy_dna(profile.children.default_dna);
                }
                Console.WriteLine($"Generation {generation}: {parent.score.score_current}");
                children = init_generation(parent, generation);

                foreach (Types.Child<T> child in children)
                {
                    if (add(child) == true)
                    {
                        Logger.Logger.log($"[ ELIT ] {Tools.Tools<T>.display_dna(child)}");
                        selection.Add(child);
                        if (child.dna.score.score_current == profile.target.key.Count)
                            status = State.found;
                    }
                }
                generation++;

                // BYPASS
                //if (generation == 10)
                //{
                //    status = State.found;
                //}
            }

            Console.WriteLine("Success");
        }

        private bool add(Types.Child<T> elite)
        {
            if (elite.dna.score.score_current < profile.target.key.Count)
            {
                foreach (Types.Child<T> child in selection)
                {
                    if ((child.dna.score.score_current >= elite.dna.score.score_current) || (child.dna.score.score_current <= child.heredity.score.score_current))
                    {
                        return (false);
                    }
                }
            } else if (status == State.found)
            {
                return (false);
            }

            return (true);
        }

        private Types.Dna<T> copy_dna(Types.Dna<T> dna)
        {
            List<Types.Gene<T>> genes = new List<Types.Gene<T>>();
            uint score = dna.score.score_current;

            foreach (Types.Gene<T> gene in dna.key)
            {
                genes.Add(gene);
            }

            return (new Types.Dna<T>(genes, score, dna.mutations));
        }

        private List<Types.Child<T>> init_generation(Types.Dna<T> dna, uint gene)
        {
            List<Types.Child<T>> generation = new List<Types.Child<T>>();

            for (int i = 0; i < profile.children.count; i++)
            {
                Logger.Logger.log($"[ LOAD ] child: {i}");
                generation.Add(new Types.Child<T>(dna, gene, profile.choices, profile.target, profile.children.mutations_maximum));
                Logger.Logger.log($"[ WAIT ] child: {i}");
                generation[generation.Count - 1].mutate();
                Logger.Logger.log($"[ DONE ] child: {i}");
                Logger.Logger.log($"[ PRNT ] {Tools.Tools<T>.display_parent(generation[generation.Count - 1])}");
                Logger.Logger.log($"[ PPSR ] {generation[generation.Count - 1].heredity.score.score_current}");
                Logger.Logger.log($"[ DATA ] {Tools.Tools<T>.display_dna(generation[generation.Count - 1])}");
            }

            return (generation);
        }
    }
}
