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
using PC.Classes;
namespace PC.Pages
{
    /// <summary>
    /// Логика взаимодействия для Datepc.xaml
    /// </summary>
    public partial class Datepc : Page
    {
        public Datepc()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Code.DB.ChangeTracker.Entries().ToList()
                    .ForEach(p => p.Reload());
                dbPC.ItemsSource = Code.DB.oborud.ToList();
                cbidnum.ItemsSource = Code.DB.oborud.ToList();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }



        private void deleteUsers_Click(object sender, RoutedEventArgs e)
        {
            var itemsDelete = dbPC.SelectedItems.Cast<oborud>().ToList();
            if (dbPC.SelectedItem != null)
            {
                if (MessageBox.Show($"Вы точно хотите удалить " +
                     $"{itemsDelete.Count()} элемент", "Внимание",
                     MessageBoxButton.YesNo, MessageBoxImage.Question)
                     == MessageBoxResult.Yes)
                {
                    try
                    {
                        Code.DB.oborud.RemoveRange(itemsDelete);
                        Code.DB.SaveChanges();
                        MessageBox.Show("Данные удалены");

                        Code.DB.ChangeTracker.Entries().ToList().ForEach(
                            p => p.Reload());
                        dbPC.ItemsSource = Code.DB.oborud.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }
            else
            {
                MessageBox.Show("выберите элементы которые" +
                    " хотите удалить");
                return;
            }
        }

        private void editusers_Click(object sender, RoutedEventArgs e)
        {
            var ID = (oborud)dbPC.SelectedItem;
            if (ID == null)
            {
                MessageBox.Show("Выберите строку для Изменения"); return;
            }
            dateinf.dateId = ID.ID;
            NavigationService.Navigate(new AddPc((sender as Button).DataContext as oborud));
        }

        private void newPC_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPc(null));
        }

        private void poick_Click(object sender, RoutedEventArgs e)
        { var date = Code.DB.oborud.ToList();
            var item = (oborud)cbidnum.SelectedItem;
            date = Code.DB.oborud.Where(w => w.zavod_numer == item.zavod_numer).ToList();
            dbPC.ItemsSource = date;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            cbidnum.Text = null;
            Code.DB.ChangeTracker.Entries().ToList().ForEach(
                            p => p.Reload());
            dbPC.ItemsSource = Code.DB.oborud.ToList();
        }
    }
}
