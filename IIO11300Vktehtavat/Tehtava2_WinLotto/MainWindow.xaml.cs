/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 27.1.2016
* Authors: Teemu Tuomela
*/

using System.Windows;
using System.Windows.Controls;

namespace Tehtava2_WinLotto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbType.Items.Add("Suomi");
            cbType.Items.Add("Viking Lotto");
            cbType.Items.Add("Eurojackpot");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            Lotto lotto = new Lotto(cbType.SelectedItem.ToString());
            int amount = int.Parse(txtNumber.Text);
            for (int i = 0; i < amount; i++)
            {
                txtDraws.Text += lotto.Draw() + "\n";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtDraws.Text = "";
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            int n;
            if (!int.TryParse((sender as TextBox).Text, out n))
            {
                btnDraw.IsEnabled = false;
            }
            else
            {
                btnDraw.IsEnabled = true;
            }
        }
    }
}
