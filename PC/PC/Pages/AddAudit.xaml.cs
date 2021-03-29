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
    /// Логика взаимодействия для AddAudit.xaml
    /// </summary>
    public partial class AddAudit : Page
    {
        public AddAudit(Audit ID)
        {
            InitializeComponent();
            
            CbUsers.ItemsSource = Code.DB.Users.ToList();
        }
        Audit _audit = new Audit();
        private void bage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            //доавбление
            if(Name.Text=="" |CbUsers.Text=="")
            {
                MessageBox.Show("Не все поля введены");
            }
            //изменеие
            if (dateinf.dateId > 0)
            {
                try
                {
                    _audit.ID = dateinf.dateId;
                    var uRow = Code.DB.Audit.Where(w => w.ID == dateinf.dateId).FirstOrDefault();
                    Code.DB.SaveChanges();
                    uRow.Name = Name.Text;
                    uRow.idUsers = CbUsers.SelectedIndex + 1;
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
                    _audit.idUsers = CbUsers.SelectedIndex + 1;
                    _audit.Name = Name.Text;
                    Code.DB.Audit.Add(_audit);
                    Code.DB.SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show("Данные добавлены");
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (dateinf.dateId > 0)
            {
                _audit.ID = dateinf.dateId;
                //данные для обновления
                Audit edit = Code.DB.Audit.Find(dateinf.dateId);
                Name.Text = edit.Name;
                CbUsers.SelectedIndex = Convert.ToInt32(edit.idUsers);
            }
        }        
    }
}
