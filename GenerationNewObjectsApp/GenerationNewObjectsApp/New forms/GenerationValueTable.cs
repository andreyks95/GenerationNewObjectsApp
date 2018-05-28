using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenerationNewObjectsApp;

namespace GenerationNewObjectsApp
{
    public partial class GenerationValueTable : Form
    {
        MorphTable morphTable;// = new MorphTable();

        //Словарь для хранения функций и их компонентов
        Dictionary<string, List<string>> componentsOfParticularFunction = new Dictionary<string, List<string>> ();


        public GenerationValueTable()
        {
            InitializeComponent();
        }

        public GenerationValueTable(MorphTable morph)
        {
            InitializeComponent();
            morphTable = morph;
            GetDataFromMorphTable(0);
        }

        //Вытаскиваем значенияя с таблицы для морфологического анализа
        private void GetDataFromMorphTable(int number = 0)
        {
            #region OptimalVariant
            //3 - й вариант
            List<string> particularFunctions = new List<string>();
            List<List<string>> listComponents = new List<List<string>>();
            object value;
            foreach (DataGridViewRow dgvr in morphTable.dataGridView1.Rows)
            {
                value = dgvr.Cells[0].Value;
                if (value == null) continue;
                //Если ещё не существует такой частной функции, то добавляем её в список
                if (!particularFunctions.Contains((string)value))
                {
                    particularFunctions.Add((string)value);
                    string lastValue = particularFunctions.Last(); //По последнему элементу отыскать 


                    //Находим все компоненты которые соответствуют частной функции
                    List<string> listComponentsPartFunc = new List<string>();
                    foreach (DataGridViewRow r in morphTable.dataGridView1.Rows)
                    {
                        value = r.Cells[0].Value;
                        if ((string)value == lastValue)
                        {
                            string component = (string)r.Cells[1].Value;
                            listComponentsPartFunc.Add(component);
                        }
                    }

                    //Добавляем все найденные компоненты в список общих компонентов
                    listComponents.Add(listComponentsPartFunc);
                }
               
            }
            #endregion

            int i = 0;
            foreach(string item in particularFunctions)
            {
                componentsOfParticularFunction.Add(item, listComponents[i++]);
            }

            //НУЖНО ПОПЫТАТЬСЯ ПРОГНАТЬ ДАНЫЙ АЛГОРИТМ
            string test = "";
            foreach (KeyValuePair<string, List<string>> component in componentsOfParticularFunction.OrderBy(l=>l.Key))
            {
                test += component.Key + " - ";
                for(int j=0; j < component.Value.Count; j++)
                {
                    test += " " + component.Value[j] + ", ";
                }
                test += "\n";
            }
            MessageBox.Show(test);

        }
    }
}
