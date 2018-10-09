using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.GeneticAlgorithm
{
    class ManagerGA
    {
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


        private  ManagerGA()
        {

        }

        public static ManagerGA GetInstance()
        {
            if (instance == null)
                return new ManagerGA();
            return instance;
        }

        //Для збереження оцінки кожного рішення по функції, а також список функцій, список рішень - цікавить їх вага
        public void SetDataForMorphTable(/*int countRows,*/ List<SolutionsOfFunction> solOfFuncList, List<Function> funcList, Dictionary<int, Dictionary<int, decimal>> estimates)
        {
            this.funcList = funcList;

            this.solOfFuncList = solOfFuncList;

            countSolutions = estimates.Keys.Count;//countRows;

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


        public void SetDataForTableParametersGoalsSolutions(Dictionary<int, decimal> dict)//List<ParametersGoalsForSolution> list)
        {
            //parametersGoalsForSolutionsList = list;
            solByParametersDict = dict;
        }

        public void SetDataForTableParametersGoalsModifications(decimal finalEstimateMods)
        {
            modsSum = finalEstimateMods;
        }
    }
}
