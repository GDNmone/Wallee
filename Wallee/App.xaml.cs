using System.Windows;
using Unity;
using Wallee.CustomControls;
using Wallee.Interfaces;
using Wallee.Services;
using Wallee.ViewModels;

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

            IUnityContainer container = new UnityContainer();

            container.RegisterType<IServiceSetting, ServiceSettingStatic>();


            var mainWindow = new WindowMain()
                {DataContext = new ViewModelContainerSearch(container.Resolve<IServiceSetting>()) { }};

            mainWindow.ShowDialog();
        }
    }
}