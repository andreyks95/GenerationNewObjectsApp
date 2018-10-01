using DataStoring;
using DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryDynamicElements
{
    class Program
    {
        static void Main(string[] args)
        {

            //#1 variant
            //string nspace = "...";
            //var q = from t in Assembly.GetExecutingAssembly().GetTypes()
            //        where t.IsClass && t.Namespace == nspace
            //        select t;
            //q.ToList().ForEach(t => Console.WriteLine(t.Name));

            //#2 variant
            //Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            //
            // foreach(Type type in types)
            // {
            //     if (type.IsClass && type.Namespace == "DataStoring")
            //         Console.WriteLine(type.Name + " in namespace:" + type.Namespace);
            // }

            DictionaryDynamic dict = DictionaryDynamic.GetInstance();

            Goal fe = new Goal() { id_goal = 1, name = "firstGoal", weight = 1 };
            Goal se = new Goal() { id_goal = 2, name = "SecondGoal", weight = 1.1m };
            Goal te = new Goal() { id_goal = 2, name = "OtherMethod", weight = 1.1m };

            dict.AddElement<Goal>(fe);
            dict.AddElement<Goal>(se);
            dict.AddElement<Goal>(te, true);

            dict.AddElement<Function>(new Function { id_function = 1, name = "firstFunc", weight = 1 });
            dict.AddElement<Function>(new Function { id_function = 2, name = "secondFunc", weight = 2 });

            try
            {
                foreach (var item in dict.GetListElements<Goal>())
                {
                    Console.WriteLine(item.id_goal + " " + item.name + " " + item.weight);
                }
                Console.WriteLine("----------------------------");
                foreach (var item in dict.GetListElements<Goal>(true))
                {
                    Console.WriteLine(item.id_goal + " " + item.name + " " + item.weight);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.InnerException);
            }

            Console.ReadKey();
        }
    }
}
