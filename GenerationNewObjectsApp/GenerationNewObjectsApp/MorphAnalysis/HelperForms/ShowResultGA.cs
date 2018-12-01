using MorphAnalysis.GeneticAlgorithm;
using MorphAnalysis.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MorphAnalysis.HelperForms
{
    public partial class ShowResultGA : Form
    {
        //для зберігання номерів  поколінь, їх кращих хромосом та оцінок
        private Dictionary<int, double> resultGAValueDict; //= new Dictionary<int, double>();
        private Dictionary<int, string> resultGAChromosomeDict; //= new Dictionary<int, string>();

        private CacheData cacheData = CacheData.GetInstance();

        List<SolutionsOfFunction> solOfFuncList; //= cacheData.GetListElements<SolutionsOfFunction>(true);

        List<Function> funcList; //= cacheData.GetListElements<Function>(true);

        //Для конвертації з хромосоми та отримання id рішень
        ConverterToFromChromosome converter = new ConverterToFromChromosome();

        ManagerGA managerGA = ManagerGA.GetInstance();

        //для налаштувань
        Dictionary<string, string> settingsGA;

        public ShowResultGA()
        {
            InitializeComponent();
        }


        public ShowResultGA(Dictionary<int, double> resultValueGADict, Dictionary<int, string> resultChromosomeGADict,
            Dictionary<string, string> settingsGA) : this()
        {
            resultGAValueDict = resultValueGADict;
            resultGAChromosomeDict = resultChromosomeGADict;

            solOfFuncList = cacheData.GetListElements<SolutionsOfFunction>(true);
            funcList = cacheData.GetListElements<Function>(true);

            this.settingsGA = settingsGA;
        }

        private void ShowResultGA_Load(object sender, EventArgs e)
        {
            //Будуємо стовбці

            resultGA_DGV.Columns.Add("NPop", "№ Покоління");

            foreach (Function func in funcList)
            {
                resultGA_DGV.Columns.Add("F" + func.id_function, func.name);
            }

            resultGA_DGV.Columns.Add("Result", "Оцінка");

            //Будуємо рядки

            foreach (KeyValuePair<int, double> kvp in resultGAValueDict)
            {
                string[] row = new string[resultGA_DGV.Columns.Count];

                int i = 0;

                //заповнюємо 1-шу комірку рядка
                int numberGeneration = kvp.Key;
                row[i++] = numberGeneration.ToString();

                string viewChromosome = resultGAChromosomeDict[kvp.Key];
                int[] solutionsNumber = converter.ConvertFromChromosome(viewChromosome, funcList.Count);
                int[] idSolutions = managerGA.GetIdSolutions(solutionsNumber);

                foreach (int idSol in idSolutions)
                {
                    string nameSolution = solOfFuncList.FirstOrDefault(sf => sf.Solution.id_solution == idSol).Solution.name;
                    row[i++] = nameSolution;
                }

                //заповнюємо останню комірку рядка
                double resultValue = kvp.Value;
                row[i++] = resultValue.ToString();

                resultGA_DGV.Rows.Add(row);
            }


        }

        private void saveToFileButton_Click(object sender, EventArgs e)
        {
            string pathFile = @"resultGA.xml";
            string projectName = projectNameComboBox.Text.Trim();
            if (projectName == null || projectName == "")
                projectName = "NoName";
            try
            {
                XMLDoc.XMLDocWriter doc = new XMLDoc.XMLDocWriter();
                doc.AddRecordToXMLDoc(pathFile, projectName, settingsGA, resultGA_DGV);
                MessageBox.Show("Найкращий результат роботи генетичного алгоритму збережено в xml-файл: resultGA.xml");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Виникла помилка під час запису xml-файлу" + "\n" + ex.Message);
            }
        }

    }
}
