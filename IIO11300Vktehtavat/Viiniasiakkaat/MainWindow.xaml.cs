/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course.
* Created: 16.3.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Viiniasiakkaat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cstr = Viiniasiakkaat.Properties.Settings.Default.Tietokanta;
                using (SqlConnection conn = new SqlConnection(cstr))
                {
                    string query = "SELECT firstname, lastname, address, city FROM vCustomers";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable("customers");
                    da.Fill(dt);
                    listBox.DataContext = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spRight.DataContext = listBox.SelectedItem;
        }
    }
}
