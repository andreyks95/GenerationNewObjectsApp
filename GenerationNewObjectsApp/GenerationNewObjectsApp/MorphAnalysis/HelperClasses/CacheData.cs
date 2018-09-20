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

        private static CacheData instance;

        #endregion


        private CacheData()
        {
            funcList = new List<Function>();
            solOfFuncList = new List<SolutionsOfFunction>();
        }

        public static CacheData GetInstance()
        {
            if (instance == null)
                instance = new CacheData();
            return instance;
        }

        #region функції додавання елементів в список

        //Додавання функцій в список
        public bool AddFunctionToList(Function func)
        {
            if (func == null) return false;

            funcList.Add(func);
            return true;
        }

        //Додавання функції та її рішення в список
        public bool AddSolutionOfFunctionToList(SolutionsOfFunction solOfFunc)
        {

            if (solOfFunc == null) return false;

            solOfFuncList.Add(solOfFunc);
            return true;
        }

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

        #endregion
    }
}
