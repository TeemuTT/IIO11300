/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course.
* Created: 19.2.2016
* Modified: 24.2.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace Viinikellari
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlDataProvider dataProvider;

        public MainWindow()
        {
            InitializeComponent();

            // Asetetaan XmlDataProviderin lähde App.Config:in mukaisesti.
            dataProvider = (Resources["Viinit"] as XmlDataProvider);
            string path = ConfigurationManager.AppSettings["fileLocation"];

            // Jos tiedostoa ei ole olemassa, luodaan uusi.
            if (!File.Exists(path))
            {
                dataProvider.Document = CreateNewDocument();
                tbStatus.Text = "Tiedostoa " + path + " ei löytynyt. Luotiin uusi tiedosto.";
            }
            else 
            {
                dataProvider.Source = new Uri(path);
                tbStatus.Text = "Viinit ladattu tiedostosta " + path;
            }
        }

        // Luo ja palauttaa XmlDocumentin joka sisältää viinikellari-elementin.
        private XmlDocument CreateNewDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode header = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.AppendChild(header);
            XmlNode root = doc.CreateNode(XmlNodeType.Element, "viinikellari", null);
            doc.AppendChild(root);
            return doc;
        }

        // Ladataan viinit käyttäjän valitsemasta tiedostosta. Tiedostosijainti
        // tallennetaan App.Config tiedostoon, jotta sama tiedosto aukeaa
        // seuraavalla käynnistyskerralla automaattisesti.
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "XML (.xml)|*.xml";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                dataProvider.Source = new Uri(dialog.FileName);
                UpdateAppSetting("fileLocation", dialog.FileName);
                tbStatus.Text = "Viinit ladattu tiedostosta " + dialog.FileName;
            }
        }

        // Lisätään uusi viini listaan. Annettava vähintään nimi ja valmistusmaa.
        private void btnNewWine_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length == 0 |
                tbCountry.Text.Length == 0)
            {
                tbStatus.Text = "Anna vähintään Nimi sekä Maa!";
                return;
            }
            XmlNode newWine = dataProvider.Document.CreateElement("wine");
            XmlNode childNode = dataProvider.Document.CreateNode(XmlNodeType.Element, "nimi", null);
            childNode.InnerText = tbName.Text;
            newWine.AppendChild(childNode);

            childNode = dataProvider.Document.CreateNode(XmlNodeType.Element, "maa", null);
            childNode.InnerText = tbCountry.Text;
            newWine.AppendChild(childNode);

            childNode = dataProvider.Document.CreateNode(XmlNodeType.Element, "arvio", null);
            childNode.InnerText = tbPoints.Text;
            newWine.AppendChild(childNode);
            dataProvider.Document.SelectSingleNode("viinikellari").AppendChild(newWine);

            tbStatus.Text = "Viini lisätty";
            tbName.Text = "";
            tbCountry.Text = "";
            tbPoints.Text = "";
            UpdateComboBox();
        }

        // Tallennetaan listaan tehdyt muutokset levylle.
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Jos alussa luotiin uusi tiedosto, pyydetään tallennussijainti.
            if (dataProvider.Source == null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "XML (.xml)|*.xml";
                Nullable<bool> result = dialog.ShowDialog();
                if (result == true)
                {
                    dataProvider.Document.Save(dialog.FileName);
                    tbStatus.Text = "Muutokset tallennettu levylle";
                    UpdateAppSetting("fileLocation", dialog.FileName);
                }
            }
            else
            {
                dataProvider.Document.Save(dataProvider.Source.LocalPath);
                tbStatus.Text = "Muutokset tallennettu levylle";
            }
        }

        // Tallennetaan viinitiedoston sijainti App.Configiin.
        // Ei toimi debug-ajossa.
        private static void UpdateAppSetting(string key, string value)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(0);
            cfg.AppSettings.Settings[key].Value = value;
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Poistetaan käyttäjän valitsema viini.
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex < 0)
            {
                tbStatus.Text = "Valitse ensin poistettava viini!";
            }
            MessageBoxResult result = MessageBox.Show("Vahvista poistaminen",
                                                      "Viinikellari",
                                                      MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                dataProvider.Document.SelectSingleNode("viinikellari")
                    .RemoveChild((dataGrid.SelectedItem as XmlNode));
                tbStatus.Text = "Viini poistettu";
                UpdateComboBox();
            }
        }

        // Maan valinnan jälkeen suodatetaan maan mukaan.
        // Vinkkiä löydetty: https://social.msdn.microsoft.com/Forums/vstudio/en-US/41654b26-eaf7-4bfb-81e8-e1bc4c94a8e5/filter-a-datagrid-from-textbox?forum=wpf
        private void cbCountrySelector_DropDownClosed(object sender, EventArgs e)
        {
            CollectionViewSource collection = Resources["ViiniCollection"] as CollectionViewSource;
            if (cbCountrySelector.Text.Equals("Kaikki"))
            {
                collection.View.Filter = null;
            }
            else
            {
                collection.View.Filter = item =>
                {
                    XmlElement wine = item as XmlElement;
                    if (wine["maa"].InnerText.Equals(cbCountrySelector.Text)) return true;
                    else return false;
                };
            }

            tbStatus.Text = "Näytetään viinit maasta '" + cbCountrySelector.Text + "'";
        }

        // Jos DataGridissä muokataan maata, täytyy päivittää maavalitsin.
        // Tässä vaiheessa maa ei ole vielä päivittynyt joten kutsutaan
        // DataGrid.CommitEdit(). CommitEdit täällä aiheuttaa loputtoman
        // silmukan joten pidetään boolean arvolla homma kurissa.
        // Vinkkiä löydetty: http://stackoverflow.com/a/6247930
        private bool recursion = false;
        private void dataGrid_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            if (!e.Column.Header.Equals("Valmistusmaa"))
            {
                return;
            }

            if (!recursion)
            {
                recursion = true;
                dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                recursion = false;
                UpdateComboBox();
            }
        }

        // Uusi tiedosto -> poista duplikaatit maista.
        private void XmlDataProvider_DataChanged(object sender, EventArgs e)
        {
            UpdateComboBox();
        }

        // Poistetaan maanvalitsimesta duplikaatit.
        private void UpdateComboBox()
        {
            cbCountrySelector.ItemsSource = dataProvider.Document.SelectNodes("/viinikellari/wine/maa");
            List<string> countries = new List<string>() { "Kaikki" };
            foreach (XmlElement item in cbCountrySelector.Items)
            {
                countries.Add(item.InnerText);
            }
            cbCountrySelector.ItemsSource = countries.Distinct();
            cbCountrySelector.SelectedIndex = 0;
        }
    }
}
