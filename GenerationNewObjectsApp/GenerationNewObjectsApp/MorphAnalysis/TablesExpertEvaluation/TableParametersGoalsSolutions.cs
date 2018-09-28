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
    public partial class TableParametersGoalsSolutions : Form
    {
        private CacheData cacheData = CacheData.GetInstance();

        private ConfigDGV configDGV;

        //Параметри цілей
        List<ParametersGoal> parametersGoalsList;

        //Рішення з морф. таблиці
        private List<SolutionsOfFunction> solOfFuncList;

        //Для тимчасового зберігання назв рішень
        private string solutionNameInTable;

        //Для тимчасового зберігання назв функції
        private string functionNameInTable;


        public TableParametersGoalsSolutions()
        {
            InitializeComponent();

            solOfFuncList = cacheData.GetList<SolutionsOfFunction>(true);

            parametersGoalsList = cacheData.GetList<ParametersGoal>(true);

            configDGV = new ConfigDGV();

        }

        private void TableParametersGoalsSolutions_Load(object sender, EventArgs e)
        {


            //Наповнюємо стовбцями таблицю
            dataGridView1.Columns.Add("FirstCol", "Тех. рішення / Параметри цілей ");

            int i = 0;
            foreach (ParametersGoal param in parametersGoalsList)
            {
                dataGridView1.Columns.Add("P" + ++i, "Параметр: " + param.name + " : " + " Ціль: " + param.Goal.name + " "
                    + "ср. знач. " + param.avg + " " + param.unit);
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
                if (!(value >= 0 && value <= 300))
                {
                    MessageBox.Show("Значення комірки повинно бути: [0;100]!", "Помилка вводу");
                    dataGridView1.CurrentCell.Value = 0;
                }

        }


        //Збереження оцінок з DGV для кожного рішення параметру цілі
        //Параметрична формула розрахунку виконання
        //технічними рішеннями згідно параметрам цілей
        private void buttonSaveRating_Click(object sender, EventArgs e)
        {

            //int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                GetFirstColumnNameSolutionFunction(i, ref solutionNameInTable, ref functionNameInTable);

                //Знайдемо рішення і функцію, яке зараз оцінюється відносно параметру цілей
                SolutionsOfFunction selectedSolOfFunc = solOfFuncList.FirstOrDefault(s => (s.Solution.name == solutionNameInTable) && (s.Function.name == functionNameInTable));
                if (selectedSolOfFunc is null) return;


                //decimal estimateSol = selectedSolOfFunc.Solution.weight ?? 0;

                int index = 0;
                decimal sum = 0;



                //Отримаємо оцінки по стовбцям 
                foreach (ParametersGoal paramGoal in parametersGoalsList)
                {

                    decimal estimateParamGoal = paramGoal.Goal.weight ?? 0;
                    decimal estimateSolOnParam = 0;
                    if (!(decimal.TryParse(dataGridView1[++index, i].Value.ToString(), out estimateSolOnParam)))
                    {
                        MessageBox.Show("Неможливо конвертувати значення комірки в тип decimal", "Помилка");
                        return;
                    }

                    //Питання відкрите щодо використання середнього значення в формулі
                    sum += (estimateParamGoal * (estimateSolOnParam / 100.0m));

                }

                //Створюємо новий об'єкт, який закріплює за кожною цілью рішення і кінцеву оцінку рішення
                foreach (ParametersGoal paramGoal in parametersGoalsList)
                {
                    ParametersGoalsForSolution parameterGoalForSolution = new ParametersGoalsForSolution();
                    //Присваюємо рішення об'єкту
                    parameterGoalForSolution.Solution = selectedSolOfFunc.Solution;
                    //Присвоємо параметричну ціль об'єкту
                    parameterGoalForSolution.ParametersGoal = paramGoal;
                    //Присваюємо загальну оцінку щодо 
                    parameterGoalForSolution.rating = sum;
                    //Додамо до списку
                    cacheData.AddParamGoalForSolToList(parameterGoalForSolution);
                }

                dataGridView1[dataGridView1.Columns.Count - 1, i].Value = sum;
            }

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = true;
            

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

    }
}
