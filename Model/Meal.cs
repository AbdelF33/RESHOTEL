using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Meal
    {
        private static RESHOTEL_DCDataContext dc;
        public decimal breakfastR { get; set; }
        public decimal dinnerR { get; set; }

        public Meal()
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

        public List<Meal> getMealPrice()
        {
            List<Meal> mealPrice = new List<Meal>();

            var query = from tm in dc.meals
                        select tm;

            foreach (var item in query)
            {
                mealPrice.Add(new Meal
                {
                    breakfastR = (decimal)item.breakfastRate,
                    dinnerR = (decimal)item.dinnerRate
                });
            }

            return mealPrice;
        }
    }
}
