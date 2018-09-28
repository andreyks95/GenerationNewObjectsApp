using MorphAnalysis.HelperClasses;
using MorphAnalysis.TablesDataInitialization;
using MorphAnalysis.TablesExpertEvaluation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorphAnalysis
{
    public partial class Form1 : Form
    {
        private CacheData cacheData = CacheData.GetInstance();

        public Form1()
        {
            InitializeComponent();
        }

        #region Таблиці ініціалізації даних

        private void button1_Click(object sender, EventArgs e)
        {
            new TableFunctions().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new TableSolutions().Show();
        }

        private void таблицяТехРішеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableSolutions().Show();
        }

        private void таблицяФункційToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableFunctions().Show();
        }

        private void таблицяОбєктівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableObjects().Show();
        }

        private void таблицяЦілейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableGoals().Show();
        }

        private void таблицяПараметриЦілейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableParametersGoals().Show();
        }

        private void таблицяКласифікаційToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableClassifications().Show();
        }

        private void таблицяМодифікаційToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableModifications().Show();
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            DisabledEnabledButtons();
        }

        //Відображення таблиці для оцінювання експертами функцій та їх рішень
        private void buttonSolsFuncs_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBoxCountExpert.Text);
            new TableFunctionsSolutions(value).Show();
            buttonSolsFuncs.Enabled = false;
            buttonMorphTable.Enabled = true;
        }

        //Відображення таблиці для оцінювання експертами цілей та їх параметрів
        private void buttonParamsGoals_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBoxCountExpert.Text);
            new TableParametersOfGoals(value).Show();
            buttonParamsGoals.Enabled = false;
            buttonSolParamTable.Enabled = true;
        }

        private void textBoxCountExpert_KeyDown(object sender, KeyEventArgs e)
        {
            int value;

            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxCountExpert.Text is null || textBoxCountExpert.Text == "")
                {
                    MessageBox.Show("Кількість експертів повинно бути числом і знаходитися в діапазоні: (0;10]", "Помилка введення");
                    DisabledEnabledButtons();
                }
                else
                {
                    if (!int.TryParse(textBoxCountExpert.Text, out value))
                    {
                        MessageBox.Show("Кількість експертів повинно бути числом і знаходитися в діапазоні: (0;10]", "Помилка введення");
                        DisabledEnabledButtons();
                    }
                    else
                    {
                        if (value > 0 && value <= 10)
                        {
                            DisabledEnabledButtons(true);
                        }
                        else
                        {
                            MessageBox.Show("Кількість експертів повинно бути числом і знаходитися в діапазоні: (0;10]", "Помилка введення");
                            DisabledEnabledButtons();
                        }
                    }
                }
            }

        }

        private void DisabledEnabledButtons(bool enabled = false)
        {
            buttonSolsFuncs.Enabled = enabled;
            buttonParamsGoals.Enabled = enabled;
        }


        //Відображення морфологічної таблиці
        private void buttonMorphTable_Click(object sender, EventArgs e)
        {
            if (cacheData.GetList<SolutionsOfFunction>(true).Count > 0
                || cacheData.GetList<Function>(true).Count > 0)
                new MorphTable().Show();
        }

        //Відображення таблиці оцінювання тех. рішень відповідно до параметрів цілей
        private void buttonSolParamTable_Click(object sender, EventArgs e)
        {
            if (cacheData.GetList<ParametersGoal>(true).Count > 0
                || cacheData.GetList<SolutionsOfFunction>(true).Count > 0)
                new TableParametersGoalsSolutions().Show();
        }
    }
}
