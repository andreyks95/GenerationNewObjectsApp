using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework6_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database - Model - Code
            //MyDBExampleForEF6Entities context = new MyDBExampleForEF6Entities();
            //List<Table> list = context.Tables.ToList();
            //foreach (Table item in list)
            //{
            //    Console.WriteLine("{0} {1} - {2}: {3}", item.Id, item.FN, item.LastName, item.Age);
            //}

            //Model - Database - Code
            //using (MFirstContainer container = new MFirstContainer())
            //{
            //    container.PersonalInfoSet.Add(new PersonalInfo { FirstName = "Rootsg", LastName = "A", Age = 27 });
            //    container.PersonalInfoSet.Add(new PersonalInfo { FirstName = "ClickBoost", LastName = "B", Age = 23 });
            //    container.SaveChanges();

            //    var list = container.PersonalInfoSet.ToList();

            //    foreach(var personalInfo in list)
            //    {
            //        Console.WriteLine(personalInfo.Id + "." + personalInfo.FirstName + " " + personalInfo.LastName + " " + personalInfo.Age);
            //    }
            //}

            //Code First - Database
            //CodeFirstModel cfm = new CodeFirstModel();

            //cfm.PersonDatas.Add(new PersonData { FName = "Shubik", LName = "Kabchik", Age = 10 });
            //cfm.SaveChanges();

            //var model = cfm.PersonDatas.ToList();
            //foreach (var item in model)
            //{
            //    Console.WriteLine(item.Id + "." + item.FName + " " + item.LName + " " + item.Age);
            //}

            //Databasse - CodeFirst
            using (CodeFirstFromDatabase cffd = new CodeFirstFromDatabase())
            {
                var list = cffd.Table.ToList();
                foreach (var item in list)
                {
                    Console.WriteLine(item.Id + " " + item.FN);
                }

            }
         }
    }
}
