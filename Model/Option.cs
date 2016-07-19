using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Option
    {
        private static RESHOTEL_DCDataContext dc;
        private Table<option> tbl_option;
        public bool isSelected { get; set; }

        public Option()
        {
            try
            {
                dc = new RESHOTEL_DCDataContext(Properties.Resources.dataSource);
                tbl_option = dc.GetTable<option>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw;
            }
        }

        /// <summary>
        /// function allow to get all options from table option in database
        /// </summary>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<option> getAlloptions()
        {
            ObservableCollection<option> allOptions = new ObservableCollection<option>();

            var query = from to in tbl_option
                        select to;

            foreach (var item in query)
            {
                allOptions.Add(new option
                {
                    id = (int)item.id,
                    label = item.label.ToString(),
                    rate = (decimal)item.rate
                });
            }

            return allOptions;
        }
    }
}
