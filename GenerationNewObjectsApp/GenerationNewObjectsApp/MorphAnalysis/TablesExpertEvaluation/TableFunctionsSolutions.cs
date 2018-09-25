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

namespace MorphAnalysis.TablesExpertEvaluation
{
    public partial class TableFunctionsSolutions : Form
    {
        private MorphModel db;

        private CacheData cacheData = CacheData.GetInstance();

        //Додаткове розширення (налаштування) DataGridView
        private ConfigDGV configDGV;

        //Отримаємо локальний список функцій та їх рішень
        private List<SolutionsOfFunction> solOfFuncList;

        //Отримаємо локальний список функцій
        private List<Function> funcList;

        //Для обраної функції з dgv1
        private Function selectedFunc;

        //Для отримання результатів LINQ рішення обраної користувачем функції
        IEnumerable<SolutionsOfFunction> getResultQuerySolOfFunc;

        //Отримаємо кількість експертів
        private int countOfExpert = 1;

        //Число (вага) для фільтрації
        private decimal valueFilter = 0;

        public TableFunctionsSolutions()
        {
            InitializeComponent();
            
            db = new MorphModel();

            solOfFuncList = cacheData.getListSolutionOfFunction;

            funcList = cacheData.getListFunction;

        }

        public TableFunctionsSolutions(int countOfExpert) : this()
        {
            this.countOfExpert = countOfExpert;

            configDGV = new ConfigDGV(countOfExpert);
        }


        private void TableFunctionsSolutions_Load(object sender, EventArgs e)
        {
            //побудова стобців
            configDGV.FillDgvManyExperts(new[] { dataGridView1, dataGridView2 }, new[] { "Функції", "Рішення функції" });

            //Налаштування стовбців тільки для читання
            configDGV.SetColumnsDgvManyOnlyRead(new[] { dataGridView1, dataGridView2 }, 0, countOfExpert + 1);

            //Скриємо від користувача стовбець "Вага" до розрахунку
            dataGridView1.Columns[countOfExpert + 1].Visible = false;
            dataGridView2.Columns[countOfExpert + 1].Visible = false;

            //відображаємо в dgv всі обрані користувачем ф-ї
            foreach (var list in funcList)
                dataGridView1.Rows.Add(list.name);

            //відображаємо в dgv функції: ім'я, характеристика, вага
            //dataGridView1.DataSource = funcList.Select(f => new { f.name, f.characteristics, f.weight } ).ToList();
        }

