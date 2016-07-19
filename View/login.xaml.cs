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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            Controller.LoginController lc = new Controller.LoginController();

            if (tb_login.Text.Equals("") || pb_password.Password.Equals(""))
            {
                MessageBox.Show("Veuillez remplir tous les champs du formulaire !", "RESHOTEL", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (!lc.checkAuth(tb_login.Text, pb_password.Password))
	            {
                    MessageBox.Show(lc.message, "RESHOTEL", MessageBoxButton.OK, MessageBoxImage.Error);
	            }else{
                    this.Close();
                }
            }
        }
    }
}
