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
    /// Логика взаимодействия для Dateaudit.xaml
    /// </summary>
    public partial class Dateaudit : Page
    {
        public Dateaudit()
        {
            InitializeComponent();
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Code.DB.ChangeTracker.Entries().ToList()
                    .ForEach(p => p.Reload());
                dbAudit.ItemsSource = Code.DB.Audit.ToList();
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void newUsers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добавить");
        }

        private void deleteUsers_Click(object sender, RoutedEventArgs e)
        {
            var itemsDelete = dbAudit.SelectedItems.Cast<Audit>().ToList();
            if (dbAudit.SelectedItem != null)
            {
                if (MessageBox.Show($"Вы точно хотите удалить " +
                     $"{itemsDelete.Count()} элемент", "Внимание",
                     MessageBoxButton.YesNo, MessageBoxImage.Question)
                     == MessageBoxResult.Yes)
                {
                    try
                    {
                        Code.DB.Audit.RemoveRange(itemsDelete);
                        Code.DB.SaveChanges();
                        MessageBox.Show("Данные удалены");

                        Code.DB.ChangeTracker.Entries().ToList().ForEach(
                            p => p.Reload());
                        dbAudit.ItemsSource = Code.DB.Audit.ToList();
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
            var ID = (Audit)dbAudit.SelectedItem;
            if (ID == null)
            {
                MessageBox.Show("Выберите строку для Изменения"); return;
            }
            dateinf.dateId = ID.ID;
            NavigationService.Navigate(new AddAudit((sender as Button).DataContext as Audit));
        }

        private void newAudit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAudit(null));
        }
    }
}
