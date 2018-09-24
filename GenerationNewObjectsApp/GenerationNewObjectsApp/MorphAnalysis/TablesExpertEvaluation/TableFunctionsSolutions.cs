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

        //Отримаємо локальний список функцій та їх рішень
        private List<SolutionsOfFunction> solOfFuncList;

        //Отримаємо локальний список функцій
        private List<Function> funcList;

        //Отримаємо кількість експертів
        private int countOfExpert = 1;

        //Додаткове розширення (налаштування) DataGridView

        private ConfigDGV configDGV;

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
            configDGV.SetColumnsDgvManyOnlyRead(new [] { dataGridView1, dataGridView2 }, 0, countOfExpert + 1);

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
                Function selectedFunc = funcList.FirstOrDefault(f => f.name == firstCellValue);

                //запит де зіставляємо рішення які зберігаються в БД
                // з рішеннями і функціями в ОЗУ. Дістаємо ім'я рішення та іншу інформацію
                var getPartQuerySolutionsWithFunction = from sol in db.Solutions.ToList()
                                                        join sf in solOfFuncList on sol.id_solution equals sf.Solution.id_solution
                                                        select new { sol.id_solution, sol.name, sol.weight, id_func = sf.Function.id_function };

                var getResult = from sol in getPartQuerySolutionsWithFunction
                                where selectedFunc.id_function == sol.id_func
                                select new { sol.name, sol.weight };

                //очищаємо строки дочірньої таблиці
                dataGridView2.Rows.Clear();

                //відображаємо в dgv всі обрані користувачем рішень для ф-ї
                foreach (var item in getResult)
                    dataGridView2.Rows.Add(item.name);

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

        //Перевірка комірок тільки на ввведення числових даних
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) =>
            configDGV.CellValidating(dataGridView1, sender, e);           

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) =>
            configDGV.CellValidating(dataGridView2, sender, e);

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
        }

        //Розрахунок для функцій 
        private void buttonCalcFuncs_Click(object sender, EventArgs e) =>         
            configDGV.RebuildTableDGV(dataGridView1);

        #endregion

    }
}
