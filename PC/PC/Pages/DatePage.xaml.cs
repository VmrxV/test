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

namespace PC.Pages
{
    /// <summary>
    /// Логика взаимодействия для DatePage.xaml
    /// </summary>
    public partial class DatePage : Page
    {
        public DatePage()
        {
            InitializeComponent();
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Dateusers());
        }

        private void pc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Datepc());
        }

        private void audit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Dateaudit());
        }
    }
}
