using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический
{
    public class Setup<T>
    {
        private Settings.Profile<T> profile { get; set; }
        private Core.Engine<T> engine { get; set; }

        public Setup(Settings.Profile<T> profile)
        {
            this.profile = profile;
            engine = new Core.Engine<T>(profile);
        }

        public void train()
        {
            engine.run();
            resume();
        }

        private void resume()
        {
            foreach (Types.Child<T> child in engine.children)
            {
                Console.WriteLine($"{display_dna(child)} | {child.dna.score.score_current}");
            }
        }

        private string display_dna(Types.Child<T> child)
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
