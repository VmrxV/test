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
using PC.Pages;
namespace PC.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Page
    {
        public AddUsers(Users ID)
        {
            InitializeComponent();
            Cbdolz.ItemsSource = Code.DB.dolz.ToList();
        }

        private void bage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
        Users _users = new Users();
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" | FirstName.Text == "" |
               LastName.Text == "" | Phone.Text==""|adres.Text==""|
               Cbdolz.Text=="")
            {
                MessageBox.Show("Не все поля введены");
            }

            if (dateinf.dateId > 0)
            {
                try
                {
                    _users.ID = dateinf.dateId;
                    var uRow = Code.DB.Users.Where(w => w.ID == dateinf.dateId).
                        FirstOrDefault();
                    Code.DB.SaveChanges();
                    uRow.Name = Name.Text;
                    uRow.LastName = LastName.Text;
                    uRow.FirstName = FirstName.Text;
                    uRow.Phone = Phone.Text;
                    uRow.Adrec = adres.Text;
                    uRow.Iddolz = Cbdolz.SelectedIndex + 1;
                    Code.DB.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    _users.FirstName = FirstName.Text;
                    _users.Name = Name.Text;
                    _users.LastName = LastName.Text;
                    _users.Iddolz = Cbdolz.SelectedIndex + 1;
                    _users.Phone = Phone.Text;
                    _users.Adrec = adres.Text;
                    Code.DB.Users.Add(_users);
                    Code.DB.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            MessageBox.Show("Данные добавлены");
            if (NavigationService.CanGoBack)
            {
                dateinf.dateId = 0;
                NavigationService.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (dateinf.dateId > 0)
            {
                _users.ID = dateinf.dateId;
                //данные для обновления
                Users edit = Code.DB.Users.Find(dateinf.dateId);
                LastName.Text = edit.LastName;
                Name.Text = Convert.ToString(edit.Name);
                FirstName.Text =edit.FirstName;
                Phone.Text = edit.Phone;
                adres.Text = edit.Adrec;
                Cbdolz.SelectedIndex =Convert.ToInt32(edit.Iddolz);
            }
        }
    }
}
