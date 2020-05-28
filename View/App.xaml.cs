using Autofac;
using System.Windows;
using View.Startup;
using ViewModel;

namespace View
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var cointainter = bootstrapper.Bootstrap();
            var mainWindow = cointainter.Resolve<MainWindow>();     
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, 
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

        }
    }
}
