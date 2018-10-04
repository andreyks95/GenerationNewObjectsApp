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

        #region 1-ша вкладка (Експертна оцінка)

        #region Таблиці для фільтрації (відбору) елементів 

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
            buttonModParamTable.Enabled = true;
        }

        #endregion

        #region Перевірка вводу даних текстбоксу

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
            buttonModParamTable.Enabled = enabled;
        }

        #endregion

        #region Оціночні таблиці

        //Відображення морфологічної таблиці
        private void buttonMorphTable_Click(object sender, EventArgs e)
        {
            if (cacheData.GetListElements<SolutionsOfFunction>(true).Count > 0
                || cacheData.GetListElements<Function>(true).Count > 0)
                new MorphTable().Show();
        }

        //Відображення таблиці оцінювання тех. рішень відповідно до параметрів цілей
        private void buttonSolParamTable_Click(object sender, EventArgs e)
        {
            if (cacheData.GetListElements<ParametersGoal>(true).Count > 0
                || cacheData.GetListElements<SolutionsOfFunction>(true).Count > 0)
                new TableParametersGoalsSolutions().Show();
        }

        //Відображення таблиці оцінювання модифікацій відповідно до параметрів цілей
        private void buttonModParamTable_Click(object sender, EventArgs e)
        {
            if (cacheData.GetListElements<ParametersGoal>(true).Count > 0
                || cacheData.GetListElements<Modification>().Count > 0)
                new TableParametersGoalsModifications().Show();
        }


        #endregion

        #endregion


        #region 2-га вкладка (Оцінки рішень) 

        private void buttonSols_Click(object sender, EventArgs e)
        {
            List<SolutionsOfFunction> listSolsOfFuncs = cacheData.GetListElements<SolutionsOfFunction>(true);
            List<ParametersGoalsForModification> listParamsGoalsForMod = cacheData.GetListElements<ParametersGoalsForModification>();
            List<ParametersGoalsForSolution> listParamsGoalsForSol = cacheData.GetListElements<ParametersGoalsForSolution>();

            if (listSolsOfFuncs.Count > 0 && listParamsGoalsForMod.Count > 0 && listParamsGoalsForSol.Count > 0)
            {
                BuildDGV(listSolsOfFuncs, listParamsGoalsForMod, listParamsGoalsForSol);  
            }
            else
            {
                MessageBox.Show("Елементи списків рішень по: функціям, параметрам цілей, а також список модифікацій по параметрам цілей " +
                    " для розрахунку оцінок рішень повинні бути  > 0", "Помилка");
                return;
            }


        }

        private void BuildDGV(List<SolutionsOfFunction> listSolsOfFuncs,
                                List<ParametersGoalsForModification> listParamsGoalsForMod,
                                List<ParametersGoalsForSolution> listParamsGoalsForSol)
        {
            var dgv = dataGridView1;
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            FinalSolutionEstimate finalEstimates = FinalSolutionEstimate.GetInstance();

            finalEstimates.SetEstimates<SolutionsOfFunction>(listSolsOfFuncs);
            finalEstimates.SetEstimates<ParametersGoalsForModification>(listParamsGoalsForMod);
            finalEstimates.SetEstimates<ParametersGoalsForSolution>(listParamsGoalsForSol);

            //Оцінка рішення згідно параметрам цілей
            var dictParamsGoalsForSolution = finalEstimates.GetEstimatesBy<ParametersGoalsForSolution>();
            //Оцінка рішення згідно функціям
            var dictSolsOfFuncs = finalEstimates.GetEstimatesBy<SolutionsOfFunction>();
            //Оцінка рішення згідно параметрам цілей з модифікаціями
            var dictParamsGoalsForSolutionWithModification = finalEstimates.GetEstimatesBy<ParametersGoalsForSolution>();
            //Сукупна оцінка рішення
            var dictFinalSol = finalEstimates.GetFinalSolutionsEstimates;

            List<Solution> sols = GetSolutionsForTable(listSolsOfFuncs, listParamsGoalsForSol);



            dgv.Columns.Add("Sol", "Рішення");
            dgv.Columns.Add("vSolParam", "Оцінка рішення згідно параметрам цілей");
            dgv.Columns.Add("vSolParamWithMod", "Оцінка рішення згідно параметрам цілей з модифікаціями");
            dgv.Columns.Add("vSolFunc", "Оцінка рішення згідно функціям");
            dgv.Columns.Add("vSolFinal", "Сукупна оцінка рішення");

            foreach (Solution sol in sols)
            {
                string[] row = new string[5];
                int id = sol.id_solution;

                row[0] = sol.name;

                if (dictParamsGoalsForSolution.ContainsKey(id))
                    row[1] = dictParamsGoalsForSolution[id].ToString();
                else
                    row[1] = "";

                if (dictParamsGoalsForSolutionWithModification.ContainsKey(id))
                    row[2] = dictParamsGoalsForSolutionWithModification[id].ToString();
                else
                    row[2] = "";

                if (dictSolsOfFuncs.ContainsKey(id))
                    row[3] = dictSolsOfFuncs[id].ToString();
                else
                    row[3] = "";

                if (dictFinalSol.ContainsKey(id))
                    row[4] = dictFinalSol[id].ToString();
                else
                    row[4] = "";

                dgv.Rows.Add(row);
            }

            finalEstimates.Dispose();
           
        }

        //Для пошуку усіх оцінених рішень експертами
        private List<Solution> GetSolutionsForTable(List<SolutionsOfFunction> listSolsOfFuncs, List<ParametersGoalsForSolution> listParamsGoalsForSol)
        {
            List<Solution> firstList = listSolsOfFuncs.Select(s => s.Solution).ToList();
            List<Solution> secondList = listParamsGoalsForSol.Select(s => s.Solution).ToList();

            List<Solution> result = firstList.Union(secondList).GroupBy(sol => sol.id_solution).Select(grp => grp.First()).ToList();
            return result;
        }

        #endregion

    }
}
