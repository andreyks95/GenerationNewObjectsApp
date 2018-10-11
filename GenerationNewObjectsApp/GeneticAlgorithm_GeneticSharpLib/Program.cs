using System;
using System.Collections;
using System.Linq;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace GeneticAlgorithm_GeneticSharpLib
{
    class Program
    {
        static void Main(string[] args)
        {

            //Program.BuildTestGA();
            // Program.BuildMyGAFirstVariant();
            Program.BuildMyGASecondVariant();

            //ConverterToFromChromosome converter = new ConverterToFromChromosome();
            //string str = converter.BuildChromosomeString(
            //    new string[] { "f1", "f2" },
            //    new string[] { "s1", "s169", "s4" },
            //    new string[] { "m1", "m3", "m16" });
            //string str = "-f1-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.-f2-|s1|.m1.m3.m16.|s169|.m1.m3.m16.|s4|.m1.m3.m16.";
            //char[] separator = new char[] { '-', '|', '.' };
            //string[] s = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            //converter.ParseChromosome(s);



            Console.ReadKey();
        }

        public static void BuildMyGASecondVariant()
        {
            var bga = new BuildGeneticAlgorithm(20, 50, 150);
            bga.Fitness = new FuncFitness(c =>
            {
                var fc = c as BinaryChromosome;
                double result = 0.0;
                foreach (GeneticSharp.Domain.Chromosomes.Gene gene in fc.GetGenes())
                {
                    result += Convert.ToDouble(gene.Value.ToString());
                }
                return result;
            });
            bga.SetSelection(Selection.TournamentSelection);
            bga.SetCrossover(Crossover.UniformCrossover);
            bga.SetMutation(Mutation.FlipBitMutation);
            bga.SetTermination(Termination.GenerationNumberTermination, 100);
            var geneticAlgorithm = bga.GetGA();
            //var latestFitness = 0.0;
            //geneticAlgorithm.GenerationRan += 
            //або
            bga.GenerationRan(geneticAlgorithm);
            bga.Start(geneticAlgorithm);
        }

        public static void BuildMyGAFirstVariant()
        {

            var ga = new BuildGeneticAlgorithm(20, 50, 150);
            ga.Fitness = new FuncFitness(c =>
            {
                var fc = c as BinaryChromosome;
                double result = 0.0;
                foreach (GeneticSharp.Domain.Chromosomes.Gene gene in fc.GetGenes())
                {
                    result += Convert.ToDouble(gene.Value.ToString());
                }
                return result;
            });
            ga.SetSelection(Selection.TournamentSelection);
            ga.SetCrossover(Crossover.UniformCrossover);
            ga.SetMutation(Mutation.FlipBitMutation);
            ga.SetTermination(Termination.GenerationNumberTermination, 100);
            ga.Start();
        }

        public static void BuildTestGA()
        {
            //Create chromosome length = 20
            BinaryChromosome chrom = new BinaryChromosome(20);

            //Create population = [2,100]
            var population = new Population(2, 100, chrom);

            //Console.WriteLine(chrom.Length);
            //Console.WriteLine(chrom.ToString());

            //create Fitness Function (функция приспособления)
            var fitness = new FuncFitness(
                (c) =>
                {
                    var fc = c as BinaryChromosome;
                    double result = 0.0;
                    foreach (Gene gene in fc.GetGenes())
                    {
                        result += Convert.ToDouble(gene.Value.ToString());
                    }
                    return result;
                }
                );


            //create selection 
            //var selection = new EliteSelection(); 
            var selection = new TournamentSelection();
            //var selection = new RouletteWheelSelection();
            //var selection = new StochasticUniversalSamplingSelection();

            //create crossover
            var crossover = new UniformCrossover(0.5f);
            //var crossover = new CutAndSpliceCrossover(); //только с EliteSelection()
            //var crossover = new OnePointCrossover();
            //var crossover = new TwoPointCrossover();
            //var crossover = new CycleCrossover(); // new OrderBasedCrossover(); new OrderedCrossover(); new PositionBasedCrossover(); new PartiallyMappedCrossover(); //может использоваться только с упорядоченными хромосомами. Указанная хромосома имеет повторяющиеся гены
            //var crossover = new ThreeParentCrossover(); //ОДНУ Итерацию выполняет

            //create mutation 
            var mutation = new FlipBitMutation();
            //var mutation = new UniformMutation(); //1 перегрузка принимает индексы генов для мутации, 2-я все гены мутируют
            //var mutation = new TworsMutation(); //Слабая
            //var mutation = new ReverseSequenceMutation(); //Слабая


            //create termination (Количество итераций)
            var termination = new GenerationNumberTermination(100);
            //var termination = new FitnessStagnationTermination(50);
            // var termination = new FitnessThresholdTermination(50); //Постоянно зацикливается
            //TimeSpan time = new TimeSpan(0, 0, 10); //10 секунд
            //var termination = new TimeEvolvingTermination(time);

            //Сам генетический алгоритм
            var ga = new GeneticAlgorithm(
                population,
                fitness,
                selection,
                crossover,
                mutation);

            ga.Termination = termination;

            Console.WriteLine("Generation:  = distance");

            var latestFitness = 0.0;

            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as BinaryChromosome;
                var bestFitness = bestChromosome.Fitness.Value;

                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.GetGenes();

                    //Console.WriteLine(
                    //    "Generation {0,2}: ({1},{2}),({3},{4}) = {5}",
                    //    ga.GenerationsNumber,
                    //    phenotype[0],
                    //    phenotype[1],
                    //    phenotype[2],
                    //    phenotype[3],
                    //    bestFitness
                    //);

                    Console.WriteLine("Generation {0,2}. Best Fitness = {1}", ga.GenerationsNumber, bestFitness);
                    Console.Write("Chromosome: ");

                    foreach (Gene g in phenotype)
                    {
                        Console.Write(g.Value.ToString() + "");
                    }
                    Console.WriteLine();
                }
            };

            ga.Start();
        }

    }
}
