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
            if (e.ColumnIndex > 0 && e.ColumnIndex < countOfExpert + 1)
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

    }
}
