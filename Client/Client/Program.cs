using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint settings = 34;

            Генетический.Settings.Profile<char> profile = new Генетический.Settings.Profile<char>()
            {
                target = init_target(settings),
                choices = init_dna(),
                children = new Генетический.Settings.Profile<char>.Children()
                {
                    count = 10,
                    mutations_maximum = 2000,
                    default_dna = init_default(settings)
                }
            };
            Генетический.Setup<char> setup = new Генетический.Setup<char>(profile);

            setup.train();
            File.WriteAllLines("model.gnt", setup.dump_model());

            Console.ReadLine();
        }

        static Генетический.Types.Dna<char> init_dna()
        {
            Генетический.Types.Dna<char> dna = new Генетический.Types.Dna<char>(new List<Генетический.Types.Gene<char>>(), 0, new List<Генетический.Types.Gene<char>>());

            for (uint i = 0; i < 10; i++)
            {
                dna.key.Add(new Генетический.Types.Gene<char>(i, (char)(i + 48)));
            }

            return (dna);
        }

        static Генетический.Types.Dna<char> init_target(uint count)
        {
            Генетический.Types.Dna<char> dna = new Генетический.Types.Dna<char>(new List<Генетический.Types.Gene<char>>(), 0, new List<Генетический.Types.Gene<char>>());

            for (uint i = 0; i < count; i++)
            {
                dna.key.Add(new Генетический.Types.Gene<char>(i, '9'));
            }

            return (dna);
        }

        static Генетический.Types.Dna<char> init_default(uint count)
        {
            Генетический.Types.Dna<char> dna = new Генетический.Types.Dna<char>(new List<Генетический.Types.Gene<char>>(), 0, new List<Генетический.Types.Gene<char>>());

            for (uint i = 0; i < count; i++)
            {
                dna.key.Add(new Генетический.Types.Gene<char>(i, '0'));
            }

            return (dna);
        }
    }
}
