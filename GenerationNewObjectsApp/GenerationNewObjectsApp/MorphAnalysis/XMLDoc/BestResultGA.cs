using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.XMLDoc
{
    class BestResultGA
    {
        public string   ProjectName         { get; set; }

        public double   Estimate            { get; set; }

        public int      MaxPopulation       { get; set; }

        public int      CountFunction       { get; set; }

        public int      BestEpoch         { get; set; }
    }
}
