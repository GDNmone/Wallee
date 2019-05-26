using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unsplasharp.Models;
using Wallee.Utils;

namespace Wallee.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMorePhoto.xaml
    /// </summary>
    public partial class PageMorePhoto : UserControl, INotifyPropertyChanged
    {
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

        /// <summary>
        /// Команда для смены выбранного изображения
        /// </summary>
        public static RoutedUICommand CommandSelectImage { get; } = new RoutedUICommand();

        /// <summary>
        /// Подгрузить следующую страницу изображений
        /// </summary>
        public static RoutedUICommand CommandNextPageImage { get; } = new RoutedUICommand();

        /// <summary>
        /// Перенестив выбор следующее изображение
        /// </summary>
        public static RoutedUICommand CommandNextImage { get; } = new RoutedUICommand();

        /// <summary>
        /// Перенестив выбор предыдущее изображение
        /// </summary>
        public static RoutedUICommand CommandBackImage { get; } = new RoutedUICommand();

        public AsyncCommand SearchCommand { get; }

        public PageMorePhoto()
        {
            //SearchCommand = new AsyncCommand(ButtonBase_OnClickUpdata);
            CommandBindings.Add(new CommandBinding(CommandSelectImage, Executed_SelectImage));
            CommandBindings.Add(new CommandBinding(CommandNextImage, Executed_NextImage));
            CommandBindings.Add(new CommandBinding(CommandBackImage, Executed_BackImage));
            CommandBindings.Add(new CommandBinding(CommandNextPageImage, Executed_NextPageImage));

            CommandNextImage.InputGestures.Add(new KeyGesture(Key.Right));
            CommandBackImage.InputGestures.Add(new KeyGesture(Key.Left));

            InitializeComponent();
        }

        private bool lockNextPage = false;

        private async void Executed_NextPageImage(object sender, ExecutedRoutedEventArgs e)
        {
            if (!lockNextPage)
            {
                lockNextPage = true;
                await Task.Run(() => ButtonBase_OnClick(sender, e));
                lockNextPage = false;
            }
        }

        private void Executed_NextImage(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Executed_BackImage(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Executed_SelectImage(object sender, ExecutedRoutedEventArgs e)
        {
            SelectPhoto = (Photo) e.Parameter;
        }

        #region Property TextSerch(string)

        private string _TextSerch;

        public string TextSerch
        {
            get { return _TextSerch; }
            set
            {
                _TextSerch = value;
                OnPropertyChanged(nameof(TextSerch));
            }
        }

        #endregion

        private int countPage = 1;
        private string lastQuery = ";";

        private async void ButtonSearch_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Clock");

            if (lastQuery == TextSerch) return;
            lastQuery = TextSerch;
            countPage = 1;
            ServiceUnsplash.Reset();

            Console.WriteLine("do");
            await ServiceUnsplash.GetPhoto(countPage, TextSerch, this.Dispatcher);

            Console.WriteLine("completed");
        }

        #region System

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            countPage++;
            await ServiceUnsplash.GetPhoto(countPage, TextSerch, this.Dispatcher);
        }
    }
}