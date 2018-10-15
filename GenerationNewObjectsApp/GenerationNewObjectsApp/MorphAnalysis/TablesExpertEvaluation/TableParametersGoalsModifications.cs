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
    public partial class TableParametersGoalsModifications : Form
    {
        private CacheData cacheData = CacheData.GetInstance();

        private ConfigDGV configDGV;

        //Параметри цілей
        List<ParametersGoal> parametersGoalsList;

        //Модифікації
        private List<Modification> modificationList;

        private string modificationNameInTable;

        //Для тимчасового зберігання назв рішень
        //private string solutionNameInTable;

        //Для тимчасового зберігання назв функції
        //private string functionNameInTable;

        public TableParametersGoalsModifications()
        {
            InitializeComponent();

            modificationList = cacheData.GetListElements<Modification>();

            parametersGoalsList = cacheData.GetListElements<ParametersGoal>(true);

            configDGV = new ConfigDGV();
        }


        private void TableParametersGoalsModifications_Load(object sender, EventArgs e)
        {
            //Наповнюємо стовбцями таблицю
            dataGridView1.Columns.Add("FirstCol", "Модифікація / Параметри цілей ");

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
            foreach (Modification mod in modificationList)
            {
                dataGridView1.Rows.Add(mod.name);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int value;
            configDGV.CellValidating(dataGridView1, sender, e);
            if (int.TryParse(Convert.ToString(e.FormattedValue), out value))
                if (!(value >= 0 && value <= 300))
                {
                    MessageBox.Show("Значення комірки повинно бути: [0;300]!", "Помилка вводу");
                    dataGridView1.CurrentCell.Value = 0;
                }

        }

        //Збереження оцінок з DGV для кожної модифікації згідно параметру цілі
        //Параметрична формула розрахунку оцінки виконання групи модифікацій j-го рішення 
        //згідно з парметрам цілей.
        private void buttonSaveRating_Click(object sender, EventArgs e)
        {
            decimal sumAllMods = 0.0m;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                //GetFirstColumnNameSolutionFunction(i, ref solutionNameInTable, ref functionNameInTable);
                GetFirstColumnNameModification(i, ref modificationNameInTable);

                //Знайдемо рішення і функцію, яке зараз оцінюється відносно параметру цілей
                Modification selectedModification = modificationList.FirstOrDefault(m => m.name == modificationNameInTable);
                if (selectedModification is null) return;


                //decimal estimateSol = selectedSolOfFunc.Solution.weight ?? 0;

                int index = 0;
                decimal sum = 0;



                //Отримаємо оцінки по стовбцям 
                foreach (ParametersGoal paramGoal in parametersGoalsList)
                {

                    decimal estimateParamGoal = paramGoal.Goal.weight ?? 0;
                    decimal estimateModOnParam = 0;
                    if (!(decimal.TryParse(dataGridView1[++index, i].Value.ToString(), out estimateModOnParam)))
                    {
                        MessageBox.Show("Неможливо конвертувати значення комірки в тип decimal", "Помилка");
                        return;
                    }

                    //Питання відкрите щодо використання середнього значення в формулі
                    sum += (estimateParamGoal * (estimateModOnParam / 100.0m));

                }

                //Створюємо новий об'єкт, який закріплює за кожною цілью рішення і кінцеву оцінку рішення
                foreach (ParametersGoal paramGoal in parametersGoalsList)
                {
                    ParametersGoalsForModification parameterGoalForModification = new ParametersGoalsForModification();
                    //Присваюємо модифікацію
                    parameterGoalForModification.Modification = selectedModification;
                    //Присвоємо параметричну ціль об'єкту
                    parameterGoalForModification.ParametersGoal = paramGoal;
                    //Присваюємо загальну оцінку щодо 
                    parameterGoalForModification.rating = sum;
                    //Додамо до списку
                    cacheData.AddElement<ParametersGoalsForModification>(parameterGoalForModification);
                }

                dataGridView1[dataGridView1.Columns.Count - 1, i].Value = sum;
                
                //накопичуємо оцінку всіх модифікацій
                sumAllMods += sum;
            }

            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Visible = true;

            MessageBox.Show("Оцінки збережено", "Пітверджено");

            //Збереження даних для генетичного алгоритму
            //А саме: збереження сукупної оцінки модифікацій для рішень (буде задієно у фітнес функції)
            GeneticAlgorithm.ManagerGA manager = GeneticAlgorithm.ManagerGA.GetInstance();
            manager.SetDataForTableParametersGoalsModifications(sumAllMods);

        }



        private void GetFirstColumnNameModification(int indexRow, ref string mod)
        {
            int firstIndex = dataGridView1.Columns.GetFirstColumn(DataGridViewElementStates.Visible, DataGridViewElementStates.None).Index;

            string valueFirstColumn = Convert.ToString(dataGridView1[firstIndex, indexRow].Value);
            valueFirstColumn = valueFirstColumn.Trim();
            if (valueFirstColumn == null || valueFirstColumn == "") return;
            //string[] solFuncFirstColumn = valueFirstColumn.Split(new char[] { '' }, StringSplitOptions.RemoveEmptyEntries);
            //for (int indexString = 0; indexString < solFuncFirstColumn.Length; indexString++)
            //    solFuncFirstColumn[indexString] = solFuncFirstColumn[indexString].Trim();
            //
            //if (solFuncFirstColumn[0] == null || solFuncFirstColumn[0] == "") return;

            //sol = solFuncFirstColumn[0];
            //func = solFuncFirstColumn[1];

            mod = valueFirstColumn;

        }
    }
}
