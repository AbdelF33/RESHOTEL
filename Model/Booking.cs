using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Booking
    {
        #region properties
        private static RESHOTEL_DCDataContext dc;

        public int bookId { get; set; }
        public DateTime dateBooked { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public bool paid { get; set; }
        public int nbResa { get; set; }
        public int nbBreakfast { get; set; }
        public int nbDinner { get; set; }
        public int customerId { get; set; }
        public int roomId { get; set; }
        public int paymentId { get; set; }
        public static int BookingLII { get; set; }
        public int roomNb { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string mail { get; set; }
        public int bookOptId { get; set; }
        public decimal price { get; set; }
        public string staffFirst { get; set; }
        public string staffLast { get; set; }
        public string features { get; set; }
        public string paymentType { get; set; }
        public string optionLabel { get; set; }
        public decimal optionRate { get; set; }
        #endregion

        public Booking()
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
        /// function allow to add booking
        /// </summary>
        /// <param name="_dateBook"></param>
        /// <param name="_checkin"></param>
        /// <param name="_checkout"></param>
        /// <param name="_paid"></param>
        /// <param name="_nbPeople"></param>
        /// <param name="_nbBreakfast"></param>
        /// <param name="_nbDinner"></param>
        /// <param name="_customerId"></param>
        /// <param name="_staffId"></param>
        /// <param name="_roomId"></param>
        /// <param name="_paymentId"></param>
        public void addBooking(DateTime _dateBook, DateTime _checkin, DateTime _checkout,bool _paid,int _nbBreakfast,int _nbDinner,int _customerId,int _staffId,int _roomId,int _paymentId, decimal _price)
        {

            booking bk = new booking()
            {
                dateBooked = _dateBook,
                checkIn = _checkin,
                checkOut = _checkout,
                paid = _paid,
                nbBreakfast = _nbBreakfast,
                nbDinner = _nbDinner,
                id_customer = _customerId,
                id_staff = _staffId,
                id_room = _roomId,
                id_payment = _paymentId,
                price = _price
            };

            dc.bookings.InsertOnSubmit(bk);
            try
            {
                dc.SubmitChanges();
                BookingLII = bk.id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                throw;
            }

        }

        /// <summary>
        /// this void allow to set options of booking
        /// </summary>
        /// <param name="_idOpt"></param>
        public void addBookOption(int _idOpt, int _idBook)
        {
            if (_idBook != 0)
            {
                BookingLII = _idBook;
            }

            bookingOption bkO = new bookingOption()
            {
                id_booking = BookingLII,
                id_options = _idOpt
            };

            dc.bookingOptions.InsertOnSubmit(bkO);
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
        /// This function allow to get all booked rooms at the hotel
        /// </summary>
        /// <param name="_bookId"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Booking> getBookRooms(int _feature)
        {
            ObservableCollection<Booking> bookedRoom = new ObservableCollection<Booking>();

            var query = (from tb in dc.bookings
                         join tc in dc.customers on tb.id_customer equals tc.id
                         join tr in dc.rooms on tb.id_room equals tr.id
                         join tf in dc.features on tr.id_features equals tf.id
                         where tr.id_features == _feature
                         select new { tb, tc, tr }
            );

            foreach (var item in query)
            {
                bookedRoom.Add(new Booking
                {
                    bookId = (int)item.tb.id,
                    checkIn = Convert.ToDateTime(item.tb.checkIn),
                    checkOut = Convert.ToDateTime(item.tb.checkOut),
                    customerId = (int)item.tc.id,
                    firstname = (string)item.tc.firstname,
                    lastname = (string)item.tc.lastname,
                    roomNb = (int)item.tr.number,
                    nbResa = (int)item.tc.nbReservation,
                    nbBreakfast = (int)item.tb.nbBreakfast,
                    nbDinner = (int)item.tb.nbDinner,
                    price = (decimal)item.tb.price
                });
            }

            return bookedRoom;
        }

        /// <summary>
        /// get booking rooms by date
        /// </summary>
        /// <param name="_feature"></param>
        /// <param name="_begin"></param>
        /// <param name="_end"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Booking> getBRBydate(int _feature, DateTime _begin, DateTime _end)
        {
            ObservableCollection<Booking> bookedRoom = new ObservableCollection<Booking>();

            var query = (from tb in dc.bookings
                         join tc in dc.customers on tb.id_customer equals tc.id
                         join tr in dc.rooms on tb.id_room equals tr.id
                         join tf in dc.features on tr.id_features equals tf.id
                         where tr.id_features == _feature
                         where tb.checkIn >= _begin && tb.checkIn <= _end
                         where tb.checkOut >= _begin && tb.checkOut <= _end
                         select new { tb, tc, tr }
            );

            foreach (var item in query)
            {
                bookedRoom.Add(new Booking
                {
                    bookId = (int)item.tb.id,
                    dateBooked = Convert.ToDateTime(item.tb.dateBooked),
                    checkIn = Convert.ToDateTime(item.tb.checkIn),
                    checkOut = Convert.ToDateTime(item.tb.checkOut),
                    customerId = (int)item.tc.id,
                    firstname = (string)item.tc.firstname,
                    lastname = (string)item.tc.lastname,
                    roomNb = (int)item.tr.number,
                    nbResa = (int)item.tc.nbReservation,
                    nbBreakfast = (int)item.tb.nbBreakfast,
                    nbDinner = (int)item.tb.nbDinner,
                    price = (decimal)item.tb.price
                });
            }

            return bookedRoom;
        }

        /// <summary>
        /// This function allow to get informations of the selected booked room 
        /// </summary>
        /// <param name="_bookId"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Booking> getBookInfo(int _bookId)
        {
            ObservableCollection<Booking> bookedInfo = new ObservableCollection<Booking>();

            var query = (from tb in dc.bookings
                         join tc in dc.customers on tb.id_customer equals tc.id
                         join ts in dc.staffs on tb.id_staff equals ts.id
                         join tr in dc.rooms on tb.id_room equals tr.id
                         join tf in dc.features on tr.id_features equals tf.id
                         join tp in dc.payments on tb.id_payment equals tp.id
                         where tb.id == _bookId
                         select new { tb, tc, tr, ts, tf,tp}
            );

            foreach (var item in query)
            {
                bookedInfo.Add(new Booking
                {
                    bookId = (int)item.tb.id,
                    dateBooked = Convert.ToDateTime(item.tb.dateBooked).Date,
                    checkIn = Convert.ToDateTime(item.tb.checkIn).Date,
                    checkOut = Convert.ToDateTime(item.tb.checkOut).Date,
                    customerId = (int)item.tc.id,
                    firstname = (string)item.tc.firstname,
                    lastname = (string)item.tc.lastname,
                    mail = (string)item.tc.email,
                    phone = (string)item.tc.phone,
                    roomId = (int)item.tr.id,
                    nbBreakfast = (int)item.tb.nbBreakfast,
                    nbDinner = (int)item.tb.nbDinner,
                    paymentId = (int)item.tp.id,
                    price = (decimal)item.tb.price,
                    staffFirst = (string)item.ts.firstname,
                    staffLast = (string)item.ts.lastname,
                    features = (string)item.tf.options,
                    paymentType = (string)item.tp.label,
                    nbResa = (int)item.tc.nbReservation
                });
            }

            return bookedInfo;
        }

        /// <summary>
        /// Get by specific booking all booking options
        /// </summary>
        /// <param name="_bookId"></param>
        /// <returns>bookOptionInfo</returns>
        public ObservableCollection<Booking> getBookOptionInfo(int _bookId)
        {
            ObservableCollection<Booking> bookOptionInfo = new ObservableCollection<Booking>();

            var query = (from tb in dc.bookings
                         join tbo in dc.bookingOptions on tb.id equals tbo.id_booking
                         join to in dc.options on tbo.id_options equals to.id
                         where tb.id == _bookId
                         select new { tb, tbo, to}
            );

            foreach (var item in query)
            {
                bookOptionInfo.Add(new Booking
                {
                    bookOptId = (int)item.tbo.id_options,
                    optionLabel = (string)item.to.label,
                    optionRate = (decimal)item.to.rate
                });
            }

            return bookOptionInfo;
        }

        /// <summary>
        /// Get booked rooms by client filter
        /// </summary>
        /// <param name="_name"></param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<Booking> getBRByclient(string _name)
        {
            ObservableCollection<Booking> bookedRoom = new ObservableCollection<Booking>();

            var query = (from tb in dc.bookings
                         join tc in dc.customers on tb.id_customer equals tc.id
                         join tr in dc.rooms on tb.id_room equals tr.id
                         where tc.firstname.Contains(_name) || tc.lastname.Contains(_name)
                         select new { tb, tc, tr }
            );

            foreach (var item in query)
            {
                bookedRoom.Add(new Booking
                {
                    bookId = (int)item.tb.id,
                    dateBooked = Convert.ToDateTime(item.tb.dateBooked),
                    checkIn = Convert.ToDateTime(item.tb.checkIn),
                    checkOut = Convert.ToDateTime(item.tb.checkOut),
                    customerId = (int)item.tc.id,
                    firstname = (string)item.tc.firstname,
                    lastname = (string)item.tc.lastname,
                    roomNb = (int)item.tr.number,
                    nbResa = (int)item.tc.nbReservation,
                    nbBreakfast = (int)item.tb.nbBreakfast,
                    nbDinner = (int)item.tb.nbDinner,
                    price = (decimal)item.tb.price
                });
            }

            return bookedRoom;
        }

        /// <summary>
        /// This function allow us to edit an existing booking
        /// </summary>
        /// <param name="_idBook"></param>
        public void setBooking(int _idBook, DateTime _checkin, DateTime _checkout, int _nbBreakfast, int _nbDinner, int _idRoom, decimal _price)
        {
            var sql = from bk in dc.bookings
                      where bk.id == _idBook
                      select bk;

            foreach (booking item in sql)
            {
                item.checkIn = _checkin;
                item.checkOut = _checkout;
                item.nbBreakfast = _nbBreakfast;
                item.nbDinner = _nbDinner;
                item.id_room = _idRoom;
                item.price = _price;
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

        /// <summary>
        /// Function to delete all option of a specific booking by Id
        /// </summary>
        /// <param name="_idBook"></param>
        public void deleteBookOpt(int _idBook)
        {
            var query = from bko in dc.bookingOptions
                        where bko.id_booking == _idBook
                        select bko;

            foreach (var item in query)
            {
                dc.bookingOptions.DeleteOnSubmit(item);
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

        /// <summary>
        /// this function allow to delete a booking
        /// </summary>
        /// <param name="_idBook"></param>
        public void cancelBooking(int _idBook)
        {
            var query = from bk in dc.bookings
                        where bk.id == _idBook
                        select bk;

            foreach (var item in query)
            {
                dc.bookings.DeleteOnSubmit(item);
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

        /// <summary>
        /// Compare rooms number with booked rooms number in a specific date
        /// </summary>
        /// <param name="_checkIn"></param>
        /// <returns>Boolean</returns>
        public bool nbBookedRmByDate(DateTime _checkIn)
        {
            bool result = false;

            var nbBookedRooms = (from tb in dc.bookings
                         join tr in dc.rooms on tb.id_room equals tr.id
                         where tb.checkIn == _checkIn
                         select tr).Count();

            var nbRooms = dc.rooms.Count();

            if (nbRooms - nbBookedRooms == 5)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Get dinners number by date and booking
        /// </summary>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<booking> getNbDinners()
        {
            ObservableCollection<booking> nbDinners = new ObservableCollection<booking>();

            var query = (from tb in dc.bookings
                         where DateTime.Now.Date >= tb.checkIn && DateTime.Now.Date <= tb.checkOut
                         || DateTime.Now.AddDays(7).Date >= tb.checkIn && DateTime.Now.AddDays(7).Date <= tb.checkOut
                         select  tb);

            foreach (var item in query)
            {
                nbDinners.Add(new booking
                {
                    checkIn = item.checkIn,
                    checkOut = item.checkOut,
                    nbDinner = item.nbDinner
                });
            }

            return nbDinners;
        }

        /// <summary>
        /// Function to update dinners number for a booking
        /// </summary>
        /// <param name="_bookId">int</param>
        /// <param name="_nbDinner">int</param>
        public void updateNbDinner(int _bookId, int _nbDinner, decimal _price)
        {
            var sql = from bk in dc.bookings
                      where bk.id == _bookId
                      select bk;

            foreach (booking item in sql)
            {
                item.nbDinner = _nbDinner;
                item.price = _price;
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
