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
            selection = new List<Types.Child<T>>();
        }

        public void run()
        {
            uint generation = 0;
            Types.Dna<T> parent = null;

            while (status == State.not_found)
            {
                Console.WriteLine($"Generation {generation}:\n");

                if (selection.Count > 0)
                {
                    parent = selection[selection.Count].dna;
                } else
                {
                    parent = profile.children.default_dna;
                }
                children = init_generation(parent);

                foreach (Types.Child<T> child in children)
                {
                    Console.WriteLine($"Current dna: {Tools.Tools<T>.display_dna(child)}");
                    Console.WriteLine($"Dna history: {child.dna.mutations.Count}");
                    if (child.dna.score.score_current > parent.score.score_current)
                    {
                        Console.WriteLine($"Elite found: {child.dna.score.score_current}");
                        selection.Add(child);
                        if (child.dna.score.score_current == profile.target.Count)
                            status = State.found;
                    }
                }
                generation++;

                // BYPASS
                if (generation == 10)
                {
                    status = State.found;
                }
            }
        }

        private List<Types.Child<T>> init_generation(Types.Dna<T> dna)
        {
            List<Types.Child<T>> generation = new List<Types.Child<T>>();

            for (int i = 0; i < profile.children.count; i++)
            {
                generation.Add(new Types.Child<T>(dna, profile.choices, profile.target, profile.children.mutations_maximum));
                generation[i].mutate();
            }

            return (generation);
        }
    }
}
