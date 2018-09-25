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
    public partial class MorphTable : Form
    {

        private CacheData cacheData = CacheData.GetInstance();

        private ConfigDGV configDGV;

        private List<Function> funcList;

        private List<SolutionsOfFunction> solOfFuncList;


        public MorphTable()
        {
            InitializeComponent();

            solOfFuncList = cacheData.getListSolutionOfFunctionMorphTable;

            funcList = cacheData.getListFunctionMorphTable;

            configDGV = new ConfigDGV();
        }

        private void MorphTable_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("FirstCol", "Тех. рішення / Функції ");
            configDGV.SetColumnsDgvOnlyRead(dataGridView1, 0);

            int i = 0;
            foreach (Function func in funcList)
            {
                dataGridView1.Columns.Add("F" + ++i, func.name);
            }

            foreach (SolutionsOfFunction solOfFunc in solOfFuncList)
            {
                dataGridView1.Rows.Add(solOfFunc.Solution.name + " => " + solOfFunc.Function.name);
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            int value;
            configDGV.CellValidating(dataGridView1, sender, e);
            if (int.TryParse(Convert.ToString(e.FormattedValue), out value))
                if (!(value >= 0 && value <= 100))
                {
                    MessageBox.Show("Значення комірки повинно бути: [0;100]!", "Помилка вводу");
                    dataGridView1.CurrentCell.Value = 0;
                }

        }
    }
}
