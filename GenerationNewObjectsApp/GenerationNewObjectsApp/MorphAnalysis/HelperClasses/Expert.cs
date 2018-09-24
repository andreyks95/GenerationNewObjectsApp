using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.HelperClasses
{
    public class Expert
    {

        private double[] values = new double[0];

        /*private void setEstimates()
        {

        }*/

         //Додаємо значення оцінки, яку виставив експерт елементу по стовбцю
        public void AddValue(double value)
        {
            double[] newList = new double[values.Length + 1];
            for (int i = 0; i < values.Length; i++)
            {
                newList[i] = values[i];
            }
            newList[newList.Length - 1] = value;
            values = newList;
        }

        public void AddValueRange(double[] array)
        {
            values = array;
        }

        //отримаємо оцінки по стовцю, що оцінював експерт
        public double[] getEstimates => values;

        //Призначаємо нові оцінки 
        public double[] setEstimates
        {
            set { values = value; }
        }
    }
}
