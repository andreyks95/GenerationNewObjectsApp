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
    public partial class TableModifications : Form
    {
        private string id;

        private MorphModel db;

        public TableModifications()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void TableModifications_Load(object sender, EventArgs e)
        {
            db.Modifications.Load();
            dataGridView1.DataSource = db.Modifications.Local.ToBindingList();

            //Cховати стовбці
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var mod = dataGridView1.CurrentRow.DataBoundItem as Modification;

            if (mod == null) return;

            id = Convert.ToString(mod.id_modification);
            textBox1.Text = mod.name;
            textBox2.Text = mod.characteristic;
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            Modification mod = new Modification
            {
                name = textBox1.Text,
                characteristic = textBox2.Text,
            };

            db.Modifications.Add(mod);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var mod = db.Modifications.Find(int.Parse(id));

            if (mod == null) return;

            mod.name = textBox1.Text;
            mod.characteristic = textBox2.Text;

            db.Modifications.AddOrUpdate(mod);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var mod = db.Modifications.Find(int.Parse(id));

            db.Modifications.Remove(mod);

            db.SaveChanges();

            dataGridView1.Refresh();
        }
    }
}
