using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MorphAnalysis.TablesDataInitialization
{
    public partial class TableParametersGoals : Form
    {

        private string id;

        private MorphModel db;

        public TableParametersGoals()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void ParametersGoals_Load(object sender, EventArgs e)
        {
            db.ParametersGoals.Load();
            dataGridView1.DataSource = db.ParametersGoals.Local.ToBindingList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var paramGoal = dataGridView1.CurrentRow.DataBoundItem as ParametersGoal;

            if (paramGoal == null) return;

            id = Convert.ToString(paramGoal.id_parameter);
            textBox1.Text = paramGoal.name;
            textBox2.Text = paramGoal.unit;
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            ParametersGoal paramGoal = new ParametersGoal
            {
                name = textBox1.Text,
                unit = textBox2.Text,
            };

            db.ParametersGoals.Add(paramGoal);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var paramGoal = db.ParametersGoals.Find(int.Parse(id));

            if (paramGoal == null) return;

            paramGoal.name = textBox1.Text;
            paramGoal.unit = textBox2.Text;

            db.ParametersGoals.AddOrUpdate(paramGoal);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var paramGoal = db.ParametersGoals.Find(int.Parse(id));

            db.ParametersGoals.Remove(paramGoal);

            db.SaveChanges();

            dataGridView1.Refresh();
        }
    }
}
