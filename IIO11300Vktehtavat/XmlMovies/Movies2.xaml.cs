/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 18.2.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Windows;
using System.Xml;

namespace XmlMovies
{
    /// <summary>
    /// Interaction logic for Movies2.xaml
    /// </summary>
    public partial class Movies2 : Window
    {
        public Movies2()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            xdpMovies.Document.Save(xdpMovies.Source.LocalPath);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlNode root = xdpMovies.Document.SelectSingleNode("/Movies");
                if (MessageBox.Show("Poistetaanko elokuva : " + tbName.Text + "?", "Elokuvagalleria", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    root.RemoveChild(lbMovies.SelectedItem as XmlNode);
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (lbMovies.SelectedIndex >= 0)
            {
                lbMovies.SelectedIndex = -1;
            }
            else
            {
                if (tbName.Text.Length == 0 |
                    tbDirector.Text.Length == 0 |
                    tbCountry.Text.Length == 0)
                {
                    MessageBox.Show("Täytä kaikki kentät!");
                    return;
                }
                try
                {
                    XmlNode root = xdpMovies.Document.SelectSingleNode("/Movies");

                    XmlNode newNode = xdpMovies.Document.CreateElement("Movie");
                    XmlAttribute xa1 = xdpMovies.Document.CreateAttribute("Name");
                    xa1.Value = tbName.Text;
                    newNode.Attributes.Append(xa1);

                    xa1 = xdpMovies.Document.CreateAttribute("Director");
                    xa1.Value = tbDirector.Text;
                    newNode.Attributes.Append(xa1);

                    xa1 = xdpMovies.Document.CreateAttribute("Country");
                    xa1.Value = tbCountry.Text;
                    newNode.Attributes.Append(xa1);

                    xa1 = xdpMovies.Document.CreateAttribute("Checked");
                    xa1.Value = checkWatched.IsChecked.ToString();
                    newNode.Attributes.Append(xa1);

                    root.AppendChild(newNode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
