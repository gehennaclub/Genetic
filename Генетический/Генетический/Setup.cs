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

        public List<string> dump_model()
        {
            Types.Child<T> winner = engine.selection[engine.selection.Count - 1];

            List<string> dump = new List<string>()
            {
                $";model\n{id(winner)}\n",
                $";generation\n{winner.generation}\n",
                $";total dna mutations\n{winner.dna.mutations.Count}\n",
                $";mutations"
            };

            foreach (Types.Child<T> elite in engine.selection)
            {
                dump.Add($"{align(elite.generation)} | {Tools.Tools<T>.display_dna(elite)}");
            }

            return (dump);
        }

        private string align(uint value)
        {
            uint alignment = 8;
            string result = "";
            string x_value = value.ToString("X");

            for (int i = x_value.Length; i < alignment; i++)
            {
                result += "0";
            }
            result += x_value;

            return (result);
        }

        private string id(Types.Child<T> winner)
        {
            string data = $"{winner.generation}{winner.mutations}-{winner.dna.key.Count()}{winner.dna.mutations.Count()}{winner.dna.score.score_current}";

            return (data);
        }

        private void resume()
        {
            foreach (Types.Child<T> child in engine.selection)
            {
                Logger.Logger.log($"{display_dna(child)} | {child.dna.score.score_current}");
            }
        }

        private string display_dna(Types.Child<T> child)
        {
            string buffer = "";
            uint total = 0;

            foreach (Types.Gene<T> gene in child.dna.key)
            {
                buffer += $"{gene.value}";
                total += (dynamic)gene.value;
            }
            foreach (Types.Gene<T> gene in child.dna.mutations)
            {
                total += (dynamic)gene.value;
            }
            buffer += $" | {total}";

            return (buffer);
        }
    }
}
