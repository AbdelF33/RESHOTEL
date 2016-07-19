using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;


namespace RESHOTEL_APP.View
{
    /// <summary>
    /// Interaction logic for hbWindow.xaml
    /// </summary>
    public partial class hbWindow : Window
    {
        #region properties
        private Model.Room rm = new Model.Room();
        private Model.Payment pyt = new Model.Payment();
        private Model.Option opt = new Model.Option();
        private Model.Booking bk = new Model.Booking();
        private Model.Customer cst = new Model.Customer();
        private Model.Meal ml = new Model.Meal();
        private Model.Promotion pr = new Model.Promotion();
        private Tools.Mail mail = new Tools.Mail();

        private int roomId { get; set; }
        private int staffId { get; set; }
        private int bookId { get; set; }
        private int roomIndex { get; set; }
        private int featureId { get; set; }
        private int featPlId { get; set; }
        private int custId { get; set; }
        private byte addEdit { get; set; }
        private List<CheckBox> optionChk { get; set; }
        private decimal totalRate { get; set; }
        private decimal dinnersRate { get; set; }
        private decimal breakfastsRate { get; set; }
        private decimal optionsRate { get; set; }
        private decimal nightsRate { get; set; }
        private int paymentID { get; set; }
        private int nbDinner { get; set; }
        private int nbBreakfast { get; set; }
        private DateTime checkin { get; set; }
        private DateTime checkout { get; set; }
        private List<int> chkOptions { get; set; }
        private int nbNights { get; set; }
        private int nbResa { get; set; }
        private int promoId { get; set; }
        private decimal tPrice { get; set; }
        #endregion

        public hbWindow()
        {
            InitializeComponent();
            optionChk = new List<CheckBox>()
            {
                chk_cardio,
                chk_jacuzzi,
                chk_wifi,
                chk_tv,
                chk_tennis
            };
        }


        #region window control

        private void hbWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            tbl_username.Text = Model.Login.firstname + " " + Model.Login.lastname;
            staffId = Model.Login.id_staff;
            if (Model.Login.profile.Equals("Responsable réservation") || Model.Login.profile.Equals("Administrateur"))
            {
                btn_Promotion.Visibility = Visibility.Visible;
            }
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Controller.LoginController.logOut(this);
        }

        private void gr_newRes_Loaded(object sender, RoutedEventArgs e)
        {
            cb_payment.ItemsSource = pyt.getAllmethod();
        }

        private void btn_dashboard_Click(object sender, RoutedEventArgs e)
        {
            //showDashboard();
            clearAll();
            showHide(true, false, false, false, false);
        }

        private void btn_planning_Click(object sender, RoutedEventArgs e)
        {
            clearAll();
            showHide(false, false, true, false, false);
        }

