using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Model
{
    class Login
    {
        
        #region variables 
        private static RESHOTEL_DCDataContext dc;
        public static string profile { get; set; }
        public static string firstname { get; set; }
        public static string lastname { get; set; }
        public static int id_staff { get; set; }
        #endregion

        public Login(){
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
        /// fonction qui permet de vérifier le login et mot de passe de l'utilisateur en base de données
        /// </summary>
        /// <param name="log"></param>
        /// <param name="pass"></param>
        /// <returns>Boolean</returns>
        public bool UserAuthentification(string log, string pass)
        {
            var query = (from ts in dc.staffs
                        join tsp in dc.staffProfiles on ts.id equals tsp.id_staff
                        join tp in dc.profiles on tsp.id_profile equals tp.id
                        where ts.username.Equals(log)
                        where ts.password.Equals(pass)
                        select new
                        {
                            firstname = ts.firstname,
                            lastname = ts.lastname,
                            profile = tp.label,
                            id_staff = ts.id
                        }
                      );

            foreach (var item in query)
            {
                profile = item.profile;
                firstname = item.firstname;
                lastname = item.lastname;
                id_staff = item.id_staff;
            }

            if (query.Count() != 0)
            {
                return true;
            }

            return false;
        }

    }

}
