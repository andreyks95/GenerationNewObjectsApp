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

namespace MainTablesForm.TablesFormDataInitialization
{
    public partial class TableFunctions : Form
    {

        private DB_EF6.MorphDBModelContext db;

        private string id;


        public TableFunctions()
        {
            InitializeComponent();

            db = new DB_EF6.MorphDBModelContext();
        }
        private void TableFunctions_Load(object sender, EventArgs e)
        {
            db.Functions.Load();
            dataGridView1.DataSource = db.Functions.Local.ToBindingList();
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстовые поля не заполнены !");
                return;
            }

            DB_EF6.Function function = new DB_EF6.Function {
                name = textBox1.Text,
                characteristics = textBox2.Text
            };

            db.Functions.Add(function);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;

        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var func = db.Functions.Find(int.Parse(id));

            if (func == null) return;

            db.Functions.AddOrUpdate(func);

            db.SaveChanges();
            dataGridView1.Refresh();

        }

        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var func = db.Functions.Find(int.Parse(id));

            if (func == null) return;

            db.Functions.Remove(func);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var func = dataGridView1.CurrentRow.DataBoundItem as DB_EF6.Function;

            if (func == null) return;

            id = Convert.ToString(func.id_function);
            textBox1.Text = func.name;
            textBox2.Text = func.characteristics;
        }
    }
}
