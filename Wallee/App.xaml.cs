using System.Windows;
using Wallee.CustomControls;

namespace Wallee
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new WindowMain();
            mainWindow.ShowDialog();
        }
    }
}