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
        IEnumerable<ParametersGoal> getResultQueryParamGoal;

        //Отримаємо кількість експертів
        private int countOfExpert = 1;


        //Число (вага) для фільтрації
        private decimal valueFilter = 0;


        public TableParametersOfGoals()
        {
            InitializeComponent();

            db = new MorphModel();

            goalList = cacheData.getListGoal;

            paramGoalList = cacheData.getListParameterGoal;

        }

        public TableParametersOfGoals(int countOfExpert) : this()
        {
            this.countOfExpert = countOfExpert;

            configDGV = new ConfigDGV(countOfExpert);
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

    }
}
