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
    public partial class TableClassifications : Form
    {
        private string id;

        private MorphModel db;

        public TableClassifications()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void TableClassifications_Load(object sender, EventArgs e)
        {
            db.Classifications.Load();
            dataGridView1.DataSource = db.Classifications.Local.ToBindingList();


            //Перейменувати заголовки стовбців
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Назва";
            dataGridView1.Columns[2].HeaderText = "Опис";

            //Cховати стовбці
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var classification = dataGridView1.CurrentRow.DataBoundItem as Classification;

            if (classification == null) return;

            id = Convert.ToString(classification.id_classification);
            textBox1.Text = classification.name;
            textBox2.Text = classification.description;
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            Classification classification = new Classification
            {
                name = textBox1.Text,
                description = textBox2.Text,
            };

            db.Classifications.Add(classification);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var classification = db.Classifications.Find(int.Parse(id));

            if (classification == null) return;

            classification.name = textBox1.Text;
            classification.description = textBox2.Text;

            db.Classifications.AddOrUpdate(classification);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var classification = db.Classifications.Find(int.Parse(id));

            db.Classifications.Remove(classification);

            db.SaveChanges();

            dataGridView1.Refresh();
        }
    }
}
