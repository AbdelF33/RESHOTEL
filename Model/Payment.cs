using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Payment
    {
        private static RESHOTEL_DCDataContext dc;

        public Payment()
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
        /// Function allow to get all payment method from table payment in database
        /// </summary>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<payment> getAllmethod()
        {
            ObservableCollection<payment> allMethod = new ObservableCollection<payment>();

            var query = from tpy in dc.payments.AsParallel()
                        select tpy;

            foreach (var item in query)
            {
                allMethod.Add(new payment
                {
                    id = (int)item.id,
                    label = item.label.ToString()
                });
            }

            return allMethod;
        }
    }
}
