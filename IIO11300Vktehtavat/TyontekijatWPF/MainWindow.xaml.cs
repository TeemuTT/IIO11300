/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 11.2.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TyontekijatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XElement xe;

        public MainWindow()
        {
            InitializeComponent();

            comboBox.Items.Add("määräaikainen");
            comboBox.Items.Add("vakituinen");
            comboBox.Items.Add("harjoittelija");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "d:\\H8705\\tyontekijat.xml";
                xe = XElement.Load(filename);
                dataGrid.DataContext = xe.Elements("tyontekija");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int CountWorkers()
        {
            return xe.Elements("tyontekija").Count();
        }

        private int CountWorkers(string tyosuhde)
        {
            var surnames = from ele in xe.Elements()
                       where ele.Element("tyosuhde").Value == tyosuhde
                       select ele.Element("sukunimi");
            return surnames.Count();
        }

        private decimal CalculateSalarySum()
        {
            decimal sum = 0;
            foreach (var node in xe.Elements("tyontekija"))
            {
                sum += Convert.ToDecimal(node.Element("palkka").Value);
            }
            return sum;
        }

        private decimal CalculateSalarySum(string tyosuhde)
        {
            decimal sum = 0;

            var salaries = from ele in xe.Elements()
                       where ele.Element("tyosuhde").Value == tyosuhde
                       select ele.Element("palkka");
            foreach (var salary in salaries)
            {
                sum += Convert.ToDecimal(salary.Value);
            }
            
            return sum;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBlock1.Text = string.Format("Työntekijöitä {0} ja palkat yhteensä {1}", CountWorkers(comboBox.SelectedItem.ToString()), CalculateSalarySum(comboBox.SelectedItem.ToString()));
        }
    }
}
