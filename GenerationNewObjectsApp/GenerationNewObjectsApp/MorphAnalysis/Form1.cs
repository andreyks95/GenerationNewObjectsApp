﻿using MorphAnalysis.TablesDataInitialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorphAnalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new TableFunctions().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new TableSolutions().Show();
        }

        private void таблицяТехРішеньToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableSolutions().Show();
        }

        private void таблицяФункційToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableFunctions().Show();
        }

        private void таблицяОбєктівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableObjects().Show();
        }

        private void таблицяЦілейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableGoals().Show();
        }

        private void таблицяПараметриЦілейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableParametersGoals().Show();
        }

        private void таблицяКласифікаційToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableClassifications().Show();
        }

        private void таблицяМодифікаційToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TableModifications().Show();
        }
    }
}
