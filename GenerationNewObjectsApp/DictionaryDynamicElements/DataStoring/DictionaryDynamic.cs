using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStoring
{
    public class DictionaryDynamic
    {

        private Dictionary<string, dynamic> _dictionary;

        private static DictionaryDynamic instance;

        private DictionaryDynamic()
        {
            _dictionary = new Dictionary<string, dynamic>();
        }

        public static DictionaryDynamic GetInstance()
        {
            if (instance == null)
                instance = new DictionaryDynamic();
            return instance;
        }

        public bool AddElement<T>(T element, bool forEvaluationTable = false)
        {
            if (element == null) return false;

            string nameKey = GetKeyDictionary(GetType<T>(), forEvaluationTable);

            bool success = false;

            if (_dictionary.ContainsKey(nameKey))
            {
                if (FindSimiliarElement<T>(element, nameKey))
                    return false;
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

                for(int i=0; i < itemProps.Length; i++)
                {
                    string propName = itemProps[i].Name.ToLower();
                    if(propName == "weight" || propName == "avg" || propName == "rating") continue;

                    string valueElement = elementProps[i].GetValue(element).ToString();
                    string valueItem = itemProps[i].GetValue(item).ToString();
                    //якщо значення властивостей співпадають, отже елемент дублюється
                    if (valueElement == valueItem)
                        return true;
                }
            }
            return false;
        }

        private bool HasListAnyElements<T>(List<T> list)
        {
            if (list == null) return false;
            else if (list.Count > 0) return true;
            else return false;
        }

    }
}