        private void btn_modifBook_Click(object sender, RoutedEventArgs e)
        {
            if (lb_bookings.SelectedIndex >= 0)
            {
                addEdit = 2;
                //use dynamic as type to cast your anonymous object to few things to do 
                dynamic slctBook = lb_bookings.SelectedItem as dynamic;
                bookId = slctBook.bookId;
                custId = slctBook.customerId;
                nbResa = slctBook.nbResa;
                

                int result = slctBook.checkIn.Date.CompareTo(DateTime.Now.AddDays(+1).Date);
                if (result < 0)
                {
                    MessageBox.Show("Toute modification de réservation doit être demandée par le client 24H à l’avance !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    //showBookForm();
                    showHide(false, true, false, false, false);
                    sp_newCustomer.IsEnabled = true;
                    chk_newCustomer.IsEnabled = false;
                    chk_newCustomer.IsChecked = true;
                    sp_customersList.IsEnabled = false;
                    sp_srchCustomer.IsEnabled = false;
                    btn_book.Visibility = Visibility.Collapsed;
                    btn_editBook.Visibility = Visibility.Visible;
                    //tb_checkIn.Visibility = Visibility.Collapsed;
                    //tb_checkOut.Visibility = Visibility.Collapsed;
                    //dp_modifCheckin.Visibility = Visibility.Visible;
                    //dp_modifCheckout.Visibility = Visibility.Visible;

                    popBookForm();
                    popOptionLV();
                    popRoomInfo();
                }

            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner une réservation !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_changeRoom_Click(object sender, RoutedEventArgs e)
        {
            //showDashboard();
            showHide(true, false, false, false, false);
            changeRoom(true);
        }

        private void btn_cancelChoice_Click(object sender, RoutedEventArgs e)
        {
            showHide(false, true, false, false, false);
            changeRoom(false);
        }


        #endregion


        #region actions
        private void btn_toolBook_Click(object sender, RoutedEventArgs e)
        {
            if (lb_rooms.SelectedIndex >= 0)
            {
                addEdit = 1;
                //use dynamic as type to cast your anonymous object to few things to do 
                dynamic slctRoom = lb_rooms.SelectedItem as dynamic;
                roomId = slctRoom.idRoom;
                roomIndex = lb_rooms.SelectedIndex;

                //showBookForm();
                showHide(false, true, false, false, false);
                popRoomInfo();
                tb_checkIn.Text = Convert.ToDateTime(dp_checkin.SelectedDate).ToString("dd/MM/yyyy");
                tb_checkOut.Text = Convert.ToDateTime(dp_checkout.SelectedDate).ToString("dd/MM/yyyy");
            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner une chambre à réserver !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_book_Click(object sender, RoutedEventArgs e)
        {
            if (controlBookForm())
            {
                if (chk_newCustomer.IsChecked == true)
                {
                    cst.addCustomer(tb_firstname.Text, tb_lastname.Text, tb_email.Text, tb_phone.Text, 1);
                    custId = Model.Customer.lastInsertId;
                }
                else
                {
                    int nbResa = 0;

                    dynamic slctCust = lv_customers.SelectedItem as dynamic;
                    custId = slctCust.id;
                    nbResa = slctCust.nbReservation;

                    //foreach (var item in lv_customers.SelectedItems)
                    //{
                    //    var custMdl = (Model.Customer)item;
                    //    custId = custMdl.id;
                    //    nbResa = custMdl.nbReservation;
                    //}

                    cst.updateNbResa(custId, nbResa + 1);
                }

                //get payment id from combobox payment
                dynamic slctpayment = cb_payment.SelectedItem as dynamic;
                paymentID = slctpayment.id;

                currentBookInfo();
                totalRate = calculateRates(nbNights, nbDinner, nbBreakfast, chkOptions);

                //bk.addBooking(DateTime.Now, checkin, checkout, true, nbBreakfast, nbDinner, custId, 2, roomId, paymentID, totalRate);
                bk.addBooking(DateTime.Now, checkin, checkout, true, nbBreakfast, nbDinner, custId, staffId, roomId, paymentID, totalRate);
                foreach (var item in chkOptions)
                {
                    bk.addBookOption(item, 0);
                }

                clearAll();

                MessageBox.Show("Réservation effectuée avec succès !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Information);

                showHide(true, false, false, false, false);

                checkAvailableRooms();
            }
        }

        private void btn_selectRoom_Click(object sender, RoutedEventArgs e)
        {
            if (lb_rooms.SelectedIndex >= 0)
            {
                //use dynamic as type to cast your anonymous object to few things to do 
                dynamic slctRoom = lb_rooms.SelectedItem as dynamic;
                roomId = slctRoom.idRoom;

                tb_checkIn.Text = Convert.ToDateTime(dp_checkin.SelectedDate).ToString("dd/MM/yyyy");
                tb_checkOut.Text = Convert.ToDateTime(dp_checkout.SelectedDate).ToString("dd/MM/yyyy");

                //showBookForm();
                showHide(false, true, false, false, false);
                popRoomInfo();
                changeRoom(false);
            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner une chambre à réserver !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void cb_roomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            featureId = int.Parse(((ComboBoxItem)cb_roomType.SelectedItem).Tag.ToString());
            if (dp_checkin.Text != "" && dp_checkout.Text != "")
            {
                lb_rooms.ItemsSource = rm.getFreeRooms(featureId, Convert.ToDateTime(dp_checkin.Text), Convert.ToDateTime(dp_checkout.Text));
            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner une date d'arrivée et une date de départ !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void cb_bookRoomType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cb_bookRoomType.SelectedIndex >= 0)
            //{
            //    featPlId = int.Parse(((ComboBoxItem)cb_bookRoomType.SelectedItem).Tag.ToString());
            //    if (dp_begin.Text == "" || dp_end.Text == "")
            //    {
            //        lb_bookings.ItemsSource = bk.getBookRooms(featPlId);
            //    }
            //    else
            //    {
            //        lb_bookings.ItemsSource = bk.getBRBydate(featPlId, Convert.ToDateTime(dp_begin.Text), Convert.ToDateTime(dp_end.Text));
            //    }
            //}
        }

        private void btn_searchByDate_Click(object sender, RoutedEventArgs e)
        {
            if (cb_bookRoomType.SelectedIndex >= 0)
            {
                featPlId = int.Parse(((ComboBoxItem)cb_bookRoomType.SelectedItem).Tag.ToString());
                if (dp_begin.Text == "" || dp_end.Text == "")
                {
                    lb_bookings.ItemsSource = bk.getBookRooms(featPlId);
                }
                else
                {
                    lb_bookings.ItemsSource = bk.getBRBydate(featPlId, Convert.ToDateTime(dp_begin.Text), Convert.ToDateTime(dp_end.Text));
                }
            }
            else
            {
                MessageBox.Show("Veuillez d'abord remplir le formulaire !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_searchByClient_Click(object sender, RoutedEventArgs e)
        {
            lb_bookings.ItemsSource = bk.getBRByclient(tb_searchByClient.Text);
        }

        private void chk_newCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (chk_newCustomer.IsChecked == true)
            {
                sp_newCustomer.IsEnabled = true;
                sp_customersList.IsEnabled = false;
                sp_srchCustomer.IsEnabled = false;
                lv_customers.SelectedIndex = -1;
                wp_discount.Visibility = Visibility.Collapsed;
            }
            else
            {
                sp_customersList.IsEnabled = true;
                sp_srchCustomer.IsEnabled = true;
                sp_newCustomer.IsEnabled = false;
            }
        }

        private void btn_searchClient_Click(object sender, RoutedEventArgs e)
        {
            lv_customers.ItemsSource = cst.getCustByName(tb_searchClient.Text);
        }

        private void btn_editBook_Click(object sender, RoutedEventArgs e)
        {
            if (controlBookForm())
            {
                cst.updateCustomer(custId, tb_firstname.Text, tb_lastname.Text, tb_email.Text, tb_phone.Text);

                dynamic slctpayment = cb_payment.SelectedItem as dynamic;
                int paymentID = slctpayment.id;

                currentBookInfo();
                totalRate = calculateRates(nbNights, nbDinner, nbBreakfast, chkOptions);

                bk.setBooking(bookId, checkin, checkout, nbBreakfast, nbDinner, roomId, totalRate);
                bk.deleteBookOpt(bookId);

                foreach (var item in chkOptions)
                {
                    bk.addBookOption(item, bookId);
                }

                clearAll();

                MessageBox.Show("Réservation modifiée avec succès !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Information);

                showHide(true, false, false, false, false);

                checkAvailableRooms();
            }
        }

        private void btn_cancelBook_Click(object sender, RoutedEventArgs e)
        {
            if (lb_bookings.SelectedIndex >= 0)
            {
                //use dynamic as type to cast your anonymous object to few things to do 
                dynamic slctBook = lb_bookings.SelectedItem as dynamic;
                bookId = slctBook.bookId;
                custId = slctBook.customerId;

                int result = slctBook.checkIn.Date.CompareTo(DateTime.Now.AddDays(+2).Date);
                if (result < 0)
                {
                    MessageBox.Show("Toute annulation de réservation doit être demandée par le client 48H à l’avance !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    bk.deleteBookOpt(bookId);
                    bk.cancelBooking(bookId);
                    MessageBox.Show("Votre réservation est annulée !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner une réservation !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_genRate_Click(object sender, RoutedEventArgs e)
        {
            currentBookInfo();
            totalRate = calculateRates(nbNights, nbDinner, nbBreakfast, chkOptions);
            sp_allPrices.Visibility = Visibility.Visible;
            tbl_totalNightsPrice.Text = nightsRate.ToString();
            tbl_totalOptPrice.Text = optionsRate.ToString();
            tbl_totalBFPrice.Text = breakfastsRate.ToString();
            tbl_totalDnrPrice.Text = dinnersRate.ToString();
            tbl_totalPrice.Text = totalRate.ToString();
            lbl_nbNights.Text = nbNights.ToString();
        }

        private void btn_addMeal_Click(object sender, RoutedEventArgs e)
        {
            if (lb_bookings.SelectedIndex >= 0)
            {
                //use dynamic as type to cast your anonymous object to few things to do 
                dynamic slctBook = lb_bookings.SelectedItem as dynamic;
                bookId = slctBook.bookId;
                custId = slctBook.customerId;
                nbDinner = slctBook.nbDinner;
                tPrice = slctBook.price;
                checkout = slctBook.checkOut;


                if (checkout <= DateTime.Now)
                {
                    MessageBox.Show("Impossible de modifier une réservation expirée !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    showHide(false, true, false, false, false);
                    gr_mealDemand.Visibility = Visibility.Visible;
                    gr_customer.IsEnabled = false;
                    gr_room.IsEnabled = false;
                    gr_option.IsEnabled = false;
                    btn_book.Visibility = Visibility.Collapsed;
                    btn_editBook.Visibility = Visibility.Collapsed;
                    btn_changeRoom.Visibility = Visibility.Collapsed;

                    popBookForm();
                    popOptionLV();
                    popRoomInfo();
                }

            }
            else
            {
                MessageBox.Show("Vous devez d'abord sélectionner une réservation !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_addMealValidate_Click(object sender, RoutedEventArgs e)
        {
            if (dp_dinnerDate.Text == "")
            {
                MessageBox.Show("Veuillez d'abord choisir une date !", "Demande de repas", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                int result = Convert.ToDateTime(dp_dinnerDate.Text).CompareTo(DateTime.Now.AddDays(+1).Date);
                if (result < 0)
                {
                    MessageBox.Show("Toute demande de repas doit être faite par le client 24H à l’avance !", "Demande de repas", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
                else
                {
                    if (cb_nbDinner.SelectedIndex >= 0)
                    {
                        int selected = int.Parse(((ComboBoxItem)cb_nbDinner.SelectedItem).Tag.ToString());
                        nbNights = (checkout.Date - Convert.ToDateTime(dp_dinnerDate.Text)).Days;
                        foreach (var item in ml.getMealPrice())
                        {
                            dinnersRate = (nbDinner * nbNights) * item.dinnerR;
                        }
                        bk.updateNbDinner(bookId, nbDinner + selected, tPrice + dinnersRate);
                        MessageBox.Show("Votre demande a été prise en compte !", "Réservation", MessageBoxButton.OK, MessageBoxImage.Information);
                        clearAll();
                        showHide(false, false, true, false, false);
                    }
                    else
                    {
                        MessageBox.Show("Veuillez d'abord saisir le nombre de personnes !", "Demande de repas", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
            }

        }

        private void btn_pdfInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (lb_bookings.SelectedIndex >= 0)
            {
                dynamic slctBook = lb_bookings.SelectedItem as dynamic;
                bookId = slctBook.bookId;

                Model.Invoice ic = new Model.Invoice();
                ic.pdfGen(bookId);
            }
            else
            {
                MessageBox.Show("Veuillez d'abord saisir une réservation dans le planning !", "Facturation", MessageBoxButton.OK, MessageBoxImage.Hand);
            }

        }

        #endregion


        #region promotions
        private void btn_Promotion_Click(object sender, RoutedEventArgs e)
        {
            showHide(false, false, false, true, false);
        }

        private void btn_searchPromo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dp_startDate.SelectedDate.ToString()) || string.IsNullOrEmpty(dp_endDate.SelectedDate.ToString()) || cb_promoRmType.SelectedIndex < 0)
            {
                lb_promotions.ItemsSource = pr.getAllPromo();
            }
            else if (cb_promoRmType.SelectedIndex >= 0 && string.IsNullOrEmpty(dp_startDate.SelectedDate.ToString()) && string.IsNullOrEmpty(dp_endDate.SelectedDate.ToString()))
            {
                featureId = int.Parse(((ComboBoxItem)cb_promoRmType.SelectedItem).Tag.ToString());
                lb_promotions.ItemsSource = pr.getPromoByfeature(featureId);
            }
            else
            {
                featureId = int.Parse(((ComboBoxItem)cb_promoRmType.SelectedItem).Tag.ToString());
                lb_promotions.ItemsSource = pr.getPromoByDate(featureId, Convert.ToDateTime(dp_startDate.SelectedDate), Convert.ToDateTime(dp_endDate.SelectedDate));
            }
        }

        private void btn_addPromo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dp_addStartDate.SelectedDate.ToString()) ||  string.IsNullOrEmpty(dp_addEndDate.SelectedDate.ToString()) || string.IsNullOrEmpty(tb_addCode.Text) || string.IsNullOrEmpty(tb_addDiscount.Text) || cb_addRmType.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs du formulaire !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                DateTime start = Convert.ToDateTime(dp_addStartDate.SelectedDate);
                DateTime end = Convert.ToDateTime(dp_addEndDate.SelectedDate);
                featureId = int.Parse(((ComboBoxItem)cb_addRmType.SelectedItem).Tag.ToString());
                int discount = int.Parse(tb_addDiscount.Text);
                pr.addPromo(start, end, tb_addCode.Text, discount, featureId);
                MessageBox.Show("Promotion ajoutée avec succès !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Information);
                clearPromo();
            }
        }

        private void btn_modifPromo_Click(object sender, RoutedEventArgs e)
        {
            if (lb_promotions.SelectedIndex >= 0)
            {
                dynamic slctPromo = lb_promotions.SelectedItem as dynamic;
                tb_addCode.Text = slctPromo.code.ToString();
                tb_addDiscount.Text = slctPromo.discount.ToString();
                dp_addStartDate.Text = slctPromo.startDate.ToString();
                dp_addEndDate.Text = slctPromo.endDate.ToString();
                cb_addRmType.SelectedIndex = (slctPromo.id_features)-1;
                promoId = slctPromo.id;
                btn_editPromo.IsEnabled = true;
                btn_addPromo.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Veuillez d'abord sélectionner une promotion à modifier !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void btn_editPromo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(dp_addStartDate.SelectedDate.ToString()) || string.IsNullOrEmpty(dp_addEndDate.SelectedDate.ToString()) || string.IsNullOrEmpty(tb_addCode.Text) || string.IsNullOrEmpty(tb_addDiscount.Text) || cb_addRmType.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs du formulaire !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                DateTime start = Convert.ToDateTime(dp_addStartDate.SelectedDate);
                DateTime end = Convert.ToDateTime(dp_addEndDate.SelectedDate);
                featureId = int.Parse(((ComboBoxItem)cb_addRmType.SelectedItem).Tag.ToString());
                int discount = int.Parse(tb_addDiscount.Text);
                pr.updatePromo(promoId, start, end, tb_addCode.Text, discount, featureId);
                MessageBox.Show("Promotion modifiée avec succès !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Information);
                clearPromo();
            }
        }

        private void btn_cancelPromo_Click(object sender, RoutedEventArgs e)
        {
            if (lb_promotions.SelectedIndex >= 0)
            {
                dynamic slctPromo = lb_promotions.SelectedItem as dynamic;
                promoId = slctPromo.id;
                if (MessageBox.Show("Etes vous sûr de vouloir supprimer cette promotion ?", "Promotions", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    pr.deletePromo(promoId);
                    MessageBox.Show("Promotion supprimée avec succès !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearPromo();
                }
                else
                {
                    MessageBox.Show("La suppression a été annulé par l'utilisateur !", "Promotions", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }
        #endregion


        #region customers
        private void btn_customer_Click(object sender, RoutedEventArgs e)
        {
            showHide(false, false, false, false, true);
        }

        private void btn_srchCust_Click(object sender, RoutedEventArgs e)
        {
            lv_customers2.ItemsSource = cst.getCustByName(tb_custName.Text);
        }

        private void btn_modifCust_Click(object sender, RoutedEventArgs e)
        {
            dynamic slctCust = lv_customers2.SelectedItem as dynamic;
            custId = slctCust.id;
            tb_lastname2.Text = slctCust.lastname;
            tb_firstname2.Text = slctCust.firstname;
            tb_email2.Text = slctCust.email;
            tb_phone2.Text = slctCust.phone;
            btn_editCust.IsEnabled = true;
            btn_addCust.IsEnabled = false;
        }

        private void btn_addCust_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_lastname2.Text) || !string.IsNullOrEmpty(tb_firstname2.Text) || !string.IsNullOrEmpty(tb_email2.Text) || !string.IsNullOrEmpty(tb_phone2.Text))
            {
                cst.addCustomer(tb_firstname2.Text, tb_lastname2.Text, tb_email2.Text, tb_phone2.Text, 0);
                MessageBox.Show("Client ajouté avec succès !", "Ajout client", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomers();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs du formulaire !", "Ajout client", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void btn_editCust_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_lastname2.Text) || !string.IsNullOrEmpty(tb_firstname2.Text) || !string.IsNullOrEmpty(tb_email2.Text) || !string.IsNullOrEmpty(tb_phone2.Text))
            {
                cst.updateCustomer(custId, tb_firstname2.Text, tb_lastname2.Text, tb_email2.Text, tb_phone2.Text);
                MessageBox.Show("Client modifié avec succès !", "Ajout client", MessageBoxButton.OK, MessageBoxImage.Information);
                clearCustomers();
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs du formulaire !", "Ajout client", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        #endregion


        #region functions

        /// <summary>
        /// function for showing or hiding element
        /// </summary>
        /// <param name="dashboard"></param>
        /// <param name="bookForm"></param>
        /// <param name="planning"></param>
        private void showHide(bool dashboard, bool bookForm, bool planning, bool promotion, bool customer)
        {
            if (dashboard)
            {
                gr_dashboard.Visibility = Visibility.Visible;
                btn_toolBook.Visibility = Visibility.Visible;
                wp_booking.Visibility = Visibility.Visible;
                gr_manager.Visibility = Visibility.Visible;
                gr_actions.Visibility = Visibility.Visible;
                gr_newResa.Visibility = Visibility.Hidden;
                gr_planning.Visibility = Visibility.Hidden;
                wp_planning.Visibility = Visibility.Hidden;
                btn_cancelBook.Visibility = Visibility.Collapsed;
                btn_modifBook.Visibility = Visibility.Collapsed;
                btn_book.Visibility = Visibility.Collapsed;
                btn_changeRoom.Visibility = Visibility.Collapsed;
                btn_editBook.Visibility = Visibility.Collapsed;
                chk_newCustomer.IsEnabled = true;
                //chk_newCustomer.IsChecked = true;
                lb_rooms.SelectedIndex = -1;
                lb_bookings.SelectedIndex = -1;
                wp_discount.Visibility = Visibility.Collapsed;
                lv_customers.SelectedIndex = -1;
                gr_promo.Visibility = Visibility.Hidden;
                btn_pdfInvoice.Visibility = Visibility.Collapsed;
                btn_addMeal.Visibility = Visibility.Collapsed;
                gr_mealDemand.Visibility = Visibility.Hidden;
                gr_custs.Visibility = Visibility.Hidden;
            }
            else if (bookForm)
            {
                gr_newResa.Visibility = Visibility.Visible;
                btn_book.Visibility = Visibility.Visible;
                btn_changeRoom.Visibility = Visibility.Visible;
                gr_manager.Visibility = Visibility.Visible;
                gr_actions.Visibility = Visibility.Visible;
                gr_customer.IsEnabled = true;
                gr_room.IsEnabled = true;
                gr_option.IsEnabled = true;
                btn_toolBook.Visibility = Visibility.Collapsed;
                gr_dashboard.Visibility = Visibility.Hidden;
                gr_planning.Visibility = Visibility.Hidden;
                wp_planning.Visibility = Visibility.Collapsed;
                wp_booking.Visibility = Visibility.Collapsed;
                btn_cancelBook.Visibility = Visibility.Collapsed;
                btn_modifBook.Visibility = Visibility.Collapsed;
                btn_cancelChoice.Visibility = Visibility.Collapsed;
                btn_selectRoom.Visibility = Visibility.Collapsed;
                gr_promo.Visibility = Visibility.Hidden;
                btn_pdfInvoice.Visibility = Visibility.Collapsed;
                btn_addMeal.Visibility = Visibility.Collapsed;
                gr_mealDemand.Visibility = Visibility.Hidden;
                gr_custs.Visibility = Visibility.Hidden;
            }
            else if (planning)
            {
                gr_planning.Visibility = Visibility.Visible;
                wp_planning.Visibility = Visibility.Visible;
                btn_cancelBook.Visibility = Visibility.Visible;
                btn_modifBook.Visibility = Visibility.Visible;
                gr_dashboard.Visibility = Visibility.Hidden;
                gr_newResa.Visibility = Visibility.Hidden;
                wp_booking.Visibility = Visibility.Hidden;
                btn_changeRoom.Visibility = Visibility.Collapsed;
                btn_book.Visibility = Visibility.Collapsed;
                btn_editBook.Visibility = Visibility.Collapsed;
                btn_toolBook.Visibility = Visibility.Collapsed;
                lb_bookings.SelectedIndex = -1;
                wp_discount.Visibility = Visibility.Collapsed;
                lv_customers.SelectedIndex = -1;
                gr_promo.Visibility = Visibility.Hidden;
                gr_manager.Visibility = Visibility.Visible;
                gr_actions.Visibility = Visibility.Visible;
                btn_addMeal.Visibility = Visibility.Visible;
                gr_mealDemand.Visibility = Visibility.Hidden;
                gr_custs.Visibility = Visibility.Hidden;
                if (Model.Login.profile.Equals("Responsable facturation") || Model.Login.profile.Equals("Administrateur"))
                {
                    btn_pdfInvoice.Visibility = Visibility.Visible;
                }
            }
            else if (promotion)
            {
                gr_promo.Visibility = Visibility.Visible;
                gr_planning.Visibility = Visibility.Hidden;
                gr_dashboard.Visibility = Visibility.Hidden;
                gr_newResa.Visibility = Visibility.Hidden;
                gr_manager.Visibility = Visibility.Collapsed;
                gr_actions.Visibility = Visibility.Collapsed;
                gr_custs.Visibility = Visibility.Hidden;
            }
            else if(customer)
            {
                gr_custs.Visibility = Visibility.Visible;
                gr_promo.Visibility = Visibility.Hidden;
                gr_planning.Visibility = Visibility.Hidden;
                gr_dashboard.Visibility = Visibility.Hidden;
                gr_newResa.Visibility = Visibility.Hidden;
                gr_manager.Visibility = Visibility.Collapsed;
                gr_actions.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// function to clear all elements of dashboard
        /// </summary>
        private void clearAll()
        {
            //lb_rooms.ItemsSource = null;
            //lb_rooms.Items.Clear();
            //lb_rooms.Items.Remove(lb_rooms.SelectedItem);
            //lb_rooms.Items.RemoveAt(roomIndex);

            lb_rooms.ItemsSource = null;
            lb_bookings.ItemsSource = null;

            lbl_roomNb.Text = "";
            lbl_roomCapacity.Text = "";
            lbl_roomFloor.Text = "";
            lbl_roomPrice.Text = "";

            tb_firstname.Text = "";
            tb_lastname.Text = "";
            tb_email.Text = "";
            tb_phone.Text = "";
            tb_checkIn.Text = "";
            tb_checkOut.Text = "";

            //dp_checkin.Text = "";
            //dp_checkout.Text = "";

            cb_breakfast.SelectedIndex = -1;
            cb_dinner.SelectedIndex = -1;
            cb_payment.SelectedIndex = -1;

            foreach (var item in optionChk)
            {
                item.IsChecked = false;
            }

            sp_allPrices.Visibility = Visibility.Hidden;
            wp_discount.Visibility = Visibility.Collapsed;
            wp_tmpDiscount.Visibility = Visibility.Collapsed;
            tbl_totalNightsPrice.Text = "";
            tbl_totalOptPrice.Text = "";
            tbl_totalBFPrice.Text = "";
            tbl_totalDnrPrice.Text = "";
            tbl_totalPrice.Text = "";
            lbl_nbNights.Text = "";
        }

        /// <summary>
        /// function to populate room information in booking form
        /// </summary>
        private void popRoomInfo()
        {
            foreach (var item in rm.getRoomInfo(roomId))
            {
                lbl_roomNb.Text = item.number.ToString();
                lbl_roomCapacity.Text = item.capacity.ToString();
                lbl_roomFloor.Text = item.floor.ToString();
                lbl_roomPrice.Text = item.price1n.ToString();
            }
        }

        /// <summary>
        /// this function allow to populate booking form, from selected booking planning
        /// </summary>
        private void popBookForm()
        {
            foreach (var item in bk.getBookInfo(bookId))
            {
                roomId = (int)item.roomId;
                tb_firstname.Text = item.firstname.ToString();
                tb_lastname.Text = item.lastname.ToString();
                tb_email.Text = item.mail.ToString();
                tb_phone.Text = item.phone.ToString();
                tb_checkIn.Text = item.checkIn.ToString("dd/MM/yyyy");
                tb_checkOut.Text = item.checkOut.ToString("dd/MM/yyyy");
                cb_breakfast.SelectedIndex = item.nbBreakfast;
                cb_dinner.SelectedIndex = item.nbDinner;
                cb_payment.SelectedIndex = item.paymentId;
            }
        }

        /// <summary>
        /// function for populate checked info
        /// </summary>
        private void popOptionLV()
        {
            List<int> test = new List<int>();
            foreach (var item in bk.getBookOptionInfo(bookId))
            {
                foreach (var chkItem in optionChk)
                {
                    if (item.bookOptId == int.Parse(chkItem.Tag.ToString()))
                    {
                        chkItem.IsChecked = true;
                    }
                }
            }
        }

        /// <summary>
        /// function to show or hide validate room button
        /// </summary>
        /// <param name="ctrl"></param>
        private void changeRoom(bool ctrl)
        {
            if (ctrl == true)
            {
                btn_dashboard.IsEnabled = false;
                btn_planning.IsEnabled = false;
                btn_modifBook.IsEnabled = false;
                btn_selectRoom.Visibility = Visibility.Visible;
                btn_cancelChoice.Visibility = Visibility.Visible;
                btn_changeRoom.Visibility = Visibility.Collapsed;
                btn_toolBook.Visibility = Visibility.Collapsed;
                btn_editBook.Visibility = Visibility.Collapsed;
                btn_book.Visibility = Visibility.Collapsed;
            }
            else
            {
                btn_dashboard.IsEnabled = true;
                btn_planning.IsEnabled = true;
                btn_modifBook.IsEnabled = true;
                btn_changeRoom.Visibility = Visibility.Visible;
                btn_selectRoom.Visibility = Visibility.Hidden;
                btn_cancelChoice.Visibility = Visibility.Hidden;
                btn_book.Visibility = addEdit == 1 ? Visibility.Visible : Visibility.Collapsed;
                if (addEdit == 2)
                {
                    btn_editBook.Visibility = Visibility.Visible;
                    chk_newCustomer.IsEnabled = false;
                }
                else
                {
                    btn_editBook.Visibility = Visibility.Collapsed;
                    chk_newCustomer.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Function to calculate all rates of current booking
        /// </summary>
        /// <param name="_nbNights"></param>
        /// <param name="_nbDinners"></param>
        /// <param name="_nbBreakfasts"></param>
        /// <param name="_chkOptions"></param>
        /// <returns>decimal</returns>
        private decimal calculateRates(int _nbNights, int _nbDinners, int _nbBreakfasts, List<int> _chkOptions)
        {
            decimal total = 0;
            dinnersRate = 0;
            breakfastsRate = 0;
            nightsRate = 0;
            optionsRate = 0;

            foreach (var item in ml.getMealPrice())
            {
                dinnersRate = (_nbDinners * _nbNights) * item.dinnerR;
                breakfastsRate = (_nbBreakfasts * _nbNights) * item.breakfastR;
            }

            foreach (var item in rm.getRoomInfo(roomId))
            {
                if (_nbNights == 5)
                {
                    nightsRate = item.price5n;
                }
                else if (_nbNights == 10)
                {
                    nightsRate = item.price10n;
                }
                else
                {
                    nightsRate = _nbNights * item.price1n;
                }
            }

            foreach (var tags in _chkOptions)
            {
                foreach (var optItems in opt.getAlloptions())
                {
                    if (tags == optItems.id)
                    {
                        optionsRate += (decimal)optItems.rate;
                    }
                }
            }

            total = dinnersRate + breakfastsRate + nightsRate + optionsRate;
            if (pr.getPromoByRoom(roomId, checkin, checkout).Count() > 0)
            {
                int discount = 0;
                foreach (var item in pr.getPromoByRoom(roomId, checkin, checkout))
                {
                    discount = (int)item.discount;
                }
                total = total - ((total * discount) / 100);
                tbl_tmpDiscount.Text = "-" + discount.ToString();
                wp_tmpDiscount.Visibility = Visibility.Visible;
            }
            if (lv_customers.SelectedIndex >= 0 || nbResa > 1)
            {
                total = total - ((total * 5) / 100);
                wp_discount.Visibility = Visibility.Visible;
            }

            return total;
        }

        /// <summary>
        /// function to get all current booking informations
        /// </summary>
        private void currentBookInfo()
        {
            nbDinner = cb_dinner.SelectedIndex >= 0 ? int.Parse(((ComboBoxItem)cb_dinner.SelectedItem).Tag.ToString()) : 0;
            nbBreakfast = cb_breakfast.SelectedIndex >= 0 ? int.Parse(((ComboBoxItem)cb_breakfast.SelectedItem).Tag.ToString()) : 0;
            checkin = Convert.ToDateTime(tb_checkIn.Text);
            checkout = Convert.ToDateTime(tb_checkOut.Text);
            nbNights = (checkout.Date - checkin.Date).Days;

            //Get all checked item and store it in a list of options
            chkOptions = new List<int>();
            foreach (var item in optionChk)
            {
                if (item.IsChecked == true)
                {
                    chkOptions.Add(int.Parse(item.Tag.ToString()));
                }
            }
        }

        /// <summary>
        /// Function for control textbox and combobox and lisview in book form
        /// </summary>
        /// <returns>boolean</returns>
        private bool controlBookForm()
        {
            bool result = true;
            string message = null;

            if (chk_newCustomer.IsChecked == true)
            {
                if (string.IsNullOrEmpty(tb_firstname.Text) || string.IsNullOrEmpty(tb_lastname.Text) || string.IsNullOrEmpty(tb_email.Text) || string.IsNullOrEmpty(tb_phone.Text))
                {
                    result = false;
                    message += "- Veuillez d'abord renseigner les informations concernant le client !\r\n";
                }
            }
            else
            {
                if (lv_customers.SelectedIndex < 0)
                {
                    result = false;
                    message += "- Veuillez d'abord séléctionner un client dans la liste !\r\n";
                }
            }
            if (cb_payment.SelectedIndex < 0)
            {
                result = false;
                message += "- Veuillez d'abord séléctionner un mode de paiement !\r\n";
            }

            if (!result)
            {
                MessageBox.Show(message, "Réservation", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return result;
        }

        /// <summary>
        /// Empty all promotions items
        /// </summary>
        private void clearPromo()
        {
            btn_editPromo.IsEnabled = false;
            btn_addPromo.IsEnabled = true;
            lb_promotions.ItemsSource = null;
            cb_addRmType.SelectedIndex = -1;
            tb_addCode.Text = null;
            tb_addDiscount.Text = null;
            dp_addStartDate.Text = null;
            dp_addEndDate.Text = null;
        }

        /// <summary>
        /// check available rooms and sendMail to booking responsible if available room is equals 5
        /// </summary>
        private void checkAvailableRooms()
        {
            //Check availables rooms
            if (bk.nbBookedRmByDate(checkin))
            {
                mail.nbRoomAlert(checkin);
            }
        }

        private void clearCustomers()
        {
            btn_editCust.IsEnabled = false;
            btn_addCust.IsEnabled = true;
            lv_customers2.ItemsSource = null;
            tb_custName.Text = null;
            tb_lastname2.Text = null;
            tb_firstname2.Text = null;
            tb_email2.Text = null;
            tb_phone2.Text = null;
        }


        #endregion




    }
}
