using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RESHOTEL_APP
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private Model.ErrorLog el = new Model.ErrorLog();
        private Tools.Mail mail = new Tools.Mail();

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);

            el.insertError(e.Exception.Message, e.Exception.StackTrace);
            mail.errorMail(e.Exception.Message, e.Exception.StackTrace);

            MessageBox.Show("Veuillez nous excuser pour ce désagrément, une message a été envoyé a notre équipe technique afin de traiter ce problème dans les plus brefs délais", "Une erreur est survenue !", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException +=
             new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        /// <summary>
        /// function to manage unhandled exception 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            //MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);

            el.insertError(ex.Message, ex.StackTrace);
            mail.errorMail(ex.Message, ex.StackTrace);
            MessageBox.Show("Veuillez nous excuser pour ce désagrément, une message a été envoyé a notre équipe technique afin de traiter ce problème dans les plus brefs délais", "Une erreur est survenue !", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        

    }
}
