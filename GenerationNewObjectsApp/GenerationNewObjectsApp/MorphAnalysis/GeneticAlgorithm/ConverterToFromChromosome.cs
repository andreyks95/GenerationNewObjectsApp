using MorphAnalysis.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.GeneticAlgorithm
{
    public class ConverterToFromChromosome
    {
        //Під питанням!!
        //Словник функцій, який зберігає в собі словник рішень та їх оцінок з морфологічної таблиці
        //private Dictionary<int, Dictionary<int, decimal>> _dictFuncSolValue;

        //Нужно сохранить оценки модификаций, оценки решений без модификаций

         //Отримаємо оцінки
        //private FinalSolutionEstimate finalEstimates = FinalSolutionEstimate.GetInstance();
        //
        ////Отримаємо перелік функцій з морф таблиці
        //private CacheData cacheData = CacheData.GetInstance();
        //
        ////Рішення відносно функцій 
        //private Dictionary<int, decimal> _solutionWithFunction;
        //
        ////Рішення відносно параметрів
        //private Dictionary<int, decimal> _solutionWithParameters;
        //
        ////модифікації
        //private Dictionary<int, decimal> _modification;
        //
        ////список функцій 
        //private List<Function> _funcList;
   


        public ConverterToFromChromosome()
        {
            //Кількість елементів між _solutionWithFunction == _solutionWithParameters

            //_solutionWithFunction   = finalEstimates.GetEstimatesBy<SolutionsOfFunction>();
            //
            //_solutionWithParameters = finalEstimates.GetEstimatesBy<ParametersGoalsForSolution>();
            //
            //_modification = finalEstimates.GetEstimatesBy<Modification>();
            //
            //_funcList = cacheData.GetListElements<Function>(true);
        }

        //Бінарний максимальний розмір блоку для розміщення рішень
        private int GetSizeBlock(int countOfSolution)
        {
            string str = Convert.ToString(countOfSolution, 2);
            return str.Length;
        }

        //Бінарний розмір хромосоми (блок * кількість функцій)
        public int GetSizeChromosome(int countOfSolution, int countOfFunction)
        {
            int sizeBlock = GetSizeBlock(countOfSolution);
            return sizeBlock * countOfFunction;
        }

        //Повертає масив НОМЕРІВ рішень (а не ID рішень) для словника, з розміру хромосоми 
        public int[] ConvertFromChromosome(string chromosomeAsString, int countOfFunction)
        {
            int sizeChromosome = chromosomeAsString.Length; //Convert.ToInt32(chromosomeAsString, 2);

            //int blockLength = sizeChromosome / countOfFunction;

            //кількість функцій = кількості рішень
            int[] solutionsNumber = new int[countOfFunction];

            var arr = Split(chromosomeAsString, countOfFunction);

            int i = 0;
            foreach (var item in arr)
            {
                solutionsNumber[i++] = Convert.ToInt32(item, 2);
            }

            return solutionsNumber;
        }

        //Для потрібненння хромосоми на рівні шматки
        private IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        //old methods
        #region Старі методи
        /*
        #region Отримати довжину хромосоми

        //Отримати довжину хромосоми
        public int GetChromosomeLength
        {
            
            //Довжина хромосоми повинна вміщувати загальну кількість модифікацій в ній 
            //+ загальну кількість рішень 
            //тому, що деякі рішення можуть реалізуватися = 1, а деякі - ні = 0 
            //приклад: 2 функції, 2 рішення, 2 модифікації => 2*2*2 = 8 мод. + 2*2 = 4 ріш. => 8 + 4 = 12 - довжина хромосоми
            get
            {
                int allModCount = (_funcList.Count * _solutionWithFunction.Count * _modification.Count);
                int allSolCount = (_funcList.Count * _solutionWithFunction.Count);
                int length = allModCount + allSolCount;
                return length;
            }
        }

        #endregion

        #region Методи для побудови псевдо хромосоми 

        public string GetPseudoChromosomeString 
        {
            //или будет возвращаться как строка, но также без учёта функции
            //string str = "0101101101110011"; //"0:101 1:011|0:111 0:011" // | - разделитель между функциями : - разделитель между решениями

            //Если длина подцепочки в функции фиксированая, то строку пожно разбить ровно на некоторые промежутки
            //В данном примере 0101
            // при этом 1 символ будет соответсвовать решению, остальные модификации
            get
            {
                //f1; f2; ...
                string[] funcsId = BuildPartChromosomeFunctionString();
                //s1; s1; ...
                string[] solsId = BuildPartChromosomeSolutionString();
                //m1; m2; ...
                string[] modsId = BuildPartChromosomeModificationString();
                //f1s1m1m2m3s2m1m2m3f2s1m1m2m3s2m1m2m3 ... block
                string result = BuildChromosomeString(funcsId, solsId, modsId);
                return result;
            }

        }

        
        //Побудувати псевдо хромосому
        private string BuildChromosomeString(string[] funcsId, string[] solsId, string[] modsId)
        {

            //example: m1m2m3 block
            string mod = string.Join("", modsId);

            //example: s1m1m2m3; s2m1m2m3
            for (int i=0; i < solsId.Length; i++)
            {
                solsId[i] = solsId[i] + mod;
            }

            //example: s1m1m2m3s2m1m2m3 block
            string solsMods = string.Join("", solsId);

            //example f1s1m1m2m3s2m1m2m3; f2s1m1m2m3s2m1m2m3; f3s1m1m2m3s2m1m2m3
            for (int i=0; i < funcsId.Length; i++)
            {
                funcsId[i] = funcsId[i] + solsMods;
            }

            //example f1s1m1m2m3s2m1m2m3f2s1m1m2m3s2m1m2m3f3s1m1m2m3s2m1m2m3 block
            string funcsSolsMods = string.Join("", funcsId);

            return funcsSolsMods;
        }

        #region Створити частини для представлення псевдо хромосоми у вигляді рядку

        //example: f1; f2; f2
        private string[] BuildPartChromosomeFunctionString()
        {
            string[] functionsId = new string[_funcList.Count];

            //example: f1; f2; f2
            for(int i=0; i < _funcList.Count; i++)
            {
                functionsId[i] = "f" + _funcList[i].id_function.ToString();
            }
            return functionsId;
        }

        private string[] BuildPartChromosomeSolutionString()
        {
            string[] solutionsId = new string[_solutionWithFunction.Count]; //_solutionWithParameters.Count           

            int i = 0;
            //example: s1; s2; s3
            //key it's id
            foreach (KeyValuePair<int, decimal> solution in _solutionWithFunction)
            {
                solutionsId[i++] = "s" + solution.Key.ToString();
            }
            return solutionsId;
        }

        private string[] BuildPartChromosomeModificationString()
        {
            string[] modificationsId = new string[_modification.Count];

            int i = 0;
            //example: m1, m2, m2
            //key it's id 
            foreach (KeyValuePair<int, decimal> modification in _modification)
            {
                modificationsId[i++] = "m" + modification.Key.ToString();
            }

            return modificationsId;
        }

        #endregion

        #endregion
    */
        #endregion
    }
}
