using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
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

namespace H10BookshopwEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopEntities ctx;
        ObservableCollection<Book> localBooks;
        ICollectionView view;
        bool isBooks;

        public MainWindow()
        {
            InitializeComponent();

            ctx = new BookShopEntities();
            ctx.Books.Load();
            localBooks = ctx.Books.Local;
            cbCountries.DataContext = localBooks.Select(n => n.country).Distinct();
            view = CollectionViewSource.GetDefaultView(localBooks);
        }

        private void btnHaeAsiakkaat_Click(object sender, RoutedEventArgs e)
        {
            var customers = ctx.Customers.ToList();
            dataGrid.DataContext = customers;
            isBooks = false;
        }

        private void btnHaeKirjat_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.DataContext = localBooks;
            isBooks = true;
            cbCountries.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ctx.SaveChanges();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            Book newBook;
            if (btnNew.Content.ToString() == "Uusi")
            {
                newBook = new Book();
                newBook.name = "Anna kirjan nimi";
                spRigth.DataContext = newBook;
                btnNew.Content = "Tallenna";
            } else
            {
                newBook = (Book)spRigth.DataContext;
                ctx.Books.Add(newBook);
                ctx.SaveChanges();
                btnNew.Content = "Uusi";
                MessageBox.Show("Kirja tallennettu");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Book book = (Book)dataGrid.SelectedItem;
            if (MessageBox.Show("Poistetaanko kirja?", "Kirjakauppa", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                ctx.Books.Remove(book);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isBooks)
            {
                spRigth.DataContext = (sender as DataGrid).SelectedItem;
            } else
            {
                spCustomer.DataContext = (sender as DataGrid).SelectedItem;
            }
        }

        private void btnHaeTilaukset_Click(object sender, RoutedEventArgs e)
        {
            Customer current = spCustomer.DataContext as Customer;
            string msg = string.Format("Asiakkaalla {0} on {1} tilausta:\n", current.DisplayName, current.OrderCount);
            foreach (var order in current.Orders)
            {
                msg += string.Format("Tilaus {0} sisältää {1} tilausriviä:\n", order.odate, order.Orderitems.Count);
                decimal cost = 0;
                foreach (var oitem in order.Orderitems)
                {
                    cost += oitem.count * oitem.Book.price.Value;
                    msg += string.Format("- kirja {0} {1} kappaletta\n", oitem.Book.name, oitem.count);
                }
                msg += string.Format("-- Hinta: {0}\n", cost);
            }
            MessageBox.Show(msg);
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.Filter = MyCountryFilter;
        }

        private bool MyCountryFilter(object item)
        {
            if (cbCountries.SelectedIndex == -1)
                return true;
            else return (item as Book).country.Contains(cbCountries.SelectedItem.ToString());
        }
    }
}
