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
            if (element == null) return false;
            if (!list.Contains(element))
            {
                list.Add(element);
                return true;
            }
            return false;
        }

        #region Додавання рішень та функцій

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


        #endregion

        public bool AddElementToList<T>(T element, bool forEvaluationTable = false)
        {
            //Реализовать метод
            if (element == null) return false;

            bool returnResult;
            string nameClass = typeof(T).Name;
            switch (nameClass)
            {
                case nameof(Function):
                    if (forEvaluationTable)
                        returnResult = CanAdd<Function>(funcListMorphTable, element as Function);
                    else
                        returnResult = CanAdd<Function>(funcList, element as Function);
                    break;

                case nameof(SolutionsOfFunction):
                    {
                        List<SolutionsOfFunction> list = null;

                        if (forEvaluationTable)
                            list = solOfFuncListMorphTable;
                        else
                            list = solOfFuncList;

                        if (FindSimiliarSolutionOfFunction(list, element as SolutionsOfFunction))
                            returnResult = false;
                        else
                            returnResult = CanAdd<SolutionsOfFunction>(list, element as SolutionsOfFunction);

                        break;
                    }

                case nameof(Goal):
                    returnResult = CanAdd<Goal>(goals, element as Goal);
                    break;

                case nameof(ParametersGoal):
                    {
                        List<ParametersGoal> list = null;

                        if (forEvaluationTable)
                            list = parametersGoalsForTables;
                        else
                            list = parametersGoals;

                        if (FindSimiliarParameterOfGoal(list, element as ParametersGoal))
                            returnResult = false;
                        else
                            returnResult = CanAdd<ParametersGoal>(list, element as ParametersGoal);

                        break;
                    }

                case nameof(ParametersGoalsForSolution):
                    returnResult = CanAdd<ParametersGoalsForSolution>(parametersGoalsForSolutionsList, element as ParametersGoalsForSolution);
                    break;

                default:
                    returnResult = false;
                    break;
            }
            return returnResult;
        }

        #region Отримання списків
        //Отримання списків в залежності від класу 
        //а також в залежності для яких таблиць: морфологічних / оціночних / кросс таблиць
        //чи звичайних для експертів 

        private bool HasListAnyElements<T>(List<T> list)
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
            if (HasListAnyElements(returnList))
                return returnList;
            else
                throw new Exception("Помилка! Пустий список елементів " + n);
        }

        #endregion

    }
}
