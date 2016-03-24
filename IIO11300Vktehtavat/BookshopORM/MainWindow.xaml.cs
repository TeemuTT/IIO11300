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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookshopORM
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            spRigth.DataContext = BLBookshop.GetTestBooks();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid.DataContext = BLBookshop.GetBooks(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book current = (Book)spRigth.DataContext;
                if (BLBookshop.UpdateBook(current) > 0)
                {
                    MessageBox.Show("Tallennus onnistui!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spRigth.DataContext = dataGrid.SelectedItem;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (btnNew.Content.ToString() == "Uusi")
            {
                Book book = new Book(0);
                book.Name = "Anna kirjan nimi";
                spRigth.DataContext = book;
                btnNew.Content = "Tallenna uusi kirja";
            } else
            {
                Book book = (Book)spRigth.DataContext;
                BLBookshop.InsertBook(book);
                dataGrid.DataContext = BLBookshop.GetBooks(true);
                btnNew.Content = "Uusi";
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book book = (Book)spRigth.DataContext;
                if (BLBookshop.DeleteBook(book))
                {
                    MessageBox.Show("Poistettu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
