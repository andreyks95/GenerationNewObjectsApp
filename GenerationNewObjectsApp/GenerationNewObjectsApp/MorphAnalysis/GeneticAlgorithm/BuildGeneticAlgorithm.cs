using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.GeneticAlgorithm
{
    //best: TournamentSelection
    public enum Selection : byte { EliteSelection, TournamentSelection, RouletteWheelSelection, StochasticUniversalSamplingSelection }

    //best: UniformCrossover з значенням 0.5f
    //bad CutAndSpliceCrossover(); //только с EliteSelection()
    //bad ThreeParentCrossover(); //ОДНУ Итерацию выполняет
    public enum Crossover : byte { UniformCrossover, OnePointCrossover, TwoPointCrossover, ThreeParentCrossover } //, CutAndSpliceCrossover }

    //best: FlipBitMutation
    //var mutation = new UniformMutation(); //1 перегрузка принимает индексы генов для мутации, 2-я все гены мутируют
    public enum Mutation : byte { FlipBitMutation, UniformMutation, TworsMutation, ReverseSequenceMutation }

    //Кількість ітерацій
    //best: GenerationNumberTermination
    //note: TimeEvolvingTermination - в конструктор потрібно передати TimeSpan
    public enum Termination : byte { GenerationNumberTermination, FitnessStagnationTermination, FitnessThresholdTermination, TimeEvolvingTermination }


    public class BuildGeneticAlgorithm
    {
        private GeneticSharp.Domain.Selections.SelectionBase _selection;
        private GeneticSharp.Domain.Crossovers.CrossoverBase _crossover;
        private GeneticSharp.Domain.Mutations.MutationBase _mutation;
        private GeneticSharp.Domain.Terminations.TerminationBase _termination;

        private BinaryChromosome _chromosome;
        private GeneticSharp.Domain.Populations.Population _population;

        private GeneticSharp.Domain.Fitnesses.IFitness _fitness;

        private GeneticSharp.Domain.GeneticAlgorithm _ga;


        public BuildGeneticAlgorithm(int countGenes, int minSizePopulation = 2, int maxSizePopulation = 100)
        {
            _chromosome = new BinaryChromosome(countGenes);
            _population = new GeneticSharp.Domain.Populations.Population(minSizePopulation, maxSizePopulation, _chromosome);
        }

        //Фітнес функція (функція пристосування)
        //double GeneticSharp.Domain.Fitnesses.IFitness.Evaluate(GeneticSharp.Domain.Chromosomes.IChromosome chromosome)
        //{
        //    var fc = chromosome as BinaryChromosome;
        //    double result = 0.0;
        //    foreach (GeneticSharp.Domain.Chromosomes.Gene gene in fc.GetGenes())
        //    {
        //        result += Convert.ToDouble(gene.Value.ToString());
        //    }
        //    return result;
        //}


        public void SetSelection(Selection selection)
        {
            switch (selection)
            {
                case Selection.EliteSelection:
                    _selection = new GeneticSharp.Domain.Selections.EliteSelection();
                    break;

                case Selection.TournamentSelection:
                    _selection = new GeneticSharp.Domain.Selections.TournamentSelection();
                    break;

                case Selection.RouletteWheelSelection:
                    _selection = new GeneticSharp.Domain.Selections.RouletteWheelSelection();
                    break;

                case Selection.StochasticUniversalSamplingSelection:
                    _selection = new GeneticSharp.Domain.Selections.StochasticUniversalSamplingSelection();
                    break;

                default:
                    throw new Exception("Не існує такого методу селекції!");
                    break;
            }
        }

        public void SetCrossover(Crossover crossover, float mixProbabilityUniformCrossover = 0.5f)
        {
            switch (crossover)
            {
                case Crossover.UniformCrossover:
                    _crossover = new GeneticSharp.Domain.Crossovers.UniformCrossover(mixProbabilityUniformCrossover); //ймовірність змішування хромосом
                    break;
                case Crossover.OnePointCrossover:
                    _crossover = new GeneticSharp.Domain.Crossovers.OnePointCrossover();
                    break;
                case Crossover.TwoPointCrossover:
                    _crossover = new GeneticSharp.Domain.Crossovers.TwoPointCrossover();
                    break;
                case Crossover.ThreeParentCrossover:
                    _crossover = new GeneticSharp.Domain.Crossovers.ThreeParentCrossover();
                    break;
                //case Crossover.CutAndSpliceCrossover:
                //    _crossover = new GeneticSharp.Domain.Crossovers.CutAndSpliceCrossover();
                //    break;
                default:
                    throw new Exception("Не існує такого виду кроссовера!");
                    break;
            }
        }

        public void SetMutation(Mutation mutation, bool allGenesMutableUniformMutation = false, params int[] mutableGenesIndexesUniformMutation)
        {
            switch (mutation)
            {
                case Mutation.FlipBitMutation:
                    _mutation = new GeneticSharp.Domain.Mutations.FlipBitMutation();
                    break;
                case Mutation.UniformMutation:
                    {
                        if (!allGenesMutableUniformMutation && mutableGenesIndexesUniformMutation == null)
                            _mutation = new GeneticSharp.Domain.Mutations.UniformMutation();
                        else if (allGenesMutableUniformMutation && mutableGenesIndexesUniformMutation == null)
                            _mutation = new GeneticSharp.Domain.Mutations.UniformMutation(allGenesMutableUniformMutation);
                        else if (mutableGenesIndexesUniformMutation.Length > 0)
                            _mutation = new GeneticSharp.Domain.Mutations.UniformMutation(mutableGenesIndexesUniformMutation);
                        break;
                    }
                case Mutation.TworsMutation:
                    _mutation = new GeneticSharp.Domain.Mutations.TworsMutation();
                    break;
                case Mutation.ReverseSequenceMutation:
                    _mutation = new GeneticSharp.Domain.Mutations.ReverseSequenceMutation();
                    break;
                default:
                    throw new Exception("Не існує такого виду мутації!");
                    break;
            }
        }

        public void SetTermination(Termination termination, double countIteration)
        {
            switch (termination)
            {
                case Termination.GenerationNumberTermination:
                    if (countIteration > 0)
                        _termination = new GeneticSharp.Domain.Terminations.GenerationNumberTermination((int)Math.Round(countIteration));
                    else
                        _termination = new GeneticSharp.Domain.Terminations.GenerationNumberTermination();
                    break;

                case Termination.FitnessStagnationTermination:
                    if (countIteration > 0)
                        _termination = new GeneticSharp.Domain.Terminations.FitnessStagnationTermination((int)Math.Round(countIteration));
                    else
                        _termination = new GeneticSharp.Domain.Terminations.FitnessStagnationTermination();
                    break;

                case Termination.FitnessThresholdTermination:
                    if (countIteration > 0)
                        _termination = new GeneticSharp.Domain.Terminations.FitnessThresholdTermination(countIteration);
                    else
                        _termination = new GeneticSharp.Domain.Terminations.FitnessThresholdTermination();
                    break;

                case Termination.TimeEvolvingTermination:
                    if (countIteration > 0)
                        _termination = new GeneticSharp.Domain.Terminations.TimeEvolvingTermination(new TimeSpan(0, 0, (int)Math.Round(countIteration)));
                    else
                        _termination = new GeneticSharp.Domain.Terminations.TimeEvolvingTermination();
                    break;

                default:
                    throw new Exception("Не існує такого методу ітерації!");
                    break;
            }
        }


        //Одне скомбіноване рішення
        //Недоречний варіант!!!
        /*public void Start()
        {
            //Сам генетический алгоритм
            var ga = new GeneticSharp.Domain.GeneticAlgorithm(
                _population,
                 _fitness,
                _selection,
                _crossover,
                _mutation);

            ga.Termination = _termination;

            var latestFitness = 0.0;

            ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = ga.BestChromosome as BinaryChromosome;
                var bestFitness = bestChromosome.Fitness.Value;

                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.GetGenes();

                    Console.WriteLine("Поколiння №{0,2}. Кращий результат = {1}", ga.GenerationsNumber, bestFitness);
                    Console.Write("Вид хромосоми: ");

                    foreach (GeneticSharp.Domain.Chromosomes.Gene g in phenotype)
                    {
                        Console.Write(g.Value.ToString() + "");
                    }
                    Console.WriteLine();
                }
            };

            ga.Start();


        }*/

        //Отримати побудований генетичний алгоритм
        public GeneticSharp.Domain.GeneticAlgorithm GetGA
        {
            //Сам генетический алгоритм
            get
            {
                return _ga;
            }
        }


        //Фітнес функція (функція пристосування)
        public GeneticSharp.Domain.Fitnesses.IFitness Fitness
        {
            set { _fitness = value; }
        }

        public void BuildFitnessFunction(ManagerGA managerGA, ConverterToFromChromosome converter)
        {
            _fitness =
            new GeneticSharp.Domain.Fitnesses.FuncFitness(c =>
            {
                var fc = c as BinaryChromosome;
                double result = 0.0;
                string viewChromosome = "";

                foreach (GeneticSharp.Domain.Chromosomes.Gene gene in fc.GetGenes())
                {
                    viewChromosome += gene.Value.ToString();
                }

                int[] solutionsNumber = converter.ConvertFromChromosome(viewChromosome, managerGA.GetCountFunctions);

                result = Convert.ToDouble(managerGA.FitnessFunction(solutionsNumber));


                return result;
            });
        }

        public void BuildGA()
        {
            _ga = new GeneticSharp.Domain.GeneticAlgorithm(
                        _population,
                        _fitness,
                        _selection,
                         _crossover,
                        _mutation);
            _ga.Termination = _termination;
        }

        //Оптимальний варіант 
        //Подумати як передати туди контрол з форми і який!!!
        //Можливо навіть таблицю, щоби можно було виводити по шапці стовбцям назви функцій
        //а по строкам ії рішення і суммарну оцінку фітнесс функції
        public void GenerationRan(Dictionary<int, double> generationBestValueDict, Dictionary<int, string> generationBestChromosomeDict)
            //ConverterToFromChromosome converter)//GeneticSharp.Domain.GeneticAlgorithm ga)
        {
            var latestFitness = 0.0;

            _ga.GenerationRan += (sender, e) =>
            {
                var bestChromosome = _ga.BestChromosome as BinaryChromosome;
                var bestFitness = bestChromosome.Fitness.Value;

                if (bestFitness != latestFitness)
                {
                    latestFitness = bestFitness;
                    var phenotype = bestChromosome.GetGenes();

                    //Console.WriteLine("Поколiння №{0,2}. Кращий результат = {1}", _ga.GenerationsNumber, bestFitness);
                    //Console.Write("Вид хромосоми: ");
                    
                    string viewChromosome = "";
                    foreach (GeneticSharp.Domain.Chromosomes.Gene g in phenotype)
                    {
                        viewChromosome += g.Value.ToString();
                        
                    }
                   // Console.Write(viewChromosome);
                   // Console.WriteLine();

                    generationBestValueDict.Add(_ga.GenerationsNumber, bestFitness);
                    generationBestChromosomeDict.Add(_ga.GenerationsNumber, viewChromosome);
                    
                }
            };
        }

        //Запустити генетичний алгоритм
        public void Start()//GeneticSharp.Domain.GeneticAlgorithm ga) 
            => _ga.Start();


        //Показати стан роботи алгоритму
        public bool IsRunning => _ga.IsRunning;
    }
}
