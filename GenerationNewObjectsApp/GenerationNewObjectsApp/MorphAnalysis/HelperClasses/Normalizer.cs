using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorphAnalysis.HelperClasses
{

    class Normalizer
    {

        //Нормалізація оцінок по стовбцям для списка експертів
        public List<Expert> CalcNormalizeExpertsEstimates(List<Expert> listExpets)
        {
            List<Expert> newValues = new List<Expert>();
            foreach (var item in listExpets)
            {
                double[] value = CalcNormalizeExpertEstimates(item);
                Expert exp = new Expert();
                exp.AddValueRange(value);
                newValues.Add(exp);
            }
            return newValues;

        }

        //Нормалізація оцінок по стовбцю для експерта
        public double[] CalcNormalizeExpertEstimates(Expert expert) => CalcNormalizeEstimates(expert.getEstimates);
        


        //Нормалізація оцінок по масиву
        public double[] CalcNormalizeEstimates(double[] values)
        {
            double[] buf = values;
            double[] newValues = new double[buf.Length];
            double sum = 0;

            for (int i = 0; i < buf.Length; i++)
            {
                sum += buf[i];
            }

            for (int i = 0; i < buf.Length; i++)
            {
                newValues[i] = buf[i] / sum;
            }

            return newValues;
        }

        //Вычислить среднее для строк (элементов)

        public double CalcAvg(double[] values) => values.Average();
        

    }
}
