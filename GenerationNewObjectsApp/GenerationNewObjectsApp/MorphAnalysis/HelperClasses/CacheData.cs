using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.HelperClasses
{
    //Патерн Singleton
    public class CacheData
    {

        private static CacheData instance;

        //Кеш словник для зберігання динамічних списків 
        private Dictionary<string, dynamic> _dictionary;

        private CacheData()
        {
            _dictionary = new Dictionary<string, dynamic>();
        }

        public static CacheData GetInstance()
        {
            if (instance == null)
                instance = new CacheData();
            return instance;
        }

        #region Методи для знаходження ідентичних: елементів, функцій і їх рішень, параметрів цілей

        //Знаходження ідентичних функції та її рішення в списку
        private bool FindSimiliarSolutionOfFunction(List<SolutionsOfFunction> list, SolutionsOfFunction solOfFunc)
        {
            foreach (SolutionsOfFunction item in list)
            {
                if ((item.Solution.name == solOfFunc.Solution.name && item.Function.name == solOfFunc.Function.name)
                     || (item.Solution.id_solution == solOfFunc.Solution.id_solution && item.Function.id_function == solOfFunc.Function.id_function)
                    )
                    return true;
            }
            return false;
        }

        //Знаходження ідентичних параметрів цілей
        private bool FindSimiliarParameterOfGoal(List<ParametersGoal> list, ParametersGoal param)
        {
            foreach (ParametersGoal item in list)
            {
                if ((item.id_parameter == param.id_parameter && item.Goal.id_goal == param.Goal.id_goal))
                    // && item.name == param.name && item.Goal.name == param.Goal.name)
                    return true;
            }
            return false;
        }

        //Знаходження ідентичних функцій та їх рішень
        private bool FindSimiliarParamOrSolOfFunc<T>(List<T> list, T element)
        {
            if (element is SolutionsOfFunction)
            {
                if (FindSimiliarSolutionOfFunction(list.Cast<SolutionsOfFunction>().ToList(), element as SolutionsOfFunction))
                    return true;
            }
            else if (element is ParametersGoal)
            {
                if (FindSimiliarParameterOfGoal(list.Cast<ParametersGoal>().ToList(), element as ParametersGoal))
                    return true;
            }
            return false;
        }

        //Знаходження ідентичних елементів
        private bool FindSimiliarElement<T>(T element, string nameKeyDictionary)
        {
            string nameKey = nameKeyDictionary;
            List<T> list = _dictionary[nameKey];
            if (list == null) return false;

            Type typeAddElement = element.GetType();
            PropertyInfo[] elementProps = typeAddElement.GetProperties(); //BindingFlags.Public); // | BindingFlags.Instance);

            foreach (T item in list)
            {
                Type typeItem = item.GetType();
                PropertyInfo[] itemProps = typeItem.GetProperties();//BindingFlags.Public); // | BindingFlags.Instance);

                if (elementProps.Length != itemProps.Length)
                    return false;

                for (int i = 0; i < itemProps.Length; i++)
                {
                    string propName = itemProps[i].Name.ToLower();
                    if (propName == "weight" || propName == "avg" || propName == "rating") continue;
                    if (itemProps[i].PropertyType.IsInterface || itemProps[i].PropertyType.IsGenericType) continue;

                    string valueElement = elementProps[i].GetValue(element)?.ToString(); //?? null;
                    string valueItem = itemProps[i].GetValue(item)?.ToString();// ?? null;

                    if (valueElement is null || valueItem is null)
                        continue;
                    //якщо значення властивостей співпадають, отже елемент дублюється
                    if (valueElement == valueItem)
                        return true;
                }
            }
            return false;
        }

        #endregion

        public bool AddElement<T>(T element, bool forEvaluationTable = false)
        {
            if (element == null) return false;

            string nameKey = GetKeyDictionary(GetType<T>(), forEvaluationTable);

            bool success = false;

            if (_dictionary.ContainsKey(nameKey))
            {
                if (element is SolutionsOfFunction || element is ParametersGoal)
                {
                    if (FindSimiliarParamOrSolOfFunc<T>(_dictionary[nameKey], element))
                        return false;
                }
                else if (element is ParametersGoalsForSolution)
                {
                    success = true;
                }
                else
                {
                    if (FindSimiliarElement<T>(element, nameKey))
                        return false;
                }
                _dictionary[nameKey].Add(element);
                success = true;
            }
            else
            {
                _dictionary.Add(nameKey, new List<T>());
                _dictionary[nameKey].Add(element);
                success = true;
            }
            return success;
        }
       
        //Отримання списків в залежності від класу 
        //а також в залежності для яких таблиць: морфологічних ( оціночних | крос таблиць)
        //чи звичайних для експертів 
        public List<T> GetListElements<T>(bool forEvaluationTable = false)
        {
            string nameKey = GetKeyDictionary(GetType<T>(), forEvaluationTable);

            if (HasListAnyElements(_dictionary[nameKey]))
                return _dictionary[nameKey];
            else
                throw new Exception("Помилка! Пустий список елементів " + nameKey);
        }

        //Дозволяє отримати тип елементу що додається
        private Type GetType<T>()
        {
            var typesQuery = from type in Assembly.GetExecutingAssembly().GetTypes()
                             where type.IsClass// && type.Name == typeof(T).Name
                             select type;

            List<Type> typeList = typesQuery.ToList();

            Type findType = typeList.FirstOrDefault(t => t.Name == typeof(T).Name);

            return findType;
        }

        //Метод для отримання ключа для словника
        private string GetKeyDictionary(Type type, bool forEvaluationTable = false)
        {
            string nameKey = type.Name;
            if (forEvaluationTable)
            {
                nameKey += "_v"; //class name + value (for EvaluationTable) // клас для оціночних таблиць
            }
            return nameKey;
        }

        private bool HasListAnyElements<T>(List<T> list)
        {
            if (list == null) return false;
            else if (list.Count > 0) return true;
            else return false;
        }


    }
}
