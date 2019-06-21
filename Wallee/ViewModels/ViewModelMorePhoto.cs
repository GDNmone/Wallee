using Didaktika.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;
using Unsplasharp.Models;
using Wallee.Interfaces;
using Wallee.Models;
using Wallee.Utils;
using AsyncCommand = Didaktika.MVVM.AsyncCommand;

namespace Wallee.ViewModels
{
    public class ViewModelMorePhoto : ViewModelNavigation
    {
        private const int countColumns = 3;
        private int numPage = 1;
        private IServiceSetting serviceSetting;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceSetting">Настройки</param>
        /// <param name="textSearch">Текст поиска</param>
        /// <param name="searchAction">Делегат для поиска по tag</param>
        public ViewModelMorePhoto(IServiceSetting serviceSetting, string textSearch, Func<object, Task> searchAction)
        {
            this.serviceSetting = serviceSetting;

            CommandSelectImage = new CustomCommand(Executed_SelectImage);
            CommandNextImage = new CustomCommand(Executed_NextImage);
            CommandBackImage = new CustomCommand(Executed_BackImage);
            CommandNextPageImage = new CustomCommand(Executed_NextPageImage);
            CommandSearch = new AsyncCommand(searchAction);


            CommandManager.RegisterClassInputBinding(typeof(ViewModelMorePhoto),
                new InputBinding(CommandNextImage, new KeyGesture(Key.Right)));
            CommandManager.RegisterClassInputBinding(typeof(ViewModelMorePhoto),
                new InputBinding(CommandBackImage, new KeyGesture(Key.Left)));

            //  Task.Factory.StartNew(() => SearchByText(textSearch));
        }

        private void Executed_NextImage(object sender)
        {
            throw new NotImplementedException();
        }

        private void Executed_BackImage(object sender)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Комманда поиска по тегу
        /// </summary>
        public AsyncCommand CommandSearch { get; }


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


        #region Property ListTags(List<ModelTile>)

        public List<ModelTile> ListTags
        {
            get { return serviceSetting.ListTags; }
            set
            {
                serviceSetting.ListTags = value;
                OnPropertyChanged(nameof(ListTags));
            }
        }

        #endregion

        private async Task<IEnumerable<Photo>> GetNextPhotos()
        {
            numPage++;
            return await ServiceUnsplash.GetPhoto(numPage, lastQuery);
        }

        public void SearchByTag(ModelTile tag)
        {
        }

        private ModelTile RemoveTile = null;
        private string lastQuery = ";";

        public async Task<bool> SearchByText(string textSearch)
        {
            if (lastQuery == textSearch) return true;


            foreach (var listColumn in ListColumns)
            {
                listColumn.Clear();
            }


            if (RemoveTile != null)
            {
                ListTags.Add(RemoveTile);
                RemoveTile = null;
            }

            if (ListTags.Any(tile => tile.TextSearch.ToLower() == lastQuery.ToLower()))
            {
                var tileFind = ListTags.First(tile => tile.TextSearch.ToLower() == lastQuery.ToLower());
                RemoveTile = tileFind;
                ListTags.Remove(tileFind);
            }

            Console.WriteLine("Click");

            lastQuery = textSearch;
            if (await ServiceUnsplash.client.GetRandomPhoto() == null) return false;
            var columnsPhoto = await ServiceUnsplash.GetPhoto(numPage, lastQuery);
            //if (columnsPhoto.Count() == 0) return;
            var columns = await GetAddedInColumns(columnsPhoto);
            //if (columnsPhoto.Count() == 0) return false;
            for (int i = 0; i < countColumns; i++)
            {
                // if (ListColumns.Count-1 < i)
                //     ListColumns.Add(new WpfObservableRangeCollection<Photo>(columns[0]));
                // else
                ListColumns[i].AddRange(columns[i]);
            }

            //ListColumns[0].AddRange(columnsPhoto);
            //OnPropertyChanged(nameof(ListColumns));
            Console.WriteLine("completed");
            return true;
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
                    index = index + 1 >= countColumns ? 0 : index + 1;
                }

                return ListColumns;
            });
        }

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

        #region Property ListColumns(List<List<Photo>>)

        private readonly List<WpfObservableRangeCollection<Photo>> _listColumns =
            new List<WpfObservableRangeCollection<Photo>>()
            {
                new WpfObservableRangeCollection<Photo>(),
                new WpfObservableRangeCollection<Photo>(),
                new WpfObservableRangeCollection<Photo>(),
            };

        public List<WpfObservableRangeCollection<Photo>> ListColumns
        {
            get { return _listColumns; }
            //private  set
            //  {
            //     // _listColumns = value;
            //      OnPropertyChanged(nameof(ListColumns));
            //  }
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

        #endregion

        private void Executed_SelectImage(object sender)
        {
            SelectPhoto = (Photo) sender;
        }
    }
}