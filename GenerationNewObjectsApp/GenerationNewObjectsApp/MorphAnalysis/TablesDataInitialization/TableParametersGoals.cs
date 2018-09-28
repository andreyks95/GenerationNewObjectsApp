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
    public partial class TableParametersGoals : Form
    {

        private string id;

        private MorphModel db;

        //кешування 
        private CacheData cacheData = CacheData.GetInstance();

        //Завантажити всі відібрані цілі з ОЗУ
        List<Goal> goals;

        public TableParametersGoals()
        {
            InitializeComponent();

            db = new MorphModel();

            goals = cacheData.GetList<Goal>();
        }

        private void ParametersGoals_Load(object sender, EventArgs e)
        {
            db.ParametersGoals.Load();
            dataGridView1.DataSource = db.ParametersGoals.Local.ToBindingList();

            comboBox1.Items.Clear();
            //Заповнити комбо-бокс тільки іменами цілей
            comboBox1.Items.AddRange(goals.Select(g => g.name).ToArray());

            //Перейменувати заголовки стовбців
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Назва";
            dataGridView1.Columns[2].HeaderText = "Одиниця виміру";
            dataGridView1.Columns[3].HeaderText = "Середнє значення (еталон)";

            //Cховати стовбці
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
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

        //Додавання параметрів цілей до списку
        private void buttonAddParameterToList_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            ParametersGoal paramGoal = dataGridView1.CurrentRow.DataBoundItem as ParametersGoal;

            if (paramGoal == null)
            {
                MessageBox.Show("Параметр для цілі не обрано!", "Помилка");
                return;
            }

            //Знаходимо ціль, яку вибрав користувач в базі даних. для зв'язки
            string selectedGoalName = comboBox1.SelectedItem.ToString();

            Goal selectedGoal = goals.FirstOrDefault(g => g.name == selectedGoalName);

            if (selectedGoal == null)
            {
                MessageBox.Show("Обраної цілі не існує в базі даних! \n Оберіть існуючу ціль!", "Помилка вибору цілі");
                return;
            }

            //Закріплюємо за параметром обрану користувачем ціль
            ParametersGoal newParamGoal = new ParametersGoal()
            {
                id_parameter = paramGoal.id_parameter,
                name = paramGoal.name,
                unit = paramGoal.unit,
                Goal = selectedGoal
            };

            //Зберегти в локальне сховище
            if (cacheData.AddElementToList<ParametersGoal>(newParamGoal))
                MessageBox.Show("Параметр: " + newParamGoal.name + " для цілі: " + newParamGoal.Goal.name + " додано для оцінювання!", "Підтверджено");
            else
                MessageBox.Show("Параметр: " + newParamGoal.name + " для цілі: " + newParamGoal.Goal.name + " вже занесено для оцінювання!", "Відхилено");
            //Зберегти до бази даних
            //db.SolutionsOfFunctions.AddOrUpdate(solOfFunc);
        }
    }
}
