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
using System.Windows.Shapes;

namespace RESHOTEL_APP.View
{
    /// <summary>
    /// Interaction logic for cleanPlanning.xaml
    /// </summary>
    public partial class cleanPlanning : Window
    {
        private Model.Room rm = new Model.Room();
        private int featId { get; set; }
        private int roomId { get; set; }

        public cleanPlanning()
        {
            InitializeComponent();
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Controller.LoginController.logOut(this);
        }

        private void cpWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbl_username.Text = Model.Login.firstname + " " + Model.Login.lastname;
            //lb_clean.ItemsSource = rm.roomToClean();
        }

        private void cb_roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            featId = int.Parse(((ComboBoxItem)cb_roomType.SelectedItem).Tag.ToString());
            lb_clean.ItemsSource = rm.roomToCleanByFeat(featId);
        }

        private void btn_cleaned_Click(object sender, RoutedEventArgs e)
        {
            if (lb_clean.SelectedIndex >= 0)
            {
                dynamic lbItem = lb_clean.SelectedItem as dynamic;
                roomId = lbItem.idRoom;
                rm.setRoomStatus(roomId);
                lb_clean.ItemsSource = rm.roomToCleanByFeat(featId);
                MessageBox.Show("Chambre néttoyée", "Planning nettoyage", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Veuillez d'abord sélectionner une chambre à néttoyer", "Planning nettoyage", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
