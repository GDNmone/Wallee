using Didaktika.MVVM;
using StyleFluentWpf.CustomControls.ControlNavigation;
using System;
using System.Threading.Tasks;
using Wallee.Interfaces;

namespace Wallee.ViewModels
{
    public class ViewModelContainerSearch : ViewModel
    {
        public ServiceNavigation ServiceNavigationSpaceImages { get; set; } = new ServiceNavigation();
        public AsyncCommand CommandSearch { get; }
        private IServiceSetting serviceSetting { get; }

        public ViewModelNavigation StartViewModel { get; }

        public ViewModelContainerSearch(IServiceSetting serviceSetting)
        {
            this.serviceSetting = serviceSetting;

            StartViewModel = new ViewModelCategories(serviceSetting, ExecuteCommandSendTag);

            #region RegisterCommand

            CommandSearch = new AsyncCommand(ButtonSearch_OnClick);

            //CommandNextImage.InputGestures.Add(new KeyGesture(Key.Right));
            //CommandBackImage.InputGestures.Add(new KeyGesture(Key.Left));

            #endregion
        }

        private async void ExecuteCommandSendTag(object obj)
        {
            await ButtonSearch_OnClick(obj);
        }

        #region Property CurrentViewModel(ViewModel)

        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        #endregion


        #region Property TextSearch(string)

        private string _textSearch;

        public string TextSearch
        {
            get { return _textSearch; }
            set
            {
                _textSearch = value;
                OnPropertyChanged(nameof(TextSearch));
            }
        }

        #endregion


        private async Task ButtonSearch_OnClick(object text)
        {
            TextSearch = (string) text;

            if (ServiceNavigationSpaceImages.CurrentViewModel is ViewModelMorePhoto moorePhoto)
            {
                var d = (ViewModelMorePhoto)moorePhoto.Clone();
                var isConnectInternet = await moorePhoto.SearchByText(TextSearch);
                if (!isConnectInternet)
                    ServiceNavigationSpaceImages.OpenViewModel(new ViewModelLostConnection());
                else
                {
                    if (d.LastQuery.Equals(text))
                        ServiceNavigationSpaceImages.OpenViewModel(d);
                    else
                        d.ScrollUp();
                }

                Console.WriteLine("do");
            }
            else
            {
                var s = new ViewModelMorePhoto(serviceSetting, ButtonSearch_OnClick, ServiceNavigationSpaceImages);
                //
                var isConnectInternet = await s.SearchByText(TextSearch);

                if (ServiceNavigationSpaceImages.CurrentViewModel is ViewModelLostConnection)
                {
                    ServiceNavigationSpaceImages.BackViewModel();
                    ServiceNavigationSpaceImages.CleareNextList();
                    ServiceNavigationSpaceImages.BackViewModel();
                    ServiceNavigationSpaceImages.CleareNextList();
                }

                if (isConnectInternet)
                {
                    ServiceNavigationSpaceImages.OpenViewModel(s);
                }
                else
                    ServiceNavigationSpaceImages.OpenViewModel(new ViewModelLostConnection());
            }
        }

        #region Commands

        #region Properties

        #endregion


        #region Methods

        #endregion

        #endregion
    }
}