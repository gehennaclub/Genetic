using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Генетический.Types
{
    public class Score
    {
        public uint score_default { get; set; }
        public uint score_current { get; set; }

        public Score(uint score_default)
        {
            this.score_default = score_default;
            this.score_current = score_default;
        }
    }
}
