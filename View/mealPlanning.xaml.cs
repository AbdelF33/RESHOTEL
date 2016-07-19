using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace RESHOTEL_APP.View
{
    /// <summary>
    /// Interaction logic for mealPlanning.xaml
    /// </summary>
    public partial class mealPlanning : Window
    {

        public mealPlanning()
        {
            InitializeComponent();
        }

        private void mpWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbl_username.Text = Model.Login.firstname + " " + Model.Login.lastname;

            Model.Booking bk = new Model.Booking();
            int[] dinnersArray = new int[7];

            foreach (var item in bk.getNbDinners())
            {

                for (int i = 0; i <= 6; i++)
                {
                    if (DateTime.Now.AddDays(i).Date >= item.checkIn && DateTime.Now.AddDays(i).Date <= item.checkOut)
                    {
                        dinnersArray[i] += (int)item.nbDinner;
                    }
                }
                
            }

            tbl_date1.Text = DateTime.Now.ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner1.Text = dinnersArray[0].ToString();

            tbl_date2.Text = DateTime.Now.AddDays(1).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner2.Text = dinnersArray[1].ToString();

            tbl_date3.Text = DateTime.Now.AddDays(2).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner3.Text = dinnersArray[2].ToString();

            tbl_date4.Text = DateTime.Now.AddDays(3).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner4.Text = dinnersArray[3].ToString();

            tbl_date5.Text = DateTime.Now.AddDays(4).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner5.Text = dinnersArray[4].ToString();

            tbl_date6.Text = DateTime.Now.AddDays(5).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner6.Text = dinnersArray[5].ToString();

            tbl_date7.Text = DateTime.Now.AddDays(6).ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));
            tbl_nbDinner7.Text = dinnersArray[6].ToString();


        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Controller.LoginController.logOut(this);
        }

    }
}
