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
    public partial class TableSolutions : Form
    {

        private string id;

        private MorphModel db;


        //кешування рішень функцій
        private CacheData solOfFuncCacheData = CacheData.GetInstance();

        //Завантажити всі відібрані функції з ОЗУ
        List<Function> funcList;

        public TableSolutions()
        {
            InitializeComponent();

            db = new MorphModel();

            funcList = solOfFuncCacheData.getListFunction;
        }

        private void TableSolutions_Load(object sender, EventArgs e)
        {
            db.Solutions.Load();
            dataGridView1.DataSource = db.Solutions.Local.ToBindingList();

            //Заповнити комбо-бокс тільки іменами функції
            comboBox1.Items.AddRange(funcList.Select(f=>f.name).ToArray());
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

        //Додавання функцій та їх технічних рішень
        private void buttonAddSolutionsOfFunctionsToList_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            Solution sol = dataGridView1.CurrentRow.DataBoundItem as Solution;

            if (sol == null)
            {
                MessageBox.Show("Рішення для функції не обрано!", "Помилка");
                return;
            }

            //Знаходимо функцію, яку вибрав користувач в базі даних. для зв'язки
            string selectedFuncName = comboBox1.SelectedItem.ToString();

            Function selectedFunc = funcList.FirstOrDefault(f => f.name == selectedFuncName);
            
            if(selectedFunc == null)
            {
                MessageBox.Show("Обраної функції не існує в базі даних! \n Оберіть існуючу функцію!", "Помилка вибору функції");
                return;
            }

            //Створюємо об'єкт де функція має своє рішення
            SolutionsOfFunction solOfFunc = new SolutionsOfFunction()
            {
                Solution = sol,
                Function = selectedFunc
            };

            MessageBox.Show("Рішення: " + solOfFunc.Solution.name + " для функції: " + solOfFunc.Function.name + " додано для оцінювання!", "Підтверджено");
            
            //Зберегти в локальне сховище
            solOfFuncCacheData.AddSolutionOfFunctionToList(solOfFunc);

            //Зберегти до бази даних
            //db.SolutionsOfFunctions.AddOrUpdate(solOfFunc);
            
        }
    }
}
