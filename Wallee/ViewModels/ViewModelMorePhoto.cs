using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Didaktika.MVVM;
using Unsplasharp.Models;
using Wallee.Models;
using Wallee.Utils;

namespace Wallee.ViewModels
{
    public class ViewModelMorePhoto : ViewModelNavigation
    {
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


        #region Property SelectPhoto(Photo)

        private Photo _selectPhoto;

        public Photo SelectPhoto
        {
            get { return _selectPhoto; }
            set
            {
                _selectPhoto = value;
                OnPropertyChanged(nameof(SelectPhoto));
            }
        }

        public ViewModelMorePhoto()
        {
            #region RegisterCommand

            CommandSelectImage = new CustomCommand(Executed_SelectImage);
            CommandNextImage = new CustomCommand(Executed_NextImage);
            CommandBackImage = new CustomCommand(Executed_BackImage);
            CommandNextPageImage = new CustomCommand(Executed_NextPageImage);
            CommandSearch = new CustomCommand(ButtonSearch_OnClick);

            CommandManager.RegisterClassInputBinding(typeof(ViewModelMorePhoto),
                new InputBinding(CommandNextImage, new KeyGesture(Key.Right)));
            CommandManager.RegisterClassInputBinding(typeof(ViewModelMorePhoto),
                new InputBinding(CommandBackImage, new KeyGesture(Key.Left)));

            //CommandNextImage.InputGestures.Add(new KeyGesture(Key.Right));
            //CommandBackImage.InputGestures.Add(new KeyGesture(Key.Left));

            #endregion
        }

        #endregion

        private int numPage = 1;
        private string lastQuery = ";";

        #region Property ListTags(ObservableCollection<ModelTile>)

        private ObservableCollection<ModelTile> _listTags;

        public ObservableCollection<ModelTile> ListTags
        {
            get { return _listTags; }
            set
            {
                _listTags = value;
                OnPropertyChanged(nameof(ListTags));
            }
        }

        #endregion

        #region Commands

        #region Properties

        /// <summary>
        /// Команда для смены выбранного изображения
        /// </summary>
        public CustomCommand CommandSelectImage { get; }

        /// <summary>
        /// Подгрузить следующую страницу изображений
        /// </summary>
        public CustomCommand CommandNextPageImage { get; }

        /// <summary>
        /// Перенестив выбор следующее изображение
        /// </summary>
        public CustomCommand CommandNextImage { get; }

        /// <summary>
        /// Перенестив выбор предыдущее изображение
        /// </summary>
        public CustomCommand CommandBackImage { get; }

        public CustomCommand CommandSearch { get; }

        #endregion


        #region Methods

        private bool lockNextPage = false;


        private async void Executed_NextPageImage(object sender)
        {
            if (!lockNextPage)
            {
                lockNextPage = true;
                var columnsPhoto = await GetNextPhotos();
               
                var columns = await GetAddedInColumns(columnsPhoto);
                for (int i = 0; i < countColumns; i++)
                {
                    ListColumns[i].AddRange(columns[i]);
                }
                lockNextPage = false;
            }
        }


        private void Executed_NextImage(object sender)
        {
            throw new NotImplementedException();
        }

        private void Executed_BackImage(object sender)
        {
            throw new NotImplementedException();
        }

        private void Executed_SelectImage(object sender)
        {
            SelectPhoto = (Photo) sender;
        }

        private ModelTile RemoveTile = null;

        private async void ButtonSearch_OnClick(object text)
        {
            TextSearch = (string) text;

            if (RemoveTile != null)
            {
                ListTags.Add(RemoveTile);
                RemoveTile = null;
            }

            if (ListTags.Any(tile => tile.TextSearch.ToLower() == TextSearch.ToLower()))
            {
                var tileFind = ListTags.First(tile => tile.TextSearch.ToLower() == TextSearch.ToLower());
                RemoveTile = tileFind;
                ListTags.Remove(tileFind);
            }

            Console.WriteLine("Click");

            if (lastQuery == TextSearch) return;
            lastQuery = TextSearch;
            numPage = 1;
            //  ServiceUnsplash.Reset();

            Console.WriteLine("do");
            var columnsPhoto = await ServiceUnsplash.GetPhoto(numPage, TextSearch);

            var columns =  await GetAddedInColumns(columnsPhoto);
            for (int i = 0; i < countColumns; i++)
            {
                ListColumns[i].AddRange(columns[i]);
            }
            //ListColumns[0].AddRange(columnsPhoto);
            //OnPropertyChanged(nameof(ListColumns));
            Console.WriteLine("completed");
        }

        #endregion

        #endregion


        #region Property ListColumns(List<List<Photo>>)

        private List<WpfObservableRangeCollection<Photo>> _listColumns = new List<WpfObservableRangeCollection<Photo>>()
        {
            new WpfObservableRangeCollection<Photo>(),
            new WpfObservableRangeCollection<Photo>(),
            new WpfObservableRangeCollection<Photo>(),
        };

        public List<WpfObservableRangeCollection<Photo>> ListColumns
        {
            get { return _listColumns; }
            set
            {
                _listColumns = value;
                OnPropertyChanged(nameof(ListColumns));
            }
        }

        #endregion


        private async Task<IEnumerable<Photo>> GetNextPhotos()
        {
            numPage++;
            return await ServiceUnsplash.GetPhoto(numPage, TextSearch);
        }


        private async Task<List<List<Photo>>> GetAddedInColumns(IEnumerable<Photo> photos)
        {
           return await Task<List<List<Photo>>>.Factory.StartNew(() =>
            {
                List<List<Photo>> ListColumns = new List<List<Photo>>();
                for (var i = 0; i < countColumns; i++)
                {
                    ListColumns.Add(new List<Photo>());
                }
                var index = 0;
                foreach (var photo in photos)
                {
                    ListColumns[index].Add(photo);
                    index = index+1 >= countColumns ? 0 : index + 1;
                }

                return ListColumns;
            });
        }

        private const int countColumns = 3;
    }
}