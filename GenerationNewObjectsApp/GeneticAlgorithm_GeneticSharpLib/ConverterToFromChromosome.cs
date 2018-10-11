using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm_GeneticSharpLib
{

    //Тестовий клас для побудови, оцінювання хромосоми 
    public class ConverterToFromChromosome
    {

        public ConverterToFromChromosome()
        {

        }

        //Бінарний максимальний розмір блоку для розміщення рішень
        public int GetSizeBlock(int countOfSolution)
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

            var arr =  Split(chromosomeAsString, countOfFunction);

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


        //Знайти id рішень в словнику 
        public int[] GetIdSolutions(int[] numbersSolutions, Dictionary<int, decimal> dict)
        {
            int[] ids = new int[numbersSolutions.Length];

            //Так як, прийшли номера рішень (їх індекси в словнику)
            //то потрібно знайти ці рішення (їх id) в словнику по індексу
            for (int i = 0; i < numbersSolutions.Length; i++)
            {
                ids[i] = dict.ElementAt(numbersSolutions[i]).Key;
            }
            return ids;

        }

        /*
        //Побудувати псевдо хромосому
        public string BuildChromosomeString(string[] funcsId, string[] solsId, string[] modsId)
        {

            //example: m1m2m3 block
            string mod = "." + string.Join(".", modsId) + ".";

            //example: s1m1m2m3; s2m1m2m3
            for (int i = 0; i < solsId.Length; i++)
            {
                solsId[i] = "|" + solsId[i] + "|" + mod;
            }

            //example: s1m1m2m3s2m1m2m3 block
            string solsMods = string.Join("", solsId);

            //example f1s1m1m2m3s2m1m2m3; f2s1m1m2m3s2m1m2m3; f3s1m1m2m3s2m1m2m3
            for (int i = 0; i < funcsId.Length; i++)
            {
                funcsId[i] = "-" + funcsId[i] + "-" + solsMods;
            }

            //example f1s1m1m2m3s2m1m2m3f2s1m1m2m3s2m1m2m3f3s1m1m2m3s2m1m2m3 block
            string funcsSolsMods = string.Join("", funcsId);

            return funcsSolsMods;
        }

        //Витягнути всі функції з массиву представлення псведо хромосоми
        public List<string> ParseChromosome(string[] strArr) {


            List<string> listSelectedStrWithFunction = new List<string>();

            foreach (string str in strArr)
            {
                foreach (char c in str)
                {
                    if (c != 'f')
                        continue;
                    else
                        listSelectedStrWithFunction.Add(str);

                }
            }

            return listSelectedStrWithFunction;

        }
        */
    }
}
