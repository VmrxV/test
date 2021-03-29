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
    /// Логика взаимодействия для AddPc.xaml
    /// </summary>
    public partial class AddPc : Page
    {
        public AddPc(oborud ID)
        {
            InitializeComponent();
            CbAudit.ItemsSource = Code.DB.Audit.ToList();
        }
        oborud _oborud = new oborud();
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "" | CbAudit.Text == ""| 
                Datetime.Text==""|
                zavod.Text=="")
            {
                MessageBox.Show("Не все поля введены");
            }

            //изменеие
            if (dateinf.dateId > 0)
            {
                try
                {
                    _oborud.ID = dateinf.dateId;
                    var uRow = Code.DB.oborud.Where(w => w.ID == dateinf.dateId).FirstOrDefault();
                    Code.DB.SaveChanges();
                    uRow.Name = Name.Text;
                    uRow.date = Convert.ToDateTime(Datetime.Text);
                    uRow.date_post = Convert.ToDateTime(Datetime.Text); ;
                    uRow.IdAudit = CbAudit.SelectedIndex + 1;
                    uRow.zavod_numer = zavod.Text;
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
                    _oborud.zavod_numer = zavod.Text;
                    _oborud.Name = Name.Text;
                    _oborud.date = Convert.ToDateTime(Datetime.Text);
                    _oborud.date_post = Convert.ToDateTime(Datetime.Text);
                    _oborud.IdAudit = CbAudit.SelectedIndex + 1;
                    Code.DB.oborud.Add(_oborud);
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

        private void bage_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (dateinf.dateId >0)
            {
                _oborud.ID = dateinf.dateId;
                //данные для обновления
                oborud edit = Code.DB.oborud.Find(dateinf.dateId);
                zavod.Text = edit.zavod_numer;
                Name.Text = Convert.ToString(edit.Name);
                Datetime.Text =Convert.ToString(edit.date_post);
                CbAudit.SelectedIndex = Convert.ToInt32(edit.IdAudit);
            }
        }
    }
}
