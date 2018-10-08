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
