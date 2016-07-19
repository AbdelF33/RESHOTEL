using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class ErrorLog
    {
        private static RESHOTEL_DCDataContext dc = new RESHOTEL_DCDataContext(Properties.Resources.dataSource);

        public void insertError(string _message, string _details)
        {
            log lg = new log()
            {
                datetime = DateTime.Now,
                username = Login.firstname + " " + Login.lastname,
                errorMessage = _message,
                errorDetails = _details
            };

            dc.logs.InsertOnSubmit(lg);
            dc.SubmitChanges();
        }
    }
}
