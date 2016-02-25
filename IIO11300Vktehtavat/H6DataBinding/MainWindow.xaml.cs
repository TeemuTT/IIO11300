/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course.
* Created: 25.2.2016
* Authors: Teemu Tuomela
*/

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace H6DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HockeyLeague smLeague;
        private List<HockeyTeam> leagueTeams;

        public MainWindow()
        {
            InitializeComponent();

            List<string> teams = new List<string>()
            {
                "Ässät", "JYP", "HIFK", "Kärpät", "Tappara",
            };
            cbTeams.ItemsSource = teams;

            smLeague = new HockeyLeague();
            leagueTeams = smLeague.GetTeams();
        }

        private void btnGetSettings_Click(object sender, RoutedEventArgs e)
        {
            btnGetSettings.Content = H6DataBinding.Properties.Settings.Default.UserName;
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            spRight.DataContext = leagueTeams;
        }

        private int pointer = 0;
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (++pointer == leagueTeams.Count) pointer = leagueTeams.Count - 1;
            spRight.DataContext = leagueTeams[pointer];
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (--pointer < 0) pointer = 0;
            spRight.DataContext = leagueTeams[pointer];
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            leagueTeams.Add(new HockeyTeam());
            pointer = leagueTeams.Count - 1;
            spRight.DataContext = leagueTeams[pointer];
        }

        private void textBlock3_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void textBlock4_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
