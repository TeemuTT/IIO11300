/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 11.2.2016
* Modified: 16.2.2016
* Authors: Teemu Tuomela
*/

using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SMLiiga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BLLiiga liiga;

        public MainWindow()
        {
            InitializeComponent();
            liiga = new BLLiiga();
            cbSeura.ItemsSource = liiga.Seurat;
            lbPelaajat.ItemsSource = liiga.Pelaajat;
        }

        private void btnUusi_Click(object sender, RoutedEventArgs e)
        {
            if (tbEnimi.Text.Length == 0 ||
                tbSnimi.Text.Length == 0 ||
                tbSiirtohinta.Text.Length == 0 ||
                cbSeura.Text.Length == 0)
            {
                statustext.Text = "Täytä kaikki kentät!";
                return;
            }

            decimal hintaTarkistus;
            if (!decimal.TryParse(tbSiirtohinta.Text, out hintaTarkistus))
            {
                statustext.Text = "Anna kunnollinen siirtohinta!";
                return;
            }

            bool onnistui = liiga.LisaaPelaaja(
                tbEnimi.Text,
                tbSnimi.Text,
                cbSeura.Text,
                Convert.ToDecimal(tbSiirtohinta.Text));
            if (onnistui)
            {
                statustext.Text = "Pelaaja lisätty onnistuneesti";
                PaivitaListBox();
            }
            else
            {
                statustext.Text = "Samanniminen pelaaja on jo olemassa!";
            }
        }

        private void PaivitaListBox()
        {
            lbPelaajat.ItemsSource = null;
            lbPelaajat.ItemsSource = liiga.Pelaajat;
        }

        private void lbPelaajat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbPelaajat.SelectedIndex < 0) return;

            BLPelaaja pelaaja = liiga.Pelaajat[lbPelaajat.SelectedIndex];
            tbEnimi.Text = pelaaja.Etunimi;
            tbSnimi.Text = pelaaja.Sukunimi;
            tbSiirtohinta.Text = pelaaja.Siirtohinta.ToString();
            cbSeura.Text = pelaaja.Seura;
        }

        private void btnLopeta_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnPoista_Click(object sender, RoutedEventArgs e)
        {
            if (lbPelaajat.SelectedIndex < 0)
            {
                statustext.Text = "Valitse poistettava pelaaja!";
                return;
            }
            liiga.PoistaPelaaja(lbPelaajat.SelectedIndex);
            statustext.Text = "Pelaaja poistettu";
            PaivitaListBox();
        }

        private void btnPaivita_Click(object sender, RoutedEventArgs e)
        {
            if (lbPelaajat.SelectedIndex < 0)
            {
                statustext.Text = "Valitse päivitettävä pelaaja!";
                return;
            }
            else if (tbEnimi.Text.Length == 0 ||
                tbSnimi.Text.Length == 0 ||
                tbSiirtohinta.Text.Length == 0 ||
                cbSeura.Text.Length == 0)
            {
                statustext.Text = "Täytä kaikki kentät!";
                return;
            }

            liiga.PaivitaPelaaja(
                lbPelaajat.SelectedIndex, tbEnimi.Text,
                tbSnimi.Text,
                cbSeura.Text,
                Convert.ToDecimal(tbSiirtohinta.Text));
            statustext.Text = "Pelaajan tiedot päivitetty";
            PaivitaListBox();
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "xml (.xml)|*.xml|txt (.txt)|*.txt";
            dlg.FileName = "pelaajat";
            dlg.DefaultExt = ".xml";

            Nullable<bool> tulos = dlg.ShowDialog();

            if (tulos == true)
            {
                try
                {
                    liiga.TallennaTiedostoon(dlg.FileName, Path.GetExtension(dlg.FileName));
                    statustext.Text = "Pelaajat tallennettu tiedostoon";
                }
                catch (Exception)
                {
                    statustext.Text = "Tallennus epäonnistui!";
                }
            }
        }

        private void btnLataa_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "xml (.xml)|*.xml|txt (.txt)|*.txt";
            Nullable<bool> tulos = dlg.ShowDialog();

            if (tulos == true)
            {
                try
                {
                    liiga.LataaTiedostosta(dlg.FileName, Path.GetExtension(dlg.FileName));
                    statustext.Text = "Tiedot ladattu";
                    PaivitaListBox();
                }
                catch (Exception)
                {
                    statustext.Text = "Tietojen lataus epäonnistui!";
                }
            }
        }
    }
}
