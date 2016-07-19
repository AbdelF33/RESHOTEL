using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Customer
    {
        private static RESHOTEL_DCDataContext dc;
        public static int lastInsertId { get; set; }

        public Customer()
        {
            try
            {
                dc = new RESHOTEL_DCDataContext(Properties.Resources.dataSource);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// This void allow us to add a new customer
        /// </summary>
        /// <param name="_firstN"></param>
        /// <param name="_lastN"></param>
        /// <param name="_mail"></param>
        /// <param name="_phone"></param>
        /// <param name="_nbResa"></param>
        public void addCustomer(string _firstN, string _lastN, string _mail, string _phone, int _nbResa)
        {
            customer ctm = new customer()
            {
                firstname = _firstN,
                lastname = _lastN,
                email = _mail,
                phone = _phone,
                nbReservation = _nbResa
            };

            dc.customers.InsertOnSubmit(ctm);
            try
            {
                dc.SubmitChanges();
                lastInsertId = ctm.id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Get customer by firstname or lastname
        /// </summary>
        /// <param name="_name"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<customer> getCustByName(string _name)
        {
            ObservableCollection<customer> customers = new ObservableCollection<customer>();

            var query = (from  tc in dc.customers 
                         where tc.firstname.Contains(_name) || tc.lastname.Contains(_name)
                         select tc 
            );

            foreach (var item in query)
            {
                customers.Add(new customer
                {
                    id = (int)item.id,
                    firstname = (string)item.firstname,
                    lastname = (string)item.lastname,
                    nbReservation = (int)item.nbReservation,
                    email = (string)item.email,
                    phone = (string)item.phone
                });
            }

            return customers;
        }

        /// <summary>
        /// this function allow to change customer booking count
        /// </summary>
        /// <param name="_idCustomer"></param>
        /// <param name="_nbReservation"></param>
        public void updateNbResa(int _idCustomer, int _nbReservation)
        {
            var query = from tc in dc.customers
                         where tc.id == _idCustomer
                         select tc;

            foreach (customer item in query)
            {
                item.nbReservation = _nbReservation;
            }

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// This function allow to update a customer by Id
        /// </summary>
        /// <param name="_idCustomer"></param>
        /// <param name="_firstN"></param>
        /// <param name="_lastN"></param>
        /// <param name="_mail"></param>
        /// <param name="_phone"></param>
        public void updateCustomer(int _idCustomer, string _firstN, string _lastN, string _mail, string _phone)
        {
            var query = from tc in dc.customers
                        where tc.id == _idCustomer
                        select tc;


            foreach (customer item in query)
            {
                item.firstname = _firstN;
                item.lastname = _lastN;
                item.email = _mail;
                item.phone = _phone;
            }

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw;
            }
            finally
            {
                dc.Connection.Close();
            }
        }

    }
}
