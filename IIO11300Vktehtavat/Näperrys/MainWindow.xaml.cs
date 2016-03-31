using System.Collections.Generic;
using System.Windows;

namespace Näperrys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Palikka> palikat;
        public MainWindow()
        {
            InitializeComponent();

            palikat = new List<Palikka>();
            for (int i = 1; i != 51; ++i)
            {
                palikat.Add(new Palikka(string.Format("{0}. Palikka", i)));
            }
            listBox.ItemsSource = palikat;
        }
    }

    public class Palikka
    {
        public string Name { get; set; }
        public Palikka(string name)
        {
            Name = name;
        }
    }
}
