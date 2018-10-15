using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.GeneticAlgorithm
{
    public class ManagerGA
    {
        #region Поля класу

        private static ManagerGA instance;

        //для фітнес функції (функція приспособленності)
        //Для назви і ваги функції
        List<Function>                              funcList;

        //для фітнес функції (функція приспособленності)
        //Для назви 
        List<SolutionsOfFunction>                   solOfFuncList;
        //Рішення і його вага
        Dictionary<int, decimal> solWeightDict = new Dictionary<int, decimal>();

        //для розміру бінарного блоку генів в функції
        int                                         countSolutions;

        //для розміру хромосоми. Кількість функцій
        private int countFunctions;

        //для фітнес функції (функція приспособленності)
        //Оцінка кожного рішення згідно функції 
        Dictionary<int, Dictionary<int, decimal>>   solByFuncValuesFromMorphTableDict;

        //для фітнес функції (функція приспособленності)
        //List<ParametersGoalsForSolution> parametersGoalsForSolutionsList;
        //Рішення і його оцінка щодо параметрів
        Dictionary<int, decimal> solByParametersDict;

        //для фітнес функції
        //сума всії модифікацій
        decimal modsSum;

        #endregion

        private ManagerGA()
        {

        }

        public static ManagerGA GetInstance()
        {
            if (instance == null)
                instance = new ManagerGA();
            return instance;
        }

        #region Методи для зберігання даних з таблиць

        //Для збереження оцінки кожного рішення по функції, а також список функцій, список рішень - цікавить їх вага
        public void SetDataForMorphTable(/*int countRows,*/ List<SolutionsOfFunction> solOfFuncList, List<Function> funcList, Dictionary<int, Dictionary<int, decimal>> estimates)
        {
            this.funcList = funcList;

            this.solOfFuncList = solOfFuncList;

            countSolutions = estimates.Keys.Count;//countRows;

            countFunctions = funcList.Count;

            solByFuncValuesFromMorphTableDict = estimates;

            CalcAvgWeightSolById();// solOfFuncList);

        }

        //Розрахунок середнього значення ваги рішення, якщо совпали id в списку рішень та фукнцій
        private void CalcAvgWeightSolById()//List<SolutionsOfFunction> solOfFuncList)
        {
            foreach (var sol in solOfFuncList)
            {
                int key = sol.Solution.id_solution;
                decimal weight = sol.Solution.weight ?? 0;

                if (!solWeightDict.ContainsKey(key))
                {
                    solWeightDict.Add(key, weight);
                }
                //якщо вже є такий ключ
                else
                {
                    decimal oldValue = solWeightDict[key],
                            newValue = weight,
                            avgValue;

                    avgValue = (oldValue + newValue) / 2.0m;
                    solWeightDict[key] = avgValue;
                }
            }
        }

        //Збереження рішень оцінених по параметрам цілей
        public void SetDataForTableParametersGoalsSolutions(Dictionary<int, decimal> dict)//List<ParametersGoalsForSolution> list)
        {
            //parametersGoalsForSolutionsList = list;
            solByParametersDict = dict;
        }
         
        //Збереження сумарної оцінки модифікацій
        public void SetDataForTableParametersGoalsModifications(decimal finalEstimateMods)
        {
            modsSum = finalEstimateMods;
        }

        #endregion

        # region Методи для фітнес функції

        //Методи для фітнес функції
        //Функція 1 | Ф2    | Ф3
        //1010      | 0101  | 0110
        //Р№10      | 5     | 6     -> Номер рішення в словнику

        //Фітнес функція
        public decimal FitnessFunction(int[] numbersSolutions)
        {
            int[] idSolutions = GetIdSolutions(numbersSolutions);

            return CalcChromosome(idSolutions);

        }

        //знаходимо оцінку рішення по 
        //кількість рішень === кількість функцій
        private decimal CalcChromosome(int[] idSols)
        {
            int idFunc = 0;

            int idSol = 0;

            decimal estimateSolution = 0;

            decimal resultFitness = 0;


            for(int i=0; i < idSols.Length; i++)
            {
                //знаходимо іd функції 
                idFunc = funcList[i].id_function;

                //id рішення
                idSol = idSols[i];

                //Розрахуємо рішення
                estimateSolution = CalcSolution(i, idSol, idFunc);

                resultFitness += estimateSolution;

            }
            return resultFitness;

            //funcList[i].weight; //знаходимо вагу функції
            //
            //solWeightDict[idSols[i]]; //знаходимо вагу рішення
            //
            ////Порібно знайти оцінку рішення по конкретній функції
            //solByFuncValuesFromMorphTableDict[idSols[i]][funcList[i].id_function];
            //
            ////Знаходимо оцінку рішення відповідно параметрам по id
            //solByParametersDict[idSols[i]];
            //
            ////Сумарна оцінка модифікацій
            //modsSum;

        }

        //Розрахувати оцінку рішення
        private decimal CalcSolution(int i, int idSol, int idFunc)
        {
            //оцінка рішення = вага рішення * (вага функції * оцінку рішення по конкретній функції) +
            //+ (оцінка рішення по параматерам цілей) + (сукупна оцінка модифікацій);

            //[idSol][idFunc] - отримаємо оцінку рішення за функцією, де: 
            //[idSol] - пошук по id рішення, [idFunc] - пошук по id функції в вкладеному словнику,

            decimal estimateSolution = solWeightDict[idSol] *
                                       (funcList[i].weight ?? 0 * solByFuncValuesFromMorphTableDict[idSol][idFunc]) +
                                        (solByParametersDict[idSol]) + modsSum;


            //funcList[i].weight; //знаходимо вагу функції

            //solWeightDict[idSols[i]]; //знаходимо вагу рішення

            //Порібно знайти оцінку рішення по конкретній функції
            // solByFuncValuesFromMorphTableDict[idSols[i]][funcList[i].id_function];

            //Знаходимо оцінку рішення відповідно параметрам по id
            //solByParametersDict[idSols[i]];

            //Сумарна оцінка модифікацій
            //modsSum;

            return estimateSolution;

        }

        //Знайти id рішень в словнику solByFuncValuesFromMorphTableDict
        private int[] GetIdSolutions(int[] numbersSolutions)
        {
            int[] ids = new int[numbersSolutions.Length];

            //Так як, прийшли номера рішень (їх індекси в словнику)
            //то потрібно знайти ці рішення (їх id) в словнику по індексу
            //Притому врахувати, що в словнику індекси починаються с 0
            //тому numbersSolutions[i]-1

            int indexDict = 0;
      
            for (int i = 0; i < numbersSolutions.Length; i++)
            {
                if (numbersSolutions[i] == 0 || numbersSolutions[i] < 0)
                    indexDict = 0;
                else
                {
                    indexDict = numbersSolutions[i]-1;
                }
                if (indexDict >= solByFuncValuesFromMorphTableDict.Keys.Count)
                    indexDict = solByFuncValuesFromMorphTableDict.Keys.Count-1;

                ids[i] = solByFuncValuesFromMorphTableDict.ElementAt(indexDict).Key;
            }
            return ids;
        }



        #endregion


        #region Властивості для отримання розміру хромосоми

        public int GetCountSolutions => countSolutions;

        public int GetCountFunctions => countFunctions;

        #endregion

    }
}
