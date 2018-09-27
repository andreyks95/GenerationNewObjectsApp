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
using MorphAnalysis.HelperClasses;

namespace MorphAnalysis.TablesDataInitialization
{
    public partial class TableGoals : Form
    {

        private string id;

        private MorphModel db;

        //кешування
        private CacheData cacheData = CacheData.GetInstance();

        public TableGoals()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void TableGoals_Load(object sender, EventArgs e)
        {
            db.Goals.Load();
            dataGridView1.DataSource = db.Goals.Local.ToBindingList();

            //Перейменувати заголовки стовбців
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Назва";
            dataGridView1.Columns[2].HeaderText = "Характеристика";
            dataGridView1.Columns[3].HeaderText = "Вага";

            //Cховати стовбці
            dataGridView1.Columns[4].Visible = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var goal = dataGridView1.CurrentRow.DataBoundItem as Goal;

            if (goal == null) return;

            id = Convert.ToString(goal.id_goal);
            textBox1.Text = goal.name;
            textBox2.Text = goal.characteristic;
        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            Goal goal = new Goal
            {
                name = textBox1.Text,
                characteristic = textBox2.Text,
            };

            db.Goals.Add(goal);
            db.SaveChanges();
            dataGridView1.Refresh();

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
        }

        //Edit
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var goal = db.Goals.Find(int.Parse(id));

            if (goal == null) return;

            goal.name = textBox1.Text;
            goal.characteristic = textBox2.Text;

            db.Goals.AddOrUpdate(goal);

            db.SaveChanges();
            dataGridView1.Refresh();
        }

        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var goal = db.Goals.Find(int.Parse(id));

            db.Goals.Remove(goal);

            db.SaveChanges();

            dataGridView1.Refresh();
        }

        private void buttonAddGoalToList_Click(object sender, EventArgs e)
        {
            var goal = dataGridView1.CurrentRow.DataBoundItem as Goal;
            if (goal == null)
            {
                MessageBox.Show("Ціль не обрана!", "Помилка");
                return;
            }
            else
            {
                if (cacheData.AddGoalToList(goal))
                    MessageBox.Show("Ціль " + goal.name + " додано для оцінювання!", "Підтверджено");
                else
                    MessageBox.Show("Ціль " + goal.name + " вже занесено для оцінювання!", "Відхилено");
            }
        }
    }
}
