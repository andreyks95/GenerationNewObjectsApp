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

        private void setEstimates()
        {

        }

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

        public double[] getEstimates => values;
    }
}
