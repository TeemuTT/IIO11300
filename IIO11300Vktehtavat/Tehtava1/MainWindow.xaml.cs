/*
* Copyright (C) JAMK/IT/Teemu Tuomela
* This file is part of the IIO11300 course project.
* Created: 14.1.2016
* Modified: 14.1.2016
* Authors: Teemu Tuomela
*/

using System;
using System.Windows;

namespace Tehtava1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double width = Double.Parse(txtWidth.Text);
                double height = Double.Parse(txtHeight.Text);
                double frameWidth = Double.Parse(txtFrameWidth.Text);
                double windowArea = BusinessLogicWindow.CalculateWindowArea(width, height) * (1E-6);
                double frameArea = BusinessLogicWindow.CalculateFrameArea(width, height, frameWidth) * (1E-6);
                double framePerimeter = BusinessLogicWindow.CalculatePerimeter(width, height, frameWidth);

                txtWindowArea.Text = windowArea.ToString() + "m\xB2";
                txtFrameArea.Text = frameArea.ToString() + "m\xB2";
                txtFramePerimeter.Text = framePerimeter.ToString() + "mm";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as System.Windows.Controls.TextBox).SelectAll();
        }
    }    
}
