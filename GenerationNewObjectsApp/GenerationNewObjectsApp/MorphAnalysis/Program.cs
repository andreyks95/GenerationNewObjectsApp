﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorphAnalysis
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
               Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
                Application.Restart();

            }
            //finally
            //{
            //    Application.Restart();
            //}
           
        }
    }
}
