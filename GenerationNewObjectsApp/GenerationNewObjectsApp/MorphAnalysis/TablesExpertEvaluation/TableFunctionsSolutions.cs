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

            //dataGridView1.Columns.Add("Functions", "Функції");
            //for (int i = 0; i < countOfExpert; i++)
            //    dataGridView1.Columns.Add("Expert" + i, "Експ. № " + (i + 1));
        }

        //
        private void FillDgvExperts(DataGridView dgv, string nameFirstColumn)
        {
            dgv.Columns.Add("FirstCol", nameFirstColumn);
            for (int i = 0; i < countOfExpert; i++)
                dgv.Columns.Add("Expert" + i, "Експ. № " + (i + 1));
        }

        private void TableFunctionsSolutions_Load(object sender, EventArgs e)
        {
            FillDgvExperts(dataGridView1, "Функції");
            FillDgvExperts(dataGridView2, "Рішення функції");

            foreach (var list in funcList)
                dataGridView1.Rows.Add(list.name);

            //відображаємо в dgv функції: ім'я, характеристика, вага
            //dataGridView1.DataSource = funcList.Select(f => new { f.name, f.characteristics, f.weight } ).ToList();
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index >= 0)
            {
                //Дізнаємося ім'я строки
                string firstCellValue = e.Row.Cells[0].Value ?. ToString(); ;

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

                dataGridView2.Rows.Clear();

                foreach (var item in getResult)
                    dataGridView2.Rows.Add(item.name);

                
                //TODO: сделать проверку ячеек на ввод только чисел (можно и дабл можно и инт)
                //Как потом вытащить оценки? Как их нормировать? Как сново отобразить новый столбец с нормированными усредёнными оценками?
               
            
                // this.dataGridView2.DataSource = getResult.ToList();
            }
        }

        private void dataGridView2_Enter(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
        }

        private void dataGridView2_MouseLeave(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
        }
    }
}
