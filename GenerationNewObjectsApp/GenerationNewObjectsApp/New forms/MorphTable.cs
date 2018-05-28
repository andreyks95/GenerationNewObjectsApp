using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerationNewObjectsApp
{
    public partial class MorphTable : Form
    {
        public MorphTable()
        {
            InitializeComponent();
        }

       //Добавить частную функцию в таблицу 
        private void button1_Click(object sender, EventArgs e)
        {
            string valueText = comboBox1.Text;
            if (!CheckExistString(valueText))
            {
                dataGridView1.Rows.Add(valueText, "Void data");
                comboBox1.Items.Add(valueText);
            }
        }

        private bool CheckExistString(string text)
        {
            bool result = default(bool);
            foreach (var item in comboBox1.Items)
            {
                if (text == item.ToString())
                { 
                    result = true; break;
                }
            }
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GenerationValueTable gen = new GenerationValueTable(this);
            gen.Show();
        }
    }
}
