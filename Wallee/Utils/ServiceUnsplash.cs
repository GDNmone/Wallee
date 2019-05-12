using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Unsplasharp;
using Unsplasharp.Models;

namespace Wallee.Utils
{
    public static class ServiceUnsplash
    {
        public class HeightPhoto
        {
            public HeightPhoto(ObservableCollection<Photo> listPhoto)
            {
                ListPhoto = listPhoto;
            }

            public double Height { get; set; }
            public ObservableCollection<Photo> ListPhoto { get; set; }
        }

        public static ObservableCollection<Photo> ListOne { get; set; } = new ObservableCollection<Photo>();
        public static ObservableCollection<Photo> ListTwo { get; set; } = new ObservableCollection<Photo>();
        public static ObservableCollection<Photo> ListThree { get; set; } = new ObservableCollection<Photo>();

        static List<HeightPhoto> listsHeight = new List<HeightPhoto>()
        {
            new HeightPhoto(ListOne),
            new HeightPhoto(ListTwo),
            new HeightPhoto(ListThree),
        };


        public static UnsplasharpClient client { get; set; } =
            new UnsplasharpClient("7c508cce62ff5e555102ef45c3a33854cd106e8fd6d46999a0b33f5e92001844");

        public static async Task GetPhoto(int numPage, string searchText, Dispatcher dispatcher)
        {
            var te = Stopwatch.StartNew();
            Console.WriteLine("1/start/" + nameof(GetPhoto) + '/' + te.ElapsedMilliseconds);

            var photosFound = await client.SearchPhotos(searchText, numPage, 30);

            Console.WriteLine("2/fin/" + nameof(GetPhoto) + '/' + te.ElapsedMilliseconds);
            await Task.Factory.StartNew(() => ShowPhoto(photosFound, dispatcher));
            Console.WriteLine("3/end/" + nameof(GetPhoto) + '/' + te.ElapsedMilliseconds);
        }

        private static void ShowPhoto(List<Photo> photosFound, Dispatcher dispatcher)
        {
            var te = Stopwatch.StartNew();

            if (photosFound.Count == 0) return;

            foreach (var photo in photosFound)
            {
                photo.ActualWidth = 250d;
                var line = MinHeightLine();
                dispatcher.Invoke(() => line.ListPhoto.Add(photo));
                line.Height += photo.ActualHeight;
            }

            var MaxList = MaxHeightLine();

            var MaxHeightValue = HeightList(MaxList.ListPhoto);

            MaxList.ListPhoto.ToList().ForEach(photo => photo.Margin = new Thickness(0));

            foreach (var list in listsHeight)
            {
                if (list != MaxList)
                {
                    var height = HeightList(list.ListPhoto);
                    var added = (((double) MaxHeightValue) - (height)) / (double) (list.ListPhoto.Count - 1);
                    list.ListPhoto.ToList().ForEach(photo => photo.Margin = new Thickness() {Bottom = added});
                }
            }

            Console.WriteLine("1/end/" + nameof(ShowPhoto) + '/' + te.ElapsedMilliseconds);
        }

        private static HeightPhoto MinHeightLine()
        {
            var heigthMin = listsHeight
                .Min(photo => photo.Height);
            return listsHeight.Find(photo => photo.Height.Equals(heigthMin));
        }

        private static HeightPhoto MaxHeightLine()
        {
            var heightMax = listsHeight
                .Max(photo => photo.Height);
            return listsHeight.Find(photo => photo.Height.Equals(heightMax));
        }

        private static double HeightList(ObservableCollection<Photo> list)
        {
            return listsHeight.Find(photo => photo.ListPhoto.Equals(list)).Height;
            ;
        }

        public static void Reset()
        {
            ListTwo.Clear();
            ListOne.Clear();
            ListThree.Clear();
        }
    }
}