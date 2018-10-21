
using GeneticSharp.Domain.Fitnesses;
using MorphAnalysis.GeneticAlgorithm;
using MorphAnalysis.HelperClasses;
using MorphAnalysis.HelperForms;
using MorphAnalysis.TablesDataInitialization;
using MorphAnalysis.TablesExpertEvaluation;
using MorphAnalysis.XMLDoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ChartDirector;

namespace MorphAnalysis
{
    public partial class Form1 : Form
    {
        private CacheData cacheData = CacheData.GetInstance();

        //для зберігання номерів  поколінь, їх кращих хромосом та оцінок
        private Dictionary<int, double> resultGAValueDict      ;// = new Dictionary<int, double>();
        private Dictionary<int, string> resultGAChromosomeDict ;// = new Dictionary<int, string>();

        //зберігання налаштувань генетичного алгоритму для зберігання в xml-документ
        //де ключ - анг. назва, значення - укр.
        private Dictionary<string, string> settingsGA;

        public Form1()
        {
            InitializeComponent();
            resultGAValueDict = new Dictionary<int, double>();
            resultGAChromosomeDict = new Dictionary<int, string>();

            settingsGA = new Dictionary<string, string>();
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

        #region 3-я вкладка (Генетичний алгоритм)

        private void StartGAButton_Click(object sender, EventArgs e)
        {
            //Керує отриманням даних для розрахунку фітнес функції 
            ManagerGA managerGA = ManagerGA.GetInstance();

            //Конвертує з / в хромосому
            ConverterToFromChromosome converter = new ConverterToFromChromosome();

            int sizeChromosome = converter.GetSizeChromosome(managerGA.GetCountSolutions, managerGA.GetCountFunctions);

            //отримаємо діапазон розміру популяції
            int minSizePopulation = Convert.ToInt32(minSizePopulationTextBox.Text.ToString());
            if(minSizePopulation < 2)
            {
                MessageBox.Show("Мінімальний розмір популяції повинен бути 2 та більше осіб","Помилка введення");
                return;
            }
            int maxSizePopulation = Convert.ToInt32(maxSizePopulationTextBox.Text.ToString());

            //Будує генетичний алгоритм
            BuildGeneticAlgorithm builderGA = new BuildGeneticAlgorithm(sizeChromosome, minSizePopulation, maxSizePopulation);


            //Знаходимо обрані користувачем RadioButtons
            List<RadioButton> checkedRadioButtons = GetCheckedRadioButtons();
            foreach (RadioButton rb in checkedRadioButtons)
            {
                //Задаємо операції для генетичного алгоритму
                SetOperationGA(rb, builderGA);
            }

            //Додаємо останні налаштування ГА до словника
            settingsGA.Add("min", minSizePopulation.ToString());
            settingsGA.Add("max", maxSizePopulation.ToString());
            settingsGA.Add("countEpochs", textBox1.Text);


            builderGA.BuildFitnessFunction(managerGA, converter);

            builderGA.BuildGA();

            //для показу значень в таблиці


            builderGA.GenerationRan(resultGAValueDict, resultGAChromosomeDict); //ga);
                                      

            builderGA.Start();//ga);

            if (!builderGA.IsRunning)
            {
                MessageBox.Show("Робота генетичного алгоритму завершена!");
                showResultGAButton.Enabled = true;
            }


        }

        //Показати результати роботу алгоритму
        private void showResultGAButton_Click(object sender, EventArgs e)
        {
            HelperForms.ShowResultGA showResultGA = new HelperForms.ShowResultGA(resultGAValueDict, resultGAChromosomeDict, settingsGA);
            showResultGA.Show();
            resultGAValueDict.Clear();
            resultGAChromosomeDict.Clear();
            showResultGAButton.Enabled = false;
        }

        private List<RadioButton> GetCheckedRadioButtons()
        {


            TabPage selectedPage = tabControl1.SelectedTab;

            Control.ControlCollection controls = selectedPage.Controls;

            var panels = controls.OfType<Panel>();

            List<GroupBox> groupBoxes = new List<GroupBox>();
            foreach (Panel p in panels)
            {
                var groupBoxesInPanel = p.Controls.OfType<GroupBox>();

                groupBoxes.AddRange(groupBoxesInPanel);
            }

            List<RadioButton> rbList = new List<RadioButton>();

            foreach (GroupBox gp in groupBoxes)
            {
                var radioButtonsInGroupBox = gp.Controls.OfType<RadioButton>();
                foreach (RadioButton rb in radioButtonsInGroupBox)
                {
                    if (rb.Checked)
                    {
                        rbList.Add(rb);
                    }
                }

            }

            return rbList;
        }

        private void SetOperationGA(RadioButton rb, BuildGeneticAlgorithm builderGA)
        {
            string tag = rb.Tag.ToString();

            switch (tag)
            {
                //block selection
                case "EliteSelection":
                case "TournamentSelection":
                case "RouletteWheelSelection":
                case "StochasticUniversalSamplingSelection":
                    SetSelection(tag, builderGA, rb.Text);
                    break;

                //block crossover
                case "UniformCrossover":
                case "OnePointCrossover":
                case "TwoPointCrossover":
                case "ThreeParentCrossover":
                    SetCrossover(tag, builderGA, rb.Text);
                    break;

                //block mutation
                case "FlipBitMutation":
                case "UniformMutation":
                case "TworsMutation":
                case "ReverseSequenceMutation":
                    SetMutation(tag, builderGA, rb.Text);
                    break;

                //block termination
                case "GenerationNumberTermination":
                case "FitnessStagnationTermination":
                case "FitnessThresholdTermination":
                case "TimeEvolvingTermination":
                    SetTermination(tag, builderGA, rb.Text);
                    break;

                default: break;

            }

        }


        //block selection
        private void SetSelection(string tag, BuildGeneticAlgorithm builderGA, string textRadioButton)
        {
            Selection selection = 0;


            string keyForSettingsDictionary;
            string valueForSettingsDictionary;

            switch (tag)
            {
                case "EliteSelection":
                    selection = Selection.EliteSelection;                    
                    break;

                case "TournamentSelection":
                    selection = Selection.TournamentSelection;
                    break;

                case "RouletteWheelSelection":
                    selection = Selection.RouletteWheelSelection;
                    break;

                case "StochasticUniversalSamplingSelection":
                    selection = Selection.StochasticUniversalSamplingSelection;
                    break;

                default:
                    throw new Exception("Не існує такого методу селекції");
                    break;
            }
            builderGA.SetSelection(selection);

            //додаємо налаштування до словнику
            settingsGA.Add(tag, textRadioButton);
        }

        //block crossover
        private void SetCrossover(string tag, BuildGeneticAlgorithm builderGA, string textRadioButton)
        {
            Crossover crossover = 0;
            switch (tag)
            {
                case "UniformCrossover":
                    crossover = Crossover.UniformCrossover;
                    break;

                case "OnePointCrossover":
                    crossover = Crossover.OnePointCrossover;
                    break;

                case "TwoPointCrossover":
                    crossover = Crossover.TwoPointCrossover;
                    break;

                case "ThreeParentCrossover":
                    crossover = Crossover.ThreeParentCrossover;
                    break;

                default:
                    throw new Exception("Не існує такого виду кросоверу");
                    break;
            }
            builderGA.SetCrossover(crossover);

            //додаємо налаштування до словнику
            settingsGA.Add(tag, textRadioButton);
        }

        //block mutation
        private void SetMutation(string tag, BuildGeneticAlgorithm builderGA, string textRadioButton)
        {
            Mutation mutation = 0;
            switch (tag)
            {

                case "FlipBitMutation":
                    mutation = Mutation.FlipBitMutation;
                    break;
                case "UniformMutation":
                    mutation = Mutation.UniformMutation;
                    break;
                case "TworsMutation":
                    mutation = Mutation.TworsMutation;
                    break;
                case "ReverseSequenceMutation":
                    mutation = Mutation.ReverseSequenceMutation;
                    break;

                default:
                    throw new Exception("Не існує такого виду мутації");
                    break;
            }
            builderGA.SetMutation(mutation);

            //додаємо налаштування до словнику
            settingsGA.Add(tag, textRadioButton);
        }

        //block termination
        private void SetTermination(string tag, BuildGeneticAlgorithm builderGA, string textRadioButton)
        {
            Termination termination = 0;
            switch (tag)
            {
                case "GenerationNumberTermination":
                    termination = Termination.GenerationNumberTermination;
                    break;
                case "FitnessStagnationTermination":
                    termination = Termination.FitnessStagnationTermination;
                    break;
                case "FitnessThresholdTermination":
                    termination = Termination.FitnessThresholdTermination;
                    break;
                case "TimeEvolvingTermination":
                    termination = Termination.TimeEvolvingTermination;
                    break;

                default:
                    throw new Exception("Не існує такого виду припинення алгоритму");
                    break;
            }
            builderGA.SetTermination(termination, Convert.ToDouble(textBox1.Text));

            //додаємо налаштування до словнику
            settingsGA.Add(tag, textRadioButton);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            double value;

            if (e.KeyCode == Keys.Enter)
            {
                if (!double.TryParse(textBox1.Text, out value))
                {
                    MessageBox.Show("Кількість епох повинна бути числом", "Помилка введення");
                }
            }
        }



        #endregion


        #region 4-а вкладка (Аналіз ГА)

        private void buildChartButton_Click(object sender, EventArgs e)
        {
            //отримаємо обраний користувачем RadioButton 
            List<RadioButton> rbList = GetCheckedRadioButtons();

            //так як завжди буде обраний 1 Radio Button 
            //То витягуємо його зі списку

            RadioButton selectedRB = rbList[0];

            if (selectedRB == null) return;

            string[] titleAxisXYZ = new string[3];
            int chooseItem = 1; 

            switch (selectedRB.Tag)
            {
                case "1":
                    titleAxisXYZ[0] = "х - кількість функцій";
                    titleAxisXYZ[1] = "y - кількість кращих мін. епох(ітерацій)";
                    titleAxisXYZ[2] = "z - найкраща оцінка";
                    chooseItem = 1;
                    break;

                case "2":
                    
                    titleAxisXYZ[0] = "x - кількість функцій";
                    titleAxisXYZ[1] = "y - макс.кількість популяції";
                    titleAxisXYZ[2] = "z - найкраща оцінка";
                    chooseItem = 2;
                    break;

                case "3":
                    titleAxisXYZ[0] = "x - кількість кращих мін. епох(ітерацій)";
                    titleAxisXYZ[1] = "y - макс.кількість популяції";
                    titleAxisXYZ[2] = "z - найкраща оцінка";
                    chooseItem = 3;
                    break;

                default:
                    break;
            }


            HelperForms.ChartForm chartForm = new HelperForms.ChartForm(titleAxisXYZ, chooseItem);
            chartForm.Show();
        }

        #endregion

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutProgram().Show();
        }
    }
}
