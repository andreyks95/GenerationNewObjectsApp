using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MorphAnalysis.XMLDoc
{
    class XMLDocReader
    {

        private BestResultGA bestResultGAClass = new BestResultGA();

        private string path;

        public XMLDocReader(string path)
        {
            this.path = path;
        }

        private void GetResults()
        {

        }

        //завантажуємо xml-документ, якщо такий є
        private XDocument LoadXMLDoc(string path)
        {
            XDocument doc;
            try
            {
               doc = XDocument.Load(path);
                return doc;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не вдалося завантажити XML-документ \n" + ex.Message);
                return null;
            }
        }

        //отримаємо дані по кожному проекту
        private void QueryResults(XDocument doc)
        {
            var BestResultGACollection = from xe in doc.Element("GA").Elements("resultGA")
                        select new BestResultGA
                        {
                            //Назва проекту
                            ProjectName = xe.Element("project").Value,
                            //Найкраща оцінка проекту
                            Estimate = Convert.ToDouble(xe.Element("result").Element("estimate").Value),
                            //Максимальний розмір популяції
                            MaxPopulation = Convert.ToInt32(xe.Element("settings").Element("population").Element("max").Value),
                            //Кількість функцій
                            CountFunction = Convert.ToInt32(xe.Element("result").Element("functions").Attribute("count").Value),
                            //Мінімальне покоління при якому був отриманий найкращий результат 
                            BestEpoch = Convert.ToInt32(xe.Element("result").Element("generationNumber").Value)
                        };

            var result = from resultGA in BestResultGACollection
                         group resultGA by resultGA.ProjectName into r
                         select new
                         {
                             ProjectName = r.Key,
                             BestResultGA = from item in r select item
                         };

            foreach (var group in result)
            {
                Console.WriteLine("{0}", group.ProjectName);
                foreach (BestResultGA resultGA in group.BestResultGA)
                    Console.WriteLine(resultGA.BestEpoch + " " + resultGA.CountFunction + " " + resultGA.Estimate + " " + resultGA.MaxPopulation);
                Console.WriteLine();
            }

        }

        //TODO: Получить группирированный словарь по проектам с оценками проектов
    }
}
