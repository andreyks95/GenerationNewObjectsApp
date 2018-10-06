using MorphAnalysis.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.GeneticAlgorithm
{
    class ConverterToFromChromosome
    {
        //Под вопросом!!!
        //Словник функцій, який зберігає в собі словник рішень та їх оцінок з морфологічної таблиці
        //private Dictionary<int, Dictionary<int, decimal>> _dictFuncSolValue;

        //Нужно сохранить оценки модификаций, оценки решений без модификаций

         //Отримаємо оцінки
        private FinalSolutionEstimate finalEstimates = FinalSolutionEstimate.GetInstance();

        //Отримаємо перелік функцій з морф таблиці
        private CacheData cacheData = CacheData.GetInstance();

        //Рішення відносно функцій 
        private Dictionary<int, decimal> _solutionWithFunction;

        //Рішення відносно параметрів
        private Dictionary<int, decimal> _solutionWithParameters;

        //модифікації
        private Dictionary<int, decimal> _modification;

        //список функцій 
        private List<Function> _funcList;
   


        public ConverterToFromChromosome()
        {
            //_dictFuncSolValue = new Dictionary<int, Dictionary<int, decimal>>();
            //
            ////Приклад роботи
            //var _dict = new Dictionary<int, decimal>();
            //_dict.Add(4, 5);
            //
            //_dictFuncSolValue.Add(4, _dict);
            //

            //Кількість елементів між _solutionWithFunction == _solutionWithParameters

            _solutionWithFunction = finalEstimates.GetEstimatesBy<SolutionsOfFunction>();

            _solutionWithParameters = finalEstimates.GetEstimatesBy<ParametersGoalsForSolution>();

            _modification = finalEstimates.GetEstimatesBy<Modification>();

            _funcList = cacheData.GetListElements<Function>(true);
        }


        public void CreatePseudoCromosomeString()
        {
            //где: f - функция, s - решение функции, m - модификация
            string[] chainString = new[] { "f1", "s1", "m1", "m2", "m3", "s2", "m1", "m2", "m3", "f2", "s1", "m1", "m2", "m3", "s2", "m1", "m2", "m3" };

            //представление хромосомы без учёта функции, необходимо при оценивании восстановить их порядок
            //BitArray bitArr = new BitArray(new bool[] { true, false, false, false, true, false, false, true, false, false, true, true, false, true, true, false });

            //или будет возвращаться как строка, но также без учёта функции
            string str = "0101101101110011"; //"0:101 1:011|0:111 0:011" // | - разделитель между функциями : - разделитель между решениями

            //Если длина подцепочки в функции фиксированая, то строку пожно разбить ровно на некоторые промежутки
            //В данном примере 0101
            // при этом 1 символ будет соответсвовать решению, остальные модификации

            string[] fName = BuildFunctionString();

        }

        private string[] BuildFunctionString()
        {
            string[] functionsId = new string[_funcList.Count];

            //foreach (Function func in _funcList)
            //{
            //    func.id_function.ToString();
            //}

            for(int i=0; i < _funcList.Count; i++)
            {
                functionsId[i] = "f" + _funcList[i].id_function.ToString();
            }
            return functionsId;
        }
    }
}
