using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Promotion
    {
        private static RESHOTEL_DCDataContext dc;

        public Promotion()
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

        public List<promotion> getPromoByRoom(int _roomId, DateTime _checkIn, DateTime _checkOut)
        {
            List<promotion> listOfPromo = new List<promotion>();

            var query = from tf in dc.features
                       join tp in dc.promotions on tf.id equals tp.id_features
                       join tr in dc.rooms on tf.id equals tr.id_features
                       where tr.id == _roomId
                       where tp.startDate <= _checkIn && tp.endDate >= _checkOut
                       select tp;

            foreach (var item in query)
            {
                listOfPromo.Add(new promotion
                {
                    code = item.code,
                    discount = item.discount,
                    startDate = item.startDate,
                    endDate = item.endDate,
                    id_features = item.id_features
                });
            }

            return listOfPromo;
        }

        public ObservableCollection<promotion> getPromoByDate(int _featId, DateTime _checkIn, DateTime _checkOut)
        {
            ObservableCollection<promotion> listOfPromo = new ObservableCollection<promotion>();

            var query = from tp in dc.promotions
                        where tp.id_features == _featId
                        where tp.startDate <= _checkIn && tp.endDate >= _checkOut
                        select tp;

            foreach (var item in query)
            {
                listOfPromo.Add(new promotion
                {
                    id = item.id,
                    code = item.code,
                    discount = item.discount, 
                    startDate = item.startDate,
                    endDate = item.endDate,
                    id_features = item.id_features
                });
            }

            return listOfPromo;
        }

        public ObservableCollection<promotion> getPromoByfeature(int _featId)
        {
            ObservableCollection<promotion> listOfPromo = new ObservableCollection<promotion>();

            var query = from tp in dc.promotions
                        where tp.id_features == _featId
                        select tp;

            foreach (var item in query)
            {
                listOfPromo.Add(new promotion
                {
                    id = item.id,
                    code = item.code,
                    discount = item.discount,
                    startDate = item.startDate,
                    endDate = item.endDate,
                    id_features = item.id_features
                });
            }

            return listOfPromo;
        }

        public ObservableCollection<promotion> getAllPromo()
        {
            ObservableCollection<promotion> listOfPromo = new ObservableCollection<promotion>();

            var query = from tp in dc.promotions
                        select tp;

            foreach (var item in query)
            {
                listOfPromo.Add(new promotion
                {
                    id = item.id,
                    code = item.code,
                    discount = item.discount,
                    startDate = item.startDate,
                    endDate = item.endDate,
                    id_features = item.id_features
                });
            }

            return listOfPromo;
        }

        public void addPromo(DateTime _startDate, DateTime _endDate, string _code, int _discount, int _featId)
        {
            promotion pr = new promotion()
            {
                startDate = _startDate,
                endDate = _endDate,
                code = _code,
                discount = _discount,
                id_features = _featId
            };

            dc.promotions.InsertOnSubmit(pr);
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

        public void updatePromo(int _promoId, DateTime _startDate, DateTime _endDate, string _code, int _discount, int _featId)
        {
            var promo = (from tp in dc.promotions
                      where tp.id == _promoId
                      select tp).Single();


            promo.startDate = _startDate;
            promo.endDate = _endDate;
            promo.code = _code;
            promo.discount = _discount;
            promo.id_features = _featId;

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

        public void deletePromo(int _promoId)
        {
            var sql = from tp in dc.promotions
                      where tp.id == _promoId
                      select tp;

            foreach (var item in sql)
            {
                dc.promotions.DeleteOnSubmit(item);
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
