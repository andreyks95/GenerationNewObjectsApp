using MainTablesForm.TablesFormDataInitialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainTablesForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonFuncTable_Click(object sender, EventArgs e)
        {
            new TableFunctions().Show();
        }
    }
}
