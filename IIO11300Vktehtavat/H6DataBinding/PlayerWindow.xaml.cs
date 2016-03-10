using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace H6DataBinding
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        List<HockeyPlayer> pelaajat;
        int osoitin = 0;
        public PlayerWindow()
        {
            InitializeComponent();
            pelaajat = TestHockeyBench.GetThreePlayers();
            dataGrid.ItemsSource = pelaajat;
            SetData();
        }

        private void SetData()
        {
            stackLeft.DataContext = pelaajat[osoitin];
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedIndex > -1 && dataGrid.SelectedIndex < pelaajat.Count)
            {
                osoitin = dataGrid.SelectedIndex;
                SetData();
            }
        }
    }
}
