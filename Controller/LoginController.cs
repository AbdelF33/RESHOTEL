using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RESHOTEL_APP.Controller
{
    class LoginController
    {
        private Model.Login mLogin = new Model.Login();
        private View.login vLogin = new View.login();
        private View.hbWindow hbw = new View.hbWindow();
        private View.mealPlanning mp = new View.mealPlanning();
        private View.cleanPlanning cp = new View.cleanPlanning();
        public string message { get; set; }

        /// <summary>
        /// Fonction qui permet de vérifier le login et mot de passe de l'utilisateur via la fenêtre de l'authentification
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Boolean</returns>
        public bool checkAuth(string login, string password)
        {
            if (mLogin.UserAuthentification(login, password) == true)
            {
                switch (Model.Login.profile)
                {
                    case "Administrateur":
                        message = "admin";
                        break;
                    case "Responsable réservation":
                        hbw.Show();
                        break;
                    case "Standardiste":
                        hbw.Show();
                        break;
                    case "Hôte d'accueil":
                        hbw.Show();
                        break;
                    case "Responsable facturation":
                        hbw.Show();
                        break;
                    case "Responsable restauration":
                        mp.Show();
                        break;
                    case "Responsable hygiène":
                        cp.Show();
                        break;
                    default:
                        message = "Erreur d'authentification";
                        break;
                }
                return true;
            }
            else
            {
                message = "Cette utilisateur n'existe pas !\nVeuillez vérifier votre login et mot de passe.";
                return false;
            }
        }

        /// <summary>
        /// function to logOut from user session
        /// </summary>
        /// <param name="_window">Window</param>
        public static void logOut(Window _window)
        {
            View.login lgv = new View.login();
            _window.Close();
            lgv.Show();
        }


    }
}
