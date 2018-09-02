using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerationNewObjectsApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new IndicatorsExpertForm());
            //Application.Run(new MorphAnalysisForm());
            //Application.Run(new GenerationEvaluationTables());
            //Application.Run(new GenerationObjects());
            // Application.Run(new RatingObjectsForm());
            //Application.Run(new MorphTable());
            Application.Run();
        }
    }
}
