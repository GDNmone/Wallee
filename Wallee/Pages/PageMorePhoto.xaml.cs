using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Unsplasharp;
using Unsplasharp.Models;
using Wallee.Utils;

namespace Wallee.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMorePhoto.xaml
    /// </summary>
    public partial class PageMorePhoto : Page, INotifyPropertyChanged
    {
        public PageMorePhoto()
        {
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
        private void ButtonBase_OnClickUpdata(object sender, RoutedEventArgs e)
        {
            if (lastQuery == TextSerch)return;
            lastQuery = TextSerch;
            countPage = 1;
            ServiceUnsplash.Reset();
            ServiceUnsplash.GetPhoto(countPage, TextSerch);
        }

        #region System

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            countPage++;
            ServiceUnsplash.GetPhoto(countPage, TextSerch);
        }
    }
}