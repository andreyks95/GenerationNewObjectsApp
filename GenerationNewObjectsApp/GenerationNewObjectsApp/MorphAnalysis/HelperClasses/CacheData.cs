using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.HelperClasses
{
    //Патерн Singleton
    public class CacheData
    {

        #region поля класу 
        //Кеш для зберігання функцій об'єкту
        private List<Function> funcList;

        //Кеш для зберігання функцій та їх рішень 
        private List<SolutionsOfFunction> solOfFuncList;

        //Кеш для зберігання функцій в морфологічну таблицю
        private List<Function> funcListMorphTable;

        //Кеш для зберігання рішень функцій в морфологічну таблицю
        private List<SolutionsOfFunction> solOfFuncListMorphTable;

        //Кеш для зберігання цілей
        private List<Goal> goals;

        //Кеш для зберігання параметрів цілей
        private List<ParametersGoal> parametersGoals;

        //Кеш для зберігання параметрів цілей та їх функцій для таблиці 
        //оцінювання модифікацій і тех. рішень для параметрів цілей
        private List<ParametersGoal> parametersGoalsForTables;

        //Кеш для зберігання параметрів цілей щодо тех. рішень
        private List<ParametersGoalsForSolution> parametersGoalsForSolutionsList;

        private static CacheData instance;

        #endregion


        private CacheData()
        {
            funcList = new List<Function>();
            solOfFuncList = new List<SolutionsOfFunction>();

            funcListMorphTable = new List<Function>();
            solOfFuncListMorphTable = new List<SolutionsOfFunction>();

            goals = new List<Goal>();
            parametersGoals = new List<ParametersGoal>();

            parametersGoalsForTables = new List<ParametersGoal>();

            parametersGoalsForSolutionsList = new List<ParametersGoalsForSolution>();
        }

        public static CacheData GetInstance()
        {
            if (instance == null)
                instance = new CacheData();
            return instance;
        }


        #region функції додавання елементів в список


        //Узагальнений метод
        public bool CanAdd<T>(List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
                return true;
            }
            return false;
        }

        #region Додавання рішень та функцій

        //Додавання функцій в список
        public bool AddFunctionToList(Function func, bool ToMorphTable = false)
        {
            if (func == null) return false;
            if (ToMorphTable)
                return CanAdd<Function>(funcListMorphTable, func);
            else
                return CanAdd<Function>(funcList, func);
        }

        //Додавання функції та її рішення в список
        public bool AddSolutionOfFunctionToList(SolutionsOfFunction solOfFunc, bool ToMorphTable = false)
        {
            if (solOfFunc == null) return false;

            if (ToMorphTable)
            {
                if (FindSimiliarSolutionOfFunction(solOfFuncListMorphTable, solOfFunc)) return false;
                return CanAdd<SolutionsOfFunction>(solOfFuncListMorphTable, solOfFunc);
            }
            else
            {
                if (FindSimiliarSolutionOfFunction(solOfFuncList, solOfFunc)) return false;
                return CanAdd<SolutionsOfFunction>(solOfFuncList, solOfFunc);
            }
        }

        //Знаходження ідентичних функції та її рішення в списку
        private bool FindSimiliarSolutionOfFunction(List<SolutionsOfFunction> list, SolutionsOfFunction solOfFunc)
        {
            foreach (SolutionsOfFunction item in list)
            {
                if (item.Solution.name == solOfFunc.Solution.name && item.Function.name == solOfFunc.Function.name)
                    return true;
            }
            return false;
        }

        #endregion

        #region Додавання параметрів та цілей

        //Додавання цілей до списку  
        public bool AddGoalToList(Goal goal)
        {
            if (goal == null) return false;
            foreach (Goal g in goals)
            {
                if (g.id_goal == goal.id_goal)
                    return false;
            }
            return CanAdd<Goal>(goals, goal);
        }

        //Додавання параметру цілі до списку
        public bool AddParameterGoalToList(ParametersGoal paramGoal, bool forTables = false)
        {
            if (paramGoal == null) return false;
            if (forTables)
            {
                if (FindSimiliarParameterOfGoal(parametersGoalsForTables, paramGoal)) return false;
                return CanAdd<ParametersGoal>(parametersGoalsForTables, paramGoal);
            }
            else
            {
                if (FindSimiliarParameterOfGoal(parametersGoals, paramGoal)) return false;
                return CanAdd<ParametersGoal>(parametersGoals, paramGoal);
            }
        }

        //Знаходження ідентичних функції та її рішення в списку
        private bool FindSimiliarParameterOfGoal(List<ParametersGoal> list, ParametersGoal param)
        {
            foreach (ParametersGoal item in list)
            {
                if (item.id_parameter == param.id_parameter && item.Goal.id_goal == param.Goal.id_goal)
                    // && item.name == param.name && item.Goal.name == param.Goal.name)
                    return true;
            }
            return false;
        }

        #endregion

        #region Додавання тех. рішень та модифікацій щодо параметрів цілей

        public bool AddParamGoalForSolToList(ParametersGoalsForSolution paramGoalForSol)
        {
            if (paramGoalForSol == null) return false;
            /*foreach(ParametersGoalsForSolution item in parametersGoalsForSolutionsList)
            {
                if (paramGoalForSol.)
                    return false;
            }*/
            return CanAdd<ParametersGoalsForSolution>(parametersGoalsForSolutionsList, paramGoalForSol);
        }

        #endregion

        #endregion

        #region Отримання списків
        //Отримання списків в залежності від класу 
        //а також в залежності для яких таблиць: морфологічних / оціночних / кросс таблиць
        //чи звичайних для експертів 

        private bool HasListElements<T>(List<T> list)
        {
            if (list == null) return false;
            else if (list.Count > 0) return true;
            else return false;
        }

        public List<T> GetList<T>(bool forEvaluationTable = false)
        {
            List<T> returnList = null;
            string n = typeof(T).Name;
            switch (n)
            {
                case nameof(Function):
                    if (forEvaluationTable)
                        returnList = funcListMorphTable.Cast<T>().ToList();
                    else
                        returnList = funcList.Cast<T>().ToList();
                    break;

                case nameof(SolutionsOfFunction):
                    if (forEvaluationTable)
                        returnList = solOfFuncListMorphTable.Cast<T>().ToList();
                    else
                        returnList = solOfFuncList.Cast<T>().ToList();
                    break;

                case nameof(Goal):
                    returnList = goals.Cast<T>().ToList();
                    break;

                case nameof(ParametersGoal):
                    if (forEvaluationTable)
                        returnList = parametersGoalsForTables.Cast<T>().ToList();
                    else
                        returnList = parametersGoals.Cast<T>().ToList();
                    break;

                case nameof(ParametersGoalsForSolution):
                    returnList = parametersGoalsForSolutionsList.Cast<T>().ToList();
                    break;

                default:
                    returnList = null;
                    break;
            }
            if (HasListElements(returnList))
                return returnList;
            else
                throw new Exception("Помилка! Пустий список елементів " + n);
        }

        #endregion

        public bool AddElementToList<T>(T element, bool forEvaluationTable = false)
        {
            //Реализовать метод
            return true;
        }


    }
}
