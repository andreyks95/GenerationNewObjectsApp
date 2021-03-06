﻿using MorphAnalysis.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorphAnalysis.TablesExpertEvaluation
{
    public partial class MorphTable : Form
    {

        private CacheData cacheData = CacheData.GetInstance();

        private ConfigDGV configDGV;

        private List<Function> funcList;

        private List<SolutionsOfFunction> solOfFuncList;

        //Для тимчасового зберігання назв рішень
        private string solutionNameInTable;

        //Для тимчасового зберігання назв функції
        private string functionNameInTable;

        List<decimal> ratings = new List<decimal>();


        //для конфігурації мат. моделей. 
        //Розрахувати оцінку з / без урахуванням ваги тех. рішення

        bool useWeightSolution = false;

        public MorphTable()
        {
            InitializeComponent();

            solOfFuncList = cacheData.GetListElements<SolutionsOfFunction>(true);

            funcList = cacheData.GetListElements<Function>(true);

            configDGV = new ConfigDGV();
        }

        public MorphTable(bool useWeightSolution = true):this()
        {
            this.useWeightSolution = useWeightSolution;
        }

        public bool UseWeightSolution
        {
            set { this.useWeightSolution = value; }
        }

        public decimal SetWeightMathModel
        {
            private get;
            set;
        } = 1;

        private void MorphTable_Load(object sender, EventArgs e)
        {
            //Наповнюємо стовбцями таблицю
            dataGridView1.Columns.Add("FirstCol", "Тех. рішення / Функції ");

            int i = 0;
            foreach (Function func in funcList)
            {
                dataGridView1.Columns.Add("F" + ++i, func.name);
            }
            dataGridView1.Columns.Add("Sum", "Оцінка");
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = false;

            //Стовбці тільки для читання
            configDGV.SetColumnsDgvOnlyRead(dataGridView1, new[] { 0, dataGridView1.Columns.Count - 1 });

            //Наповнюємо рядками таблицю
            foreach (SolutionsOfFunction solOfFunc in solOfFuncList)
            {
                dataGridView1.Rows.Add(solOfFunc.Solution.name + " & " + solOfFunc.Function.name);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int value;
            configDGV.CellValidating(dataGridView1, sender, e);
            if (int.TryParse(Convert.ToString(e.FormattedValue), out value))
                if (!(value >= 0 && value <= 100))
                {
                    MessageBox.Show("Значення комірки повинно бути: [0;100]!", "Помилка вводу");
                    dataGridView1.CurrentCell.Value = 0;
                }

        }

        //Збереження оцінок з DGV для кожного рішення функції
        //Структурна формула розрахунку виконання
        //функцій технічним рішенням
        private void buttonSaveRating_Click(object sender, EventArgs e)
        {
            //Зберігаємо дані для генетичного алгоритму
            //Словник містить в собі id рішення по ключу та по значенню вкладений словник з id функцією та оцінкою рішення за цією функцією
            //якщо id рішення по ключу збігаються то розраховується середнє значення рішення
            Dictionary<int, Dictionary<int, decimal>> dictForGA = new Dictionary<int, Dictionary<int, decimal>>();


            //int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            //Передвигаємося по строкам таблиці
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                GetFirstColumnNameSolutionFunction(i, ref solutionNameInTable, ref functionNameInTable);
               
                //Знайдемо рішення функції, яка зараз оцінюється відносно функцій
                SolutionsOfFunction selectedSolOfFunc = solOfFuncList.FirstOrDefault(s => (s.Solution.name == solutionNameInTable) && (s.Function.name == functionNameInTable));
                if (selectedSolOfFunc is null) return;

                
                decimal estimateSol = 1;
                //якщо враховувати математичну модель с вагою технічних рішень
                if (useWeightSolution)
                    estimateSol = selectedSolOfFunc.Solution.weight ?? 0;

                int index = 0;
                decimal sum = 0;

                Dictionary<int, decimal> solOfFuncEstimateDict = new Dictionary<int, decimal>();

                //Отримаємо оцінки по стовбцям 
                foreach (Function func in funcList)
                {
                    decimal estimateFunc = func.weight ?? 0;
                    decimal estimateSolOnFunc = 0;
                    if (!(decimal.TryParse(dataGridView1[++index, i].Value.ToString(), out estimateSolOnFunc)))
                    {
                        MessageBox.Show("Неможливо конвертувати значення комірки в тип decimal", "Помилка");
                        return;
                    }
                    sum += (estimateFunc * (estimateSolOnFunc / 100.0m));
                    
                    //Накопичуємо для даного рядку ітерації (рішення) його оцінки по функціям 
                    solOfFuncEstimateDict.Add(func.id_function, estimateSolOnFunc / 100.0m);
                }

                //Зберігаємо оцінки рішення по кожній функції (для генетичного алгоритму)
                SaveValueSolByFuncForDictionaryGA(dictForGA, solOfFuncEstimateDict, selectedSolOfFunc);


                decimal finalEstimate = estimateSol * sum;

                //з урахуванням ваги мат. моделі
                finalEstimate *= SetWeightMathModel;

                selectedSolOfFunc.rating = finalEstimate;
                dataGridView1[dataGridView1.Columns.Count - 1, i].Value = finalEstimate;
                //ratings.Add(finalEstimate);
            }
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = true;

            MessageBox.Show("Оцінки збережено","Пітверджено");

            //Передача даних для генетичного алгоритму
            AddDataToGA(//dataGridView1.Rows.Count, 
                solOfFuncList, funcList, dictForGA);
        }

        //Метод для зберігання у словник оцінок рішень по кожній функції 
        private void SaveValueSolByFuncForDictionaryGA(Dictionary<int, Dictionary<int, decimal>> dict, Dictionary<int, decimal> funcDict, SolutionsOfFunction solOfFunc)
        {
            int idSol = solOfFunc.Solution.id_solution;

            decimal newAvgEstimate = 0.0m;

            //якщо немає такого рішення то додаємо його до словника
            if (!dict.ContainsKey(idSol))
            {
                dict.Add(idSol, funcDict);
            }
            //якщо таке рішення вже є в словнику розраховуємо йому середню оцінку по функції в стовбцю
            else
            {
                decimal oldEstimate = 0.0m,
                        newEstimate = 0.0m;

                foreach (var col in funcDict)
                {
                    oldEstimate = dict[idSol][col.Key];
                    newEstimate = col.Value;

                    newAvgEstimate = (oldEstimate + newEstimate) / 2.0m;

                    dict[idSol][col.Key] = newAvgEstimate;
                }            
            }

        }

        private void GetFirstColumnNameSolutionFunction(int indexRow, ref string sol, ref string func)
        {
            int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;

            string valueFirstColumn = Convert.ToString(dataGridView1[firstIndex, indexRow].Value);
            valueFirstColumn = valueFirstColumn.Trim();
            if (valueFirstColumn == null || valueFirstColumn == "") return;
            string[] solFuncFirstColumn = valueFirstColumn.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            for (int indexString = 0; indexString < solFuncFirstColumn.Length; indexString++)
                solFuncFirstColumn[indexString] = solFuncFirstColumn[indexString].Trim();

            if (solFuncFirstColumn[0] == null || solFuncFirstColumn[0] == "") return;

            sol = solFuncFirstColumn[0];
            func = solFuncFirstColumn[1];

        }


        //Передача даних для генетичного алгоритму
        private void AddDataToGA(//int countRows, 
            List<SolutionsOfFunction> solOfFuncList, List<Function> funcList, Dictionary<int, Dictionary<int, decimal>> estimates)
        {
            GeneticAlgorithm.ManagerGA manager = GeneticAlgorithm.ManagerGA.GetInstance();

            manager.SetDataForMorphTable(//countRows, 
                solOfFuncList, funcList, estimates);
        }
    }
}
