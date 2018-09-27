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
        public bool AddParameterGoalToList(ParametersGoal paramGoal)
        {
            if (paramGoal == null) return false;
            if (FindSimiliarParameterOfGoal(parametersGoals, paramGoal)) return false;
            return CanAdd<ParametersGoal>(parametersGoals, paramGoal);
        }

        //Знаходження ідентичних функції та її рішення в списку
        private bool FindSimiliarParameterOfGoal(List<ParametersGoal> list, ParametersGoal param)
        {
            foreach (ParametersGoal item in list)
            {
                if (item.id_parameter == param.id_parameter && item.Goal.id_goal == param.Goal.id_goal )
                   // && item.name == param.name && item.Goal.name == param.Goal.name)
                    return true;
            }
            return false;
        }

        #endregion

        #endregion

        #region отримання списків

        public List<Function> getListFunction
        {
            get
            {
                if (funcList.Count > 0)
                    return funcList;
                else
                    throw new Exception("Помилка! Пустий список функцій!");
            }
        }


        public List<SolutionsOfFunction> getListSolutionOfFunction
        {
            get
            {
                if (solOfFuncList.Count > 0)
                    return solOfFuncList;
                else
                    throw new Exception("Помилка! Пустий список функцій та їх рішень!");
            }
        }

        public List<Function> getListFunctionMorphTable
        {
            get
            {
                if (funcListMorphTable.Count > 0)
                    return funcListMorphTable;
                else
                    throw new Exception("Помилка! Пустий список функцій для передачі в морфологічну таблицю!");
            }
        }

        public List<SolutionsOfFunction> getListSolutionOfFunctionMorphTable
        {
            get
            {
                if (solOfFuncListMorphTable.Count > 0)
                    return solOfFuncListMorphTable;
                else
                    throw new Exception("Помилка! Пустий список функцій та їх рішень для передачі в морфологічну таблицю!");
            }
        }


        public List<Goal> getListGoal
        {
            get
            {
                if (goals.Count > 0)
                    return goals;
                else
                    throw new Exception("Помилка! Пустий список цілей!");
            }
        }

        public List<ParametersGoal> getListParameterGoal
        {
            get
            {
                if (parametersGoals.Count > 0)
                    return parametersGoals;
                else
                    throw new Exception("Помилка! Пустий список параметрів цілей");
            }
        }

        #endregion
    }
}
