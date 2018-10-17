using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unsplasharp;
using Unsplasharp.Models;

namespace Wallee.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMorePhoto.xaml
    /// </summary>
    public partial class PageMorePhoto : Page
    {
        private UnsplasharpClient client;
        public PageMorePhoto()
        {
            InitializeComponent();
  //          UnsplasharpClient client = new UnsplasharpClient("");
            client = new UnsplasharpClient("7c508cce62ff5e555102ef45c3a33854cd106e8fd6d46999a0b33f5e92001844");
            
        }

        public async void GetPhoto()
        {
            var photosFound = await (client.SearchPhotos("mountains"));
            
            foreach (var photo in photosFound)
            {
                ListImages.Add(photo);
            }
        }

        public ObservableCollection<Photo> ListImages { get; set; }
         = new ObservableCollection<Photo>();

        private void ButtonBase_OnClickUpdata(object sender, RoutedEventArgs e)
        {
            GetPhoto();
        }
    }
}
