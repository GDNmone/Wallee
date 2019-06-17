using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Didaktika.MVVM;
using StyleFluentWpf.CustomControls.ControlNavigation;
using Unsplasharp.Models;
using Wallee.Models;
using Wallee.Utils;

namespace Wallee.ViewModels
{
    public class ViewModelContainerSearch : ViewModel
    {
        public ServiceNavigation ServiceNavigationSpaceImages { get; set; } = new ServiceNavigation();
        public CustomCommand CommandSearch { get; }


        public ViewModelContainerSearch()
        {
            CommandNewSearch = new CustomCommand(Action_CommandNewSearch);
            ;

            #region RegisterCommand

            CommandSearch = new CustomCommand(ButtonSearch_OnClick);

            //CommandNextImage.InputGestures.Add(new KeyGesture(Key.Right));
            //CommandBackImage.InputGestures.Add(new KeyGesture(Key.Left));

            #endregion
        }

        private void Action_CommandNewSearch(object obj)
        {
           // if (CurrentViewModel.GetType() != typeof(ViewModelMorePhoto))
           //     ServiceNavigationSpaceImages.OpenViewModel(new ViewModelMorePhoto(((ModelTile)obj).TextSearch, new ViewModelMorePhoto(TextSearch,
           //         CurrentViewModel is ViewModelCategories t ? t.ListTiles : new List<ModelTile>()))
           //     { ListTags = ((ViewModelCategories) StartViewModel).ListTiles});
        }


        public CustomCommand CommandNewSearch { get; set; }


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

        public ViewModelNavigation StartViewModel { get; } = new ViewModelCategories();

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


        private async void ButtonSearch_OnClick(object text)
        {
            TextSearch = (string) text;

            if (ServiceNavigationSpaceImages.CurrentViewModel is ViewModelMorePhoto moorePhoto)
            {
                await moorePhoto.SearchByText(TextSearch);


                //numPage = 1;
                //  ServiceUnsplash.Reset();

                Console.WriteLine("do");
            }
            else
                ServiceNavigationSpaceImages.OpenViewModel(new ViewModelMorePhoto(TextSearch,
                    CurrentViewModel is ViewModelCategories t ? t.ListTiles : new List<ModelTile>()));
        }

        #region Commands

        #region Properties

        #endregion


        #region Methods

        #endregion

        #endregion
    }
}