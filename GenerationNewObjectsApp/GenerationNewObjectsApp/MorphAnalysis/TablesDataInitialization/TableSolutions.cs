using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorphAnalysis.TablesDataInitialization
{
    public partial class TableSolutions : Form
    {

        private string id;

        private MorphModel db;

        public TableSolutions()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void TableSolutions_Load(object sender, EventArgs e)
        {
            db.Solutions.Load();
            dataGridView1.DataSource = db.Solutions.Local.ToBindingList();
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty || textBox3.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            Solution sol = new Solution
            {
                name = textBox1.Text,
                characteristic = textBox2.Text,
                bibliographic_description = textBox3.Text
            };

            db.Solutions.Add(sol);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var sol = db.Solutions.Find(int.Parse(id));

            if (sol == null) return;

            sol.name = textBox1.Text;
            sol.characteristic = textBox2.Text;
            sol.bibliographic_description = textBox3.Text;

            db.Solutions.AddOrUpdate(sol);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var sol = db.Solutions.Find(int.Parse(id));

            db.Solutions.Remove(sol);

            db.SaveChanges();

            dataGridView1.Refresh();
        }        


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var sol = dataGridView1.CurrentRow.DataBoundItem as Solution;

            if (sol == null) return;

            id = Convert.ToString(sol.id_solution);
            textBox1.Text = sol.name;
            textBox2.Text = sol.characteristic;
            textBox3.Text = sol.bibliographic_description;
        }
    }
}