        //Відображаємо дочірні записи згідно обраного рядку батьківської таблиці (рішення для функцій)
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index >= 0)
            {
                //Дізнаємося ім'я 0-ї комірки. Назва функції
                string firstCellValue = e.Row.Cells[0].Value?.ToString(); ;

                if (firstCellValue is null) return;


                label1.Text = "Розрахувати вагу рішень в межах обраної функції:" + "\n" + firstCellValue;


                //string firstCellValue = e.Row.Cells[0].Value.ToString();

                //отримаємо функцію для запиту
                selectedFunc = funcList.FirstOrDefault(f => f.name == firstCellValue);

                //СТАРИЙ ЗАПИТ!!!
                //запит де зіставляємо рішення які зберігаються в БД
                // з рішеннями і функціями в ОЗУ. Дістаємо ім'я рішення та іншу інформацію
                //var getPartQuerySolutionsWithFunction = from sol in db.Solutions.ToList()
                //                                        join sf in solOfFuncList on sol.id_solution equals sf.Solution.id_solution
                //                                        select new { sol.id_solution, sol.name, sol.weight, id_func = sf.Function.id_function };

                //var getResult = from sol in getPartQuerySolutionsWithFunction
                //                where selectedFunc.id_function == sol.id_func
                //                select new { sol.name, sol.weight };


                //запит де зіставляємо рішення які зберігаються в БД
                // з рішеннями і функціями в ОЗУ. Дістаємо ім'я рішення та іншу інформацію
                IEnumerable<SolutionsOfFunction> getPartQuerySolutionsWithFunction = from sol in db.Solutions.ToList()
                                                                                     join sf in solOfFuncList on sol.id_solution equals sf.Solution.id_solution
                                                                                     select sf;

                getResultQuerySolOfFunc = from sf in getPartQuerySolutionsWithFunction
                                          where selectedFunc.id_function == sf.Function.id_function
                                          select sf;

                //очищаємо строки дочірньої таблиці
                dataGridView2.Rows.Clear();

                //відображаємо в dgv всі обрані користувачем рішень для ф-ї
                foreach (var item in getResultQuerySolOfFunc)
                    dataGridView2.Rows.Add(item.Solution.name);

                // this.dataGridView2.DataSource = getResult.ToList();
            }
        }

        #region Додаткове налаштування dgv's

        //Вимикаємо фокус на dgv1 коли редагуємо dgv2
        private void dataGridView2_Enter(object sender, EventArgs e) =>
            dataGridView1.Enabled = false;

        //Вмикаємо фокус на dgv1 коли покидаємо dgv2
        private void dataGridView2_MouseLeave(object sender, EventArgs e) =>
            dataGridView1.Enabled = true;

        //Відкючаємо стовбець ваги dgv2 коли знаходимося в dgv1
        private void dataGridView1_Enter(object sender, EventArgs e) =>
            dataGridView2.Columns[countOfExpert + 1].Visible = false;

        //Перевірка комірок тільки на ввведення числових даних
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) =>
            configDGV.CellValidating(dataGridView1, sender, e);

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) =>
            configDGV.CellValidating(dataGridView2, sender, e);


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            decimal value;

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text is null || textBox1.Text == "")
                    MessageBox.Show("Введене значення повинне бути числом!", "Помилка введення");
                else
                {
                    if (!decimal.TryParse(textBox1.Text, out value))
                        MessageBox.Show("Введене значення повинне бути числом!", "Помилка введення");
                    else
                    {
                        if (value >= 0 && value <= 1)
                            valueFilter = value;
                        else
                            MessageBox.Show("Вага повинна бути >=0 && <=1", "Помилка введення");
                    }
                }
            }
        }

        #endregion


        #region Розрахунок ваги елементів відносно один одного 

        //Розрахунок для рішень обраної функції
        private void buttonCalcSols_Click(object sender, EventArgs e)
        {
            //отримаємо оцінки з таблиці
            //List<Expert> experts = configDGV.GetExpertColumnEstimates(dataGridView2);
            //класс нормалізації
            //Normalizer normalizer = new Normalizer();
            ////передати експертів
            ////отримати нормовані оцінки
            //experts = normalizer.CalcNormalizeExpertsEstimates(experts);
            //розрахуємо середнє значення стовбців та виконаємо їх нормалізацію
            //double[] rowsAvgCache = configDGV.GetAvgValuesRows(experts);
            //перебудуємо таблицю
            //configDGV.RebuildTableDGV(dataGridView2, experts, rowsAvgCache);

            configDGV.RebuildTableDGV(dataGridView2);
            buttonSaveResultSolutionsOfFunc.Enabled = true;
        }

        //Розрахунок для функцій 
        private void buttonCalcFuncs_Click(object sender, EventArgs e)
        {
            configDGV.RebuildTableDGV(dataGridView1);
            buttonSaveResultFuncs.Enabled = true;
        }
        #endregion

        #region Збереження результатів в ОЗУ для побудови морф. таблиці

        //Збереження результатів оцінювання рішень в межах функції
        private void buttonSaveResultSolutionsOfFunc_Click(object sender, EventArgs e)
        {
            string nameSolution = null;
            decimal weightSolution = 0;

            int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            int lastIndex = dataGridView1.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                nameSolution = Convert.ToString(dataGridView1[firstIndex, i].Value);
                weightSolution = Convert.ToDecimal(dataGridView1[lastIndex, i].Value);

                if (nameSolution != null && weightSolution >= 0 && weightSolution <= 1 && weightSolution >= valueFilter)
                {
                    SolutionsOfFunction selectedSolOfFunc = getResultQuerySolOfFunc.FirstOrDefault(s => s.Solution.name == nameSolution);
                    if (selectedSolOfFunc is null) return;
                    selectedSolOfFunc.Solution.weight = weightSolution;
                    selectedSolOfFunc.rating = weightSolution;

                    cacheData.AddSolutionOfFunctionToList(selectedSolOfFunc, true);
                    /*Console.WriteLine("selectedSolOfFunc" + selectedSolOfFunc.Function.id_function + ": " + selectedSolOfFunc.Function.name +
                            " sol = " + selectedSolOfFunc.Solution.id_solution + ": " + selectedSolOfFunc.Solution.name +
                            " " + selectedSolOfFunc.Solution.weight + " " + selectedSolOfFunc.rating);*/
                }
            }

        }

        //Збереження результатів оцінювання функцій 
        private void buttonSaveResultFuncs_Click(object sender, EventArgs e)
        {
            string nameFunc = null;
            decimal weightFunc = 0;

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    GetNameWeightResult(dataGridView1, i, ref nameFunc, ref weightFunc);
            //    if (nameFunc != null && weightFunc >= 0 && weightFunc <= 1 && weightFunc >= valueFilter)
            //    {
            //        Function func = funcList.FirstOrDefault(f=> f.name == nameFunc);
            //        if (func is null) return;
            //        func.weight = weightFunc;
            //        cacheData.AddFunctionToList(func, true);
            //        //Console.WriteLine("Func " + func.id_function + ": " + func.name + " " + func.weight);
            //    }
            //}

            int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            int lastIndex = dataGridView1.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                nameFunc = Convert.ToString(dataGridView1[firstIndex, i].Value);
                weightFunc = Convert.ToDecimal(dataGridView1[lastIndex, i].Value);

                if (nameFunc != null && weightFunc >= 0 && weightFunc <= 1 && weightFunc >= valueFilter)
                {
                    Function func = funcList.FirstOrDefault(f => f.name == nameFunc);
                    if (func is null) return;
                    func.weight = weightFunc;
                    cacheData.AddFunctionToList(func, true);
                    //Console.WriteLine("Func " + func.id_function + ": " + func.name + " " + func.weight);
                }
            }
        }


        //private void GetNameWeightResult(DataGridView dgv, int indexRow, ref string name, ref decimal weight)
        //{
        //    for (int j = 0; j < dataGridView1.Columns.Count; j++)
        //    {
        //        if (dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index == j)
        //            name = Convert.ToString(dataGridView1[j, indexRow].Value);
        //        if (dataGridView1.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index == j)
        //            weight = Convert.ToDecimal(dataGridView1[j, indexRow].Value);
        //    }
        //}

        #endregion

    }
}
