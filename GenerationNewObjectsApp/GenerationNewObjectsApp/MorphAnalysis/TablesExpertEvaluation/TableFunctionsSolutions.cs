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

        //Відображаємо дочірні записи згідно обраного рядку батьківської
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index >= 0)
            {
                //Дізнаємося ім'я 0-ї комірки
                string firstCellValue = e.Row.Cells[0].Value?.ToString(); ;

                if (firstCellValue is null) return;


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

            List<Expert> experts = new List<Expert>();
            //пересуваємося по стовпцям
            //Зберігаємо кожний стовбець значень експертів. Для нормування
            for (int col = 0; col < countOfExpert; col++)
            {
                Expert expert = new Expert();
                //пересуваємося по рядках
                for (int row = 0; row < dataGridView2.Rows.Count; row++)
                {
                    double value = Convert.ToDouble(dataGridView2[col+1, row].Value); //col+1 тому що в 1-й комірці назва ф-ї
                    expert.AddValue(value);
                }
                experts.Add(expert);
            }

            
            Normalizer normalizer = new Normalizer();
            //передати експертів
            //отримати нормовані оцінки
            experts = normalizer.CalcNormalizeExpertsEstimates(experts);

            //Розрахувати середнє значення рядків та знову нормалізувати їх
            double[] rowArrayCache = new double[experts.Count];
            double[] rowsAvgCache = new double[experts[0].getEstimates.Length];
            //перебудуємо dgv з нормаваними оцінками
            for (int row = 0; row < experts[0].getEstimates.Length; row++)
            {
                //пересуваємося по рядках
                for (int col = 0; col < experts.Count; col++)
                {
                   rowArrayCache[col] = experts[col].getEstimates[row]; //col+1 тому що в 1-й комірці назва ф-ї
                }
                rowsAvgCache[row] = normalizer.CalcAvg(rowArrayCache);
            }
            rowsAvgCache = normalizer.CalcNormalizeEstimates(rowsAvgCache);


            //перебудуємо dgv з нормаваними оцінками
            for (int col = 0; col < countOfExpert + 1; col++)
            {
                //пересуваємося по рядках
                for (int row = 0; row < dataGridView2.Rows.Count; row++)
                {
                    if (col == countOfExpert)
                        dataGridView2[col+1, row].Value = rowsAvgCache[row];
                    else
                        dataGridView2[col + 1, row].Value = experts[col].getEstimates[row]; //col+1 тому що в 1-й комірці назва ф-ї
                }
            }
            dataGridView2.Columns[countOfExpert + 1].Visible = true;

        }


        //Розрахунок для функцій 
        private void buttonCalcFuncs_Click(object sender, EventArgs e)
        {

            List<Expert> experts = new List<Expert>();
            //пересуваємося по стовпцям
            //Зберігаємо кожний стовбець значень експертів. Для нормування
            for (int col = 0; col < countOfExpert; col++)
            {
                Expert expert = new Expert();
                //пересуваємося по рядках
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    double value = Convert.ToDouble(dataGridView1[col + 1, row].Value); //col+1 тому що в 1-й комірці назва ф-ї
                    expert.AddValue(value);
                }
                experts.Add(expert);
            }


            Normalizer normalizer = new Normalizer();
            //передати експертів
            //отримати нормовані оцінки
            experts = normalizer.CalcNormalizeExpertsEstimates(experts);

            //TODO: HERE

            //Розрахувати середнє значення рядків та знову нормалізувати їх
            double[] rowArrayCache = new double[experts.Count];
            double[] rowsAvgCache = new double[experts[0].getEstimates.Length];
            //перебудуємо dgv з нормаваними оцінками
            for (int row = 0; row < experts[0].getEstimates.Length; row++)
            {
                //пересуваємося по рядках
                for (int col = 0; col < experts.Count; col++)
                {
                    rowArrayCache[col] = experts[col].getEstimates[row]; //col+1 тому що в 1-й комірці назва ф-ї
                }
                rowsAvgCache[row] = normalizer.CalcAvg(rowArrayCache);
            }
            rowsAvgCache = normalizer.CalcNormalizeEstimates(rowsAvgCache);


            //перебудуємо dgv з нормаваними оцінками
            for (int col = 0; col < countOfExpert + 1; col++)
            {
                //пересуваємося по рядках
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    if (col == countOfExpert)
                        dataGridView1[col + 1, row].Value = rowsAvgCache[row];
                    else
                        dataGridView1[col + 1, row].Value = experts[col].getEstimates[row]; //col+1 тому що в 1-й комірці назва ф-ї
                }
            }
            dataGridView1.Columns[countOfExpert + 1].Visible = true;

        }

        #endregion


    }
}
