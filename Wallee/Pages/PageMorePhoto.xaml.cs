using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignColors;
using Wallee.Utils;

namespace Wallee.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMorePhoto.xaml
    /// </summary>
    public partial class PageMorePhoto : UserControl, INotifyPropertyChanged
    {
        public AsyncCommand SearchCommand { get; }

        public PageMorePhoto()
        {
            //SearchCommand = new AsyncCommand(ButtonBase_OnClickUpdata);

            InitializeComponent();
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

    public static class CommandPageMorePhoto
    {
    }
}