using MorphAnalysis.HelperClasses;
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
    public partial class TableFunctions : Form
    {

        private string id;

        private MorphModel db;

        //кешування функцій
        private CacheData funcCacheData = CacheData.GetInstance();

        public TableFunctions()
        {
            InitializeComponent();

            db = new MorphModel();
        }

        private void TableFunctions_Load(object sender, EventArgs e)
        {
            db.Functions.Load();
            dataGridView1.DataSource = db.Functions.Local.ToBindingList();

            //Перейменувати заголовки стовбців
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Назва";
            dataGridView1.Columns[2].HeaderText = "Текстовий опис";//"Характеристика";
            dataGridView1.Columns[3].HeaderText = "Вага";

            //Сховати стовбці
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;


        }

        //Add
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Текстові поля незаповнені!");
                return;
            }

            Function func = new Function
            {
                name = textBox1.Text,
                characteristics = textBox2.Text 
            };

            db.Functions.Add(func);
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

            func.name = textBox1.Text;
            func.characteristics = textBox2.Text;

            db.Functions.AddOrUpdate(func);

            db.SaveChanges();
            dataGridView1.Refresh();

        }

        //Delete
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (id == String.Empty) return;

            var func = db.Functions.Find(int.Parse(id));

            db.Functions.Remove(func);

            db.SaveChanges();

            dataGridView1.Refresh();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var func = dataGridView1.CurrentRow.DataBoundItem as Function;

            if (func == null) return;

            id = Convert.ToString(func.id_function);
            textBox1.Text = func.name;
            textBox2.Text = Convert.ToString(func.characteristics);
        }

        private void buttonAddFuncToList_Click(object sender, EventArgs e)
        {
            var func = dataGridView1.CurrentRow.DataBoundItem as Function;
            if (func == null)
            {
                MessageBox.Show("Функція не обрана!", "Помилка");
                return;
            }
            else
            {  
                if(funcCacheData.AddElement<Function>(func))
                    MessageBox.Show("Функцію " + func.name + " додано для оцінювання!", "Підтверджено");
                else
                    MessageBox.Show("Функцію " + func.name + " вже занесено для оцінювання!", "Відхилено");
            }
        }
    }
}
