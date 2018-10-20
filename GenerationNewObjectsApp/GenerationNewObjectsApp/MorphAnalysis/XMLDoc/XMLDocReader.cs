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
            var items = from xe in doc.Element("GA").Elements("resultGA")
                        select new BestResultGA
                        {
                            ProjectName = xe.Element("project").Value,
                            Estimate = Convert.ToDouble(xe.Element("result").Element("estimate").Value),
                            MaxPopulation = Convert.ToInt32(xe.Element("settings").Element("population").Element("max").Value),
                            CountFunction = Convert.ToInt32(xe.Element("result").Element("functions").Attribute("count").Value),
                            BestEpoch = Convert.ToInt32(xe.Element("result").Element("generationNumber").Value)
                        };
        }

        //TODO: Получить группирированный словарь по проектам с оценками проектов
    }
}
