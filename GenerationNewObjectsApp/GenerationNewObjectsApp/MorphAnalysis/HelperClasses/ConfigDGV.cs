using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorphAnalysis.HelperClasses
{
    class ConfigDGV
    {
        private readonly int countOfExpert = 1;

        private Normalizer normalizer = new Normalizer();

        public ConfigDGV(int countOfExpert = 1)
        {
            this.countOfExpert = countOfExpert;
        }

        #region Побудова стовбців dgv згідно кількості експертів

        //Побудова dgv згідно кількості експертів

        public void FillDgvManyExperts(DataGridView[] dgvs, string[] nameFirstColumn)
        {
            int i = 0;
            foreach (var item in dgvs)
            {
                FillDgvExperts(item, nameFirstColumn[i++]);
            }
        }

        public void FillDgvExperts(DataGridView dgv, string nameFirstColumn)
        {
            dgv.Columns.Add("FirstCol", nameFirstColumn);
            for (int i = 0; i < countOfExpert; i++)
                dgv.Columns.Add("Expert" + (i + 1), "Експ. № " + (i + 1));
            dgv.Columns.Add("Sum", "Вага");
        }

        #endregion

        #region  Налаштування стовбців dgv тільки для читання

        //Налаштування стовбців dgv тільки для читання
        public void SetColumnsDgvManyOnlyRead(DataGridView[] dgvs, params int[] indexes)
        {
            foreach (var item in dgvs)
            {
                SetColumnsDgvOnlyRead(item, indexes);
            }
        }

        public void SetColumnsDgvOnlyRead(DataGridView dgv, params int[] indexes)
        {
            foreach (int item in indexes)
                dgv.Columns[item].ReadOnly = true;
        }

        #endregion

        #region Перірка комірок на введення числових даних

        //Перевірка комірок тільки на ввведення числових даних
        public void CellValidating(DataGridView dgv, object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex > 0)// && e.ColumnIndex < countOfExpert + 1)
            {
                //Перевіряємо чи є значення в комірці числовим
                if (!IsDigits(e.FormattedValue))
                {
                    MessageBox.Show("Введіть тільки числові дані!");
                    dgv.CurrentCell.Value = 0;
                    e.Cancel = true;

                }

            }
        }

        private bool IsDigits(object obj)
        {
            string value = Convert.ToString(obj);

            int i;
            double d;

            if (double.TryParse(value, out d) || int.TryParse(value, out i))
                return true;
            else
                return false;
        }

        #endregion

        #region отримання значень по стовбцям. Нормалізація даних. Перебудова таблиці з новими значеннями 

        ////Зберігаємо кожний стовбець значень експертів. Для нормування
        public List<Expert> GetExpertsColumnsEstimates(DataGridView dgv)
        {
            List<Expert> experts = new List<Expert>();
            //пересуваємося по стовпцям
            //Зберігаємо кожний стовбець значень експертів. Для нормування
            for (int col = 0; col < countOfExpert; col++)
            {
                Expert expert = new Expert();
                //пересуваємося по рядках
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    double value = Convert.ToDouble(dgv[col + 1, row].Value); //col+1 тому що в 1-й комірці назва елементу
                    expert.AddValue(value);
                }
                experts.Add(expert);
            }

            return experts;
        }

        //Розрахуємо середнє значення для кожного рядка dgv і нормуємо їх по стовбцю якщо true
        public double[] GetAvgValuesRows(List<Expert> experts, bool normalize = true)
        {
            double[] rowsAvgCache;
            if (normalize)
            {
                rowsAvgCache = GetAvgValuesRows(experts);
                rowsAvgCache = normalizer.CalcNormalizeEstimates(rowsAvgCache);
            }
            else
            {
                rowsAvgCache = GetAvgValuesRows(experts);
            }
            return rowsAvgCache;
        }

        private double[] GetAvgValuesRows(List<Expert> experts)
        {
            //Розрахувати середнє значення рядків та знову нормалізувати їх
            double[] rowArrayCache = new double[experts.Count];
            double[] rowsAvgCache = new double[experts[0].getEstimates.Length];
            //проходження по значенням кожного експерта (рядки таблиці)
            for (int row = 0; row < experts[0].getEstimates.Length; row++)
            {
                //пересуваємося по кожному експерту (стовбці таблиці)
                for (int col = 0; col < experts.Count; col++)
                {
                    rowArrayCache[col] = experts[col].getEstimates[row]; //col+1 тому що в 1-й комірці назва ф-ї, рішення
                }
                rowsAvgCache[row] = normalizer.CalcAvg(rowArrayCache);
            }
            return rowsAvgCache;
        }



        //Перебудуємо таблицю з відображенням норманих оцінок і середніх нормованих оцінок (вагу)
        public void RebuildTableDGV(DataGridView dgv, List<Expert> experts, double[] rowsAvgCache)
        {
            //перебудуємо dgv з нормаваними оцінками
            for (int col = 0; col < countOfExpert + 1; col++)
            {
                //пересуваємося по рядках
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    if (col == countOfExpert)
                        dgv[col + 1, row].Value = rowsAvgCache[row];
                    else
                        dgv[col + 1, row].Value = experts[col].getEstimates[row]; //col+1 тому що в 1-й комірці назва ф-ї
                }
            }

            //Відобразимо стовбець "Вага"
            dgv.Columns[countOfExpert + 1].Visible = true;
        }

        //Перебудуємо таблицю з відображенням норманих оцінок і середніх нормованих оцінок (вагу)
        public void RebuildTableDGV(DataGridView dgv, bool normalize = true)
        {
            //отримати оцінки кожного експерта по стовбцям
            List<Expert> experts = this.GetExpertsColumnsEstimates(dgv);

            //передати експертів
            //отримати нормовані оцінки
            if(normalize)
                experts = normalizer.CalcNormalizeExpertsEstimates(experts);

            //отримати середні нормалізовані оцінки
            double[] rowsAvgCache = this.GetAvgValuesRows(experts, normalize);

            //Перебудувати таблицю з новими значеннями
            this.RebuildTableDGV(dgv, experts, rowsAvgCache);
        }

        #endregion
    }
}
