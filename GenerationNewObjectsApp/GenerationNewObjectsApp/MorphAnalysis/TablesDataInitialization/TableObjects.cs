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
    public partial class TableObjects : Form
    {
        private string id;

        private MorphModel db;

        public TableObjects()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void TableObjects_Load(object sender, EventArgs e)
        {
            db.MorphObjects.Load();
            dataGridView1.DataSource = db.MorphObjects.Local.ToBindingList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var obj = dataGridView1.CurrentRow.DataBoundItem as MorphObject;

            if (obj == null) return;

            id = Convert.ToString(obj.id_object);
            textBox1.Text = obj.name;
            textBox2.Text = obj.characteristic;
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            MorphObject obj = new MorphObject
            {
                name = textBox1.Text,
                characteristic = textBox2.Text,
            };

            db.MorphObjects.Add(obj);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var obj = db.MorphObjects.Find(int.Parse(id));

            if (obj == null) return;

            obj.name = textBox1.Text;
            obj.characteristic = textBox2.Text;

            db.MorphObjects.AddOrUpdate(obj);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        //Remove
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var obj = db.MorphObjects.Find(int.Parse(id));

            db.MorphObjects.Remove(obj);

            db.SaveChanges();

            dataGridView1.Refresh();
        }
    }
    }
