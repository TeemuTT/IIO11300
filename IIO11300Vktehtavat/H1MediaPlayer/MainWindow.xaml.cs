/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 28.1.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Windows;
using Microsoft.Win32;

namespace H1MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IsPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            txtPath.Text = "D:\\H8705\\CoffeeMaker.mp4";
            LoadSource();
        }

        private void LoadSource()
        {
            try
            {
                mediaElement.Source = new Uri(txtPath.Text);
                IsPlaying = false;
                SetButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetButtons()
        {
            btnPlay.IsEnabled = !IsPlaying;
            btnPause.IsEnabled = IsPlaying;
            btnStop.IsEnabled = IsPlaying;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
            IsPlaying = true;
            SetButtons();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            IsPlaying = false;
            SetButtons();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            IsPlaying = false;
            SetButtons();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();

            OpenFileDialog dialog = new OpenFileDialog();
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                txtPath.Text = dialog.FileName;
                LoadSource();
            }
        }

        private void txtPath_LostFocus(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            LoadSource();
        }
    }
}
