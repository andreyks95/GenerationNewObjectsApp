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
    public partial class TableParametersOfGoals : Form
    {
        private MorphModel db;

        private CacheData cacheData = CacheData.GetInstance();

        //Додаткове розширення (налаштування) DataGridView
        private ConfigDGV configDGV;

        //Отримаємо локальний список цілей
        private List<Goal> goalList;

        //Отримаємо локальний список параметрів цілей
        private List<ParametersGoal> paramGoalList;

        //Для обраної цілі з dgv1
        private Goal selectedGoal;

        //Для отримання результатів LINQ параметрів цілі обраної користувачем цілі
        private IEnumerable<ParametersGoal> getResultQueryParamGoal;

        //Локальне сховище параметрів цілей для передачі в таблиці оцінювання рішень по цілям та модифікацій по цілям
        private List<ParametersGoal> paramGoalLocalList;

        //Отримаємо кількість експертів
        private int countOfExpert = 1;


        //Число (вага) для фільтрації
        private decimal valueFilter = 0;


        public TableParametersOfGoals()
        {
            InitializeComponent();

            db = new MorphModel();

            goalList = cacheData.GetList<Goal>();

            paramGoalList = cacheData.GetList<ParametersGoal>();

        }

        public TableParametersOfGoals(int countOfExpert) : this()
        {
            this.countOfExpert = countOfExpert;

            configDGV = new ConfigDGV(countOfExpert);

            paramGoalLocalList = new List<ParametersGoal>();
        }

        private void TableParametersOfGoals_Load(object sender, EventArgs e)
        {
            //побудова стобців
            configDGV.FillDgvManyExperts(new[] { dataGridView1, dataGridView2 }, new[] { "Цілі", "Параметри цілей" });

            //Налаштування стовбців тільки для читання
            configDGV.SetColumnsDgvManyOnlyRead(new[] { dataGridView1, dataGridView2 }, 0, countOfExpert + 1);

            //Скриємо від користувача стовбець "Вага" до розрахунку
            dataGridView1.Columns[countOfExpert + 1].Visible = false;
            dataGridView2.Columns[countOfExpert + 1].HeaderText = "Ср. значення";
            dataGridView2.Columns[countOfExpert + 1].Visible = false;

            //відображаємо в dgv всі обрані користувачем цілі
            foreach (var item in goalList)
                dataGridView1.Rows.Add(item.name);
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index >= 0)
            {
                //Дізнаємося ім'я 0-ї комірки. Назва цілі
                string firstCellValue = e.Row.Cells[0].Value?.ToString(); ;
                if (firstCellValue is null) return;

                label1.Text = "Розрахувати ср. значення параметрів \n в межах обраної цілі:" + "\n" + firstCellValue;

                //отримаємо функцію для запиту
                selectedGoal = goalList.FirstOrDefault(g => g.name == firstCellValue);


                //запит де зіставляємо параметри параметри цілей які зберігаються в БД
                // з параматреми цілей в ОЗУ. Дістаємо параметри цілі
                IEnumerable<ParametersGoal> getPartQueryParametersWithGoal = from paramDB in db.ParametersGoals.ToList()
                                                                             join paramLocal in paramGoalList on paramDB.id_parameter equals paramLocal.id_parameter
                                                                             select paramLocal;

                getResultQueryParamGoal = from paramLocal in getPartQueryParametersWithGoal
                                          where paramLocal.Goal.id_goal == selectedGoal.id_goal
                                          select paramLocal;

                //очищаємо строки дочірньої таблиці
                dataGridView2.Rows.Clear();

                //відображаємо в dgv всі обрані користувачем рішень для ф-ї
                foreach (var item in getResultQueryParamGoal)
                    dataGridView2.Rows.Add(item.name);

            }
        }


        #region Додаткове налаштування dgv's

        //Вимикаємо фокус на dgv1 коли редагуємо dgv2
        private void dataGridView2_Enter(object sender, EventArgs e) =>
        dataGridView1.Enabled = false;

        //Вмикаємо фокус на dgv1 коли покидаємо dgv2
        private void dataGridView2_MouseLeave(object sender, EventArgs e) =>
        dataGridView1.Enabled = true;

        //Відкючаємо стовбець ср. значень dgv2 коли знаходимося в dgv1
        private void dataGridView1_Enter(object sender, EventArgs e) =>
        dataGridView2.Columns[countOfExpert + 1].Visible = false;

        //Перевірка комірок тільки на ввведення числових даних
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) =>
        configDGV.CellValidating(dataGridView1, sender, e);

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) =>
        configDGV.CellValidating(dataGridView2, sender, e);

        /*private void textBox1_KeyDown(object sender, KeyEventArgs e)
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
        }*/

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            decimal value;

            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text is null || textBox2.Text == "")
                    MessageBox.Show("Введене значення повинне бути числом!", "Помилка введення");
                else
                {
                    if (!decimal.TryParse(textBox2.Text, out value))
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

        #region Розрахунок ваги елементів відносно один одного та середніх значень параметрів рішень

        //Розрахунок середнього значення параметру цілей
        private void buttonCalcParams_Click(object sender, EventArgs e)
        {
            configDGV.RebuildTableDGV(dataGridView2, false);
            buttonSaveResultParamsOfGoal.Enabled = true;
        }

        //Розрахунок ваги цілей
        private void buttonCalcGoals_Click(object sender, EventArgs e)
        {
            configDGV.RebuildTableDGV(dataGridView1);
            buttonSaveResultGoals.Enabled = true;
        }

        #endregion

        private void buttonSaveResultGoals_Click(object sender, EventArgs e)
        {
            int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            int lastIndex = dataGridView1.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;


            //Присвоюємо для кожної цілі її вагу, якщо вона пройшла фільтрацію
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string nameGoal = Convert.ToString(dataGridView1[firstIndex, i].Value);
                decimal weightGoal = Convert.ToDecimal(dataGridView1[lastIndex, i].Value);

                if (nameGoal != null && weightGoal >= 0 && weightGoal <= 1 && weightGoal >= valueFilter)
                {
                    ParametersGoal paramGoal = paramGoalLocalList.FirstOrDefault(p => p.Goal.name == nameGoal);
                    if (paramGoal is null) return;
                    paramGoal.Goal.weight = weightGoal;
                }
            }
            //Перебираємо локальний список параметрів цілі та відхиляємо ті цілі в яких нема ваги
            //або якщо вона менша заданному фільтру (при загрузці з БД. Можливий пропуск з більшим значенням!)
            foreach (ParametersGoal item in paramGoalLocalList)
            {
                if (item.Goal is null)
                    continue;
                if (item.Goal.weight is null)
                    continue;
                if (item.Goal.weight <= valueFilter)
                    continue;

                Goal goal = new Goal()
                {
                    id_goal = item.Goal.id_goal,
                    name = item.Goal.name,
                    characteristic = item.Goal.characteristic,
                    weight = item.Goal.weight
                };

                ParametersGoal newParam = new ParametersGoal()
                {
                    id_parameter = item.id_parameter,
                    unit = item.unit,
                    name = item.name,
                    avg = item.avg,
                    Goal = goal
                };

                //cacheData.AddParameterGoalToList(newParam, true);
                cacheData.AddElementToList<ParametersGoal>(newParam, true);
            }
            MessageBox.Show("Дані успішно додані", "Підтверджено");
            /*foreach (var item in cacheData.getListParameterGoalForTables)
            {
                Console.Write("ID param =" + item.id_parameter + " " + item.name + " avg = " + item.avg
                    + " \n ID goal" + item.Goal.id_goal + " " + item.Goal.name + " weight = " + item.Goal.weight + "\n");
            }*/

        }

        private void buttonSaveResultParamsOfGoal_Click(object sender, EventArgs e)
        {

            //обрана користувачем ціль
            string nameSelectedGoalDGV1 = dataGridView1.CurrentRow.Cells[0].Value?.ToString();

            if (nameSelectedGoalDGV1 == null) return;

            //Знаходимо індекси в dgv2 для назв параметру цілі і її середньої оцінки
            int firstIndexDGV2 = dataGridView2.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            int lastIndexDGV2 = dataGridView2.Columns.GetLastColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;

            for (int rowIndex = 0; rowIndex < dataGridView2.Rows.Count; rowIndex++)
            {
                //Назва параметру
                string nameParameterGoal = Convert.ToString(dataGridView2[firstIndexDGV2, rowIndex].Value);
                //Середнє значення параметру цілі. Можливе зі знаком "-"
                decimal avgParameterGoal = Convert.ToDecimal(dataGridView2[lastIndexDGV2, rowIndex].Value);

                if (nameParameterGoal != null)
                {
                    ParametersGoal selectedParamOfGoal = getResultQueryParamGoal.FirstOrDefault(p => p.name == nameParameterGoal
                                                                                 && p.Goal.name == nameSelectedGoalDGV1);
                    if (selectedParamOfGoal is null) return;
                    selectedParamOfGoal.avg = avgParameterGoal;
                    //selectedParamOfGoal.Goal.

                    paramGoalLocalList.Add(selectedParamOfGoal);

                    //cacheData.AddSolutionOfFunctionToList(selectedSolOfFunc, true);
                }

            }
            MessageBox.Show("Дані успішно додані", "Підтверджено");
        }
    }
}
