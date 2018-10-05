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

            //BitArray bitArr = new BitArray(10);

            //Program.BuildTestGA();

            //Проецирование морф таблицы на хромосому:

            //где: f - функция, s - решение функции, m - модификация
            string[] chainString = new[] { "f1", "s1", "m1", "m2", "m3", "s2", "m1", "m2", "m3", "f2", "s1", "m1", "m2", "m3", "s2", "m1", "m2", "m3" };

            //представление хромосомы без учёта функции, необходимо при оценивании восстановить их порядок
            //BitArray bitArr = new BitArray(new bool[] { true, false, false, false, true, false, false, true, false, false, true, true, false, true, true, false });

            //или будет возвращаться как строка, но также без учёта функции
            string str = "0101101101110011"; //"0:101 1:011|0:111 0:011" // | - разделитель между функциями : - разделитель между решениями

            //Если длина подцепочки в функции фиксированая, то строку пожно разбить ровно на некоторые промежутки
            //В данном примере 0101
            // при этом 1 символ будет соответсвовать решению, остальные модификации

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

            Console.ReadKey();
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
