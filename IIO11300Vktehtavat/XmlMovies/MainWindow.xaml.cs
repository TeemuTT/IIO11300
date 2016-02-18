/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 18.2.2016
* Authors: Teemu Tuomela
*/

using System.Windows;

namespace XmlMovies
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

        private void btnOpenMovies1_Click(object sender, RoutedEventArgs e)
        {
            Movies1 win = new Movies1();
            win.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOpenMovies2_Click(object sender, RoutedEventArgs e)
        {
            Movies2 win = new Movies2();
            win.ShowDialog();
        }
    }
}
