using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.GeneticAlgorithm
{
    public class BinaryChromosome : BinaryChromosomeBase
    {
        int _length;

        public BinaryChromosome(int length) : base(length)
        {
            _length = length;

            CreateGenes();
        }

        //Создаёт дубликат класса 
        public override IChromosome CreateNew()
        {
            return new BinaryChromosome(_length);
        }

        public void FlipGene(int index)
        {
            var value = (int)GetGene(index).Value;

            ReplaceGene(index, new Gene(value == 0 ? 1 : 0));
        }
        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(RandomizationProvider.Current.GetInt(0, 2));
        }

        public override string ToString()
        {
            //return String.Join(string.Empty, GetGenes().Select(g => g.Value.ToString()).ToArray());
            string value = "";
            foreach (var item in GetGenes())
            {
                value += item.Value;
            }
            return value;
        }

    }
}
