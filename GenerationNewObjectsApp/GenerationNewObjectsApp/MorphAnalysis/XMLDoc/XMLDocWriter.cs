using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MorphAnalysis.XMLDoc
{
    class XMLDocWriter
    {
        private string pathFile = @"resultGA.xml";

        public void AddRecordToXMLDoc(string path, string projectName, Dictionary<string, string> settingsGA, DataGridView dgv)
        {
            string pathFile = path;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(pathFile);

            XDocument xdoc;

            //якщо файл існує і > 0, тоді додаємо записи
            if (fileInfo.Exists && fileInfo.Length > 0)
            {
                //root element
                xdoc = XDocument.Load(pathFile);

                int countResultGAElements = xdoc.Element("GA").Elements("resultGA").Count();

                XElement xmlNode = BuildXMLNode(countResultGAElements, projectName, settingsGA, dgv);

                xdoc.Element("GA").Add(xmlNode);

            }
            //створюємо новий файл
            else
            {
                xdoc = new XDocument();

                //root element
                XElement parentElement = new XElement("GA");

                XElement xmlNode = BuildXMLNode(0, projectName, settingsGA, dgv);

                parentElement.Add(xmlNode);

                xdoc.Add(parentElement);
            }


            xdoc.Save(pathFile);
        }


        private XElement BuildXMLNode(int numberNode, string nameProject, Dictionary<string, string> settingsGA, DataGridView dgv)
        {
            //result GA

            XElement resultGAElement = new XElement("resultGA");

            XAttribute numberResultGAAttr = new XAttribute("number", numberNode);

            resultGAElement.Add(numberResultGAAttr);

            //Отримаємо елементи:
            //назва проекту
            XElement projectPartXML = BuildProjectElement(nameProject);
            //налаштування, які були в ГА
            XElement settingsPartXML = BuildSettingsElement(settingsGA);
            //Найкращий результат роботи ГА
            XElement resultPartXML = BuildResultElement(dgv);

            resultGAElement.Add(projectPartXML);
            resultGAElement.Add(settingsPartXML);
            resultGAElement.Add(resultPartXML);

            return resultGAElement;

        }

        //Будуємо елемент назви проекту
        private XElement BuildProjectElement(string nameProject)
        {

            return new XElement("project", nameProject);
        }


        //Будуємо дерево налаштувань, які були в алгоритмі
        private XElement BuildSettingsElement(Dictionary<string, string> settingsGA)
        {
            XElement settingsElement = new XElement("settings");

            XElement selectionElement = new XElement("selection"),
             crossoverElement = new XElement("crossover"),
             mutationElement = new XElement("mutation"),
             terminationElement = new XElement("termination"),
             populationElement,
             minPopulationElement = new XElement("min"),
             maxPopulationElement = new XElement("max");

            XAttribute attr;

            foreach (KeyValuePair<string, string> kvpSetting in settingsGA)
            {
                //Потрібно знайти налаштування по ключу
                string key = kvpSetting.Key,
                    value = kvpSetting.Value;


                if (key.ToLower().Contains("selection"))
                {
                    selectionElement = new XElement("selection", value);
                    attr = new XAttribute("nameEn", key);
                    selectionElement.Add(attr);
                }
                else if (key.ToLower().Contains("crossover"))
                {
                    crossoverElement = new XElement("crossover", value);
                    attr = new XAttribute("nameEn", key);
                    crossoverElement.Add(attr);
                }
                else if (key.ToLower().Contains("mutation"))
                {
                    mutationElement = new XElement("mutation", value);
                    attr = new XAttribute("nameEn", key);
                    mutationElement.Add(attr);
                }
                else if (key.ToLower().Contains("termination"))
                {
                    terminationElement = new XElement("termination", value);
                    attr = new XAttribute("nameEn", key);
                    XAttribute countTerminationAttr = new XAttribute("countEpochs", settingsGA["countEpochs"]);
                    terminationElement.Add(attr, countTerminationAttr);
                }
                else if (key.ToLower().Contains("min"))
                {
                    minPopulationElement = new XElement("min", value);

                }
                else if (key.ToLower().Contains("max"))
                {
                    maxPopulationElement = new XElement("max", value);
                }
            }



            populationElement = new XElement("population");
            populationElement.Add(minPopulationElement);
            populationElement.Add(maxPopulationElement);

            //Додавання елементів до батіківського узлу
            settingsElement.Add(selectionElement);
            settingsElement.Add(crossoverElement);
            settingsElement.Add(mutationElement);
            settingsElement.Add(terminationElement);
            settingsElement.Add(populationElement);

            return settingsElement;
        }


        //Будуємо дерево найкращого результату
        private XElement BuildResultElement(DataGridView dgv)
        {
            //Блок (дерево) результатів
            XElement result = new XElement("result");

            //Номер найкращого покоління
            XElement generationNumber = new XElement("generationNumber");

            //Функції та їх рішення
            XElement functions = new XElement("functions",
                        new XAttribute("count", dgv.Columns.Count - 2)); //-2 тому, що перший і останній стовбець не пов'язані з назвами функцій

            //Краща оцінка 
            XElement estimate = new XElement("estimate");


            //Отримати кращу строку та словник результатів з неї
            DataGridViewRow row = GetBestRowDGV(dgv);
            Dictionary<string, string> bestRowResult = GetResultRowDGV(row, dgv);

            //Count - 2, тому що, 1 це номер кращого покоління, 2 це фінальна оцінка 
            XElement[] functionArr = new XElement[dgv.Columns.Count - 2];

            int i = 0;
            foreach (var item in bestRowResult)
            {
                if (item.Key == "generationNumber")
                {
                    generationNumber = new XElement("generationNumber", item.Value);
                    continue;
                }

                if (item.Key == "estimate")
                {
                    estimate = new XElement("estimate", item.Value);
                    continue;
                }

                XElement nameFunc = new XElement("name", item.Key);
                XElement solFunc = new XElement("solution", item.Value);
                XElement func = new XElement("function", nameFunc, solFunc);
                functionArr[i++] = func;
            }

            //СОбрать и вернуть результат
            result.Add(generationNumber);
            foreach (var func in functionArr)
            {
                functions.Add(func);
            }
            result.Add(functions);
            result.Add(estimate);

            return result;
        }


        //Отримати найкращий результат (макс. оцінка)
        private DataGridViewRow GetBestRowDGV(DataGridView dgv)
        {
            var dgvRows = dgv.Rows.Cast<DataGridViewRow>();

            double temp;

            //DataGridViewRow bestRow = dgvRows.Max(r => double.TryParse(r.Cells[dgv.Columns.Count - 1].Value.ToString(), out temp)
            //                            ? r : null);



            temp = dgvRows.Max(r => double.TryParse(r.Cells[dgv.Columns.Count - 1].Value.ToString(), out temp)
                                         ? temp : 0);

            var query = from row in dgvRows
                        where double.Parse(row.Cells[dgv.Columns.Count - 1].Value.ToString()) == temp
                        select row;

            return query.First();
            //return bestRow;

        }


        //Отримати результати у вигляді словника з рядку
        private Dictionary<string, string> GetResultRowDGV(DataGridViewRow row, DataGridView dgv)
        {
            //результат рядку: номер покоління, функції та їх рішення, результат
            Dictionary<string, string> funcSolDict = new Dictionary<string, string>();

            string resultCell;


            for (int i = 0; i < row.Cells.Count; i++)
            {
                resultCell = row.Cells[i].Value.ToString();

                if (i == 0)
                {
                    funcSolDict.Add("generationNumber", resultCell);
                    continue;
                }
                if (i == (row.Cells.Count - 1))
                {
                    funcSolDict.Add("estimate", resultCell);
                    continue;
                }

                //Кількість комірок == кількості стовбців в таблиці
                //Додаємо назву функції (стовбець) та рішення (комірка)
                funcSolDict.Add(dgv.Columns[i].HeaderText, resultCell);

            }

            return funcSolDict;
        }

    }
}
