using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Room
    {
        #region properties
        private static RESHOTEL_DCDataContext dc;

        public int number { get; set; }
        public int idRoom { get; set; }
        public bool status { get; set; }
        public DateTime cleaned { get; set; }
        public string option { get; set; }
        public int capacity { get; set; }
        public int floor { get; set; }
        public string bedType { get; set; }
        public decimal price1n { get; set; }
        public decimal price5n { get; set; }
        public decimal price10n { get; set; }
        public string imageUrl { get; set; }
        public string promoCode { get; set; }
        public decimal discount { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        #endregion

        public Room()
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
        /// This function allow to get all free rooms at the hotel by room type
        /// </summary>
        /// <param name="feature"></param>
        /// <returns>ObservableCollection</returns>
        //public ObservableCollection<Room> getFreeRooms(int feature)
        //{
        //    ObservableCollection<Room> simpleRoom = new ObservableCollection<Room>();

        //    var query = (from tf in tbl_feature
        //                 join tr in tbl_room on tf.id equals tr.id_features
        //                 //join tp in tbl_promotion on tf.id_promotion equals tp.id
        //                 where tr.id_features == feature
        //                 select new { tf, tr }


                                //where (_checkIn < tb.checkIn && _checkIn > tb.checkOut) ||
                                //   (_checkOut < tb.checkIn && _checkOut > tb.checkOut) ||
                                //   (tb.checkIn < _checkIn && tb.checkIn > _checkOut) ||
                                //   (tb.checkOut < _checkIn && tb.checkOut > _checkOut)

                                //where _checkIn < tb.checkIn && _checkOut < tb.checkIn ||
                                //   _checkIn > tb.checkOut && _checkOut > tb.checkOut

                                //where tb.checkIn >= _checkIn && tb.checkIn <= _checkOut
                                //where tb.checkOut >= _checkIn && tb.checkOut <= _checkOut

                                //where (_checkIn > tb.checkIn && _checkIn < tb.checkOut) ||
                                //   (_checkOut > tb.checkIn && _checkOut < tb.checkOut) ||
                                //   (tb.checkIn > _checkIn && tb.checkIn < _checkOut) ||
                                //   (tb.checkOut > _checkIn && tb.checkOut < _checkOut)

        //    );

        //    foreach (var item in query)
        //    {
        //        if ((bool)item.tr.status)
        //        {
        //            simpleRoom.Add(new Room { 
        //                idRoom = (int)item.tr.id,
        //                number = (int)item.tr.number,
        //                capacity = (int)item.tf.capacity,
        //                //option = item.tf.options.ToString(),
        //                floor = (int)item.tf.floor,
        //                price1n = (decimal)item.tf.price1n,
        //                price5n = (decimal)item.tf.price5n,
        //                price10n = (decimal)item.tf.price10n,
        //                imageUrl = "/RESHOTEL_APP;component/Images/room.jpg"
        //            });
        //        }
        //    }

        //    return simpleRoom;
        //}

        /// <summary>
        /// This function allow to get all free rooms at the hotel by room type
        /// </summary>
        /// <param name="feature"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Room> getFreeRooms(int _feature, DateTime _checkIn, DateTime _checkOut)
        {
            ObservableCollection<Room> simpleRoom = new ObservableCollection<Room>();

            var query = (from tf in dc.features
                         join tr in dc.rooms on tf.id equals tr.id_features
                         where tr.id_features == _feature
                         join tb in dc.bookings on tr.id equals tb.id_room into joinedBR
                         from tbi in (from tb in joinedBR
                                      where (_checkIn >= tb.checkIn && _checkIn <= tb.checkOut) ||
                                         (_checkOut >= tb.checkIn && _checkOut <= tb.checkOut) ||
                                         (tb.checkIn >= _checkIn && tb.checkIn <= _checkOut) ||
                                         (tb.checkOut >= _checkIn && tb.checkOut <= _checkOut)
                                        select tb).DefaultIfEmpty()
                        where tbi.id_room == null
                        select new { tf, tr }
            ).Distinct();

            foreach (var item in query)
            {
                simpleRoom.Add(new Room
                {
                    idRoom = (int)item.tr.id,
                    number = (int)item.tr.number,
                    capacity = (int)item.tf.capacity,
                    //option = item.tf.options.ToString(),
                    floor = (int)item.tf.floor,
                    price1n = (decimal)item.tf.price1n,
                    price5n = (decimal)item.tf.price5n,
                    price10n = (decimal)item.tf.price10n,
                    imageUrl = "/RESHOTEL_APP;component/Images/room.jpg"
                });
            }

            return simpleRoom;
        }

        /// <summary>
        /// set room status (available or not)
        /// </summary>
        /// <param name="_idRoom"></param>
        /// <param name="_status"></param>
        public void setRoomStatus(int _idRoom)
        {
            var sql = from r in dc.rooms
                      where r.id == _idRoom
                      select r;

            foreach (room item in sql)
            {
                item.status = true;
                item.cleaned = DateTime.Now;
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
        /// Get room informations
        /// </summary>
        /// <param name="_roomId"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Room> getRoomInfo(int _roomId)
        {

            ObservableCollection<Room> roomInfo = new ObservableCollection<Room>();
            var query = (from tf in dc.features
                         join tr in dc.rooms on tf.id equals tr.id_features
                         //join tp in tbl_promotion on tf.id_promotion equals tp.id
                         where tr.id == _roomId
                         select new { tf, tr }
            );

            foreach (var item in query)
            {
                roomInfo.Add(new Room
                {
                    number = (int)item.tr.number,
                    floor = (int)item.tf.floor,
                    capacity = (int)item.tf.capacity,
                    price1n = (decimal)item.tf.price1n,
                    price5n = (decimal)item.tf.price5n,
                    price10n = (decimal)item.tf.price10n                    
                });
            }

            return roomInfo;
        }

        /// <summary>
        /// Remove item from a collection
        /// </summary>
        /// <param name="_oc"></param>
        /// <param name="_id"></param>
        public void removeItemOC(ObservableCollection<Room> _oc, int _id)
        {
            foreach (var item in _oc)
            {
                if (item.idRoom == _id)
                {
                    _oc.Remove(item);
                }
            }
        }

        /// <summary>
        /// This function show a collection of all room to clean
        /// </summary>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Room> roomToClean()
        {
            ObservableCollection<Room> toClean = new ObservableCollection<Room>();

            var query = from tb in dc.bookings
                        join tr in dc.rooms on tb.id_room equals tr.id
                        join tf in dc.features on tr.id_features equals tf.id
                        where DateTime.Now.Date >= tb.checkIn && DateTime.Now.Date <= tb.checkOut //Les chambres libéré le même jour ne sont plus affiché le lendemain
                        where ((TimeSpan)(DateTime.Now.Date - tr.cleaned)).TotalDays > 3
                        || tb.checkOut == DateTime.Now.Date
                        //where tr.status == false
                        select new { tr, tb, tf };

            foreach (var item in query)
            {
                toClean.Add(new Room
                {
                    idRoom = (int)item.tr.id,
                    number = (int)item.tr.number,
                    cleaned = (DateTime)item.tr.cleaned,
                    startDate = (DateTime)item.tb.checkIn,
                    endDate = (DateTime)item.tb.checkOut,
                    floor = (int)item.tf.floor
                });
            }

            return toClean;
        }

        /// <summary>
        /// This function show a collection of room to clean by feature
        /// </summary>
        /// <param name="_featId">int</param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Room> roomToCleanByFeat(int _featId)
        {
            ObservableCollection<Room> toClean = new ObservableCollection<Room>();

            var query = from tb in dc.bookings
                        join tr in dc.rooms on tb.id_room equals tr.id
                        join tf in dc.features on tr.id_features equals tf.id
                        where DateTime.Now.Date >= tb.checkIn && DateTime.Now.Date <= tb.checkOut //Les chambres libéré le même jour ne sont plus affiché le lendemain
                        where ((TimeSpan)(DateTime.Now.Date - tr.cleaned)).TotalDays > 3
                        //|| tb.checkOut == DateTime.Now.Date 
                        //where tr.status == false
                        where tf.id == _featId
                        select new { tr, tb, tf };

            foreach (var item in query)
            {
                toClean.Add(new Room
                {
                    idRoom = (int)item.tr.id,
                    number = (int)item.tr.number,
                    cleaned = (DateTime)item.tr.cleaned,
                    startDate = (DateTime)item.tb.checkIn,
                    endDate = (DateTime)item.tb.checkOut,
                    floor = (int)item.tf.floor
                });
            }

            return toClean;
        }

    }
}
