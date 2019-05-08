using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Unsplasharp;
using Unsplasharp.Models;

namespace Wallee.Utils
{
    public static class ServiceUnsplash
    {
        public static ObservableCollection<Photo> ListOne { get; set; } = new ObservableCollection<Photo>();
        public static ObservableCollection<Photo> ListTwo { get; set; } = new ObservableCollection<Photo>();
        public static ObservableCollection<Photo> ListThree { get; set; } = new ObservableCollection<Photo>();

        static List<ObservableCollection<Photo>> lists = new List<ObservableCollection<Photo>>()
        {
            ListOne,
            ListTwo,
            ListThree
        };

        private static UnsplasharpClient client { get; set; } =
            new UnsplasharpClient("7c508cce62ff5e555102ef45c3a33854cd106e8fd6d46999a0b33f5e92001844");

        public static async void GetPhoto(int numPage, string searchText)
        {
            var photosFound = await (client.SearchPhotos(searchText, numPage, 40));

            foreach (var t in photosFound)
            {
                MinimunHeight().Add(t);
            }

            var MaxList = MaxHeight();

            var MaxHeightValue = HeightList(MaxList);

            foreach (var list in lists)
            {
                if (list != MaxList)
                {
                    var added = (((double)MaxHeightValue) - (HeightList(list)) / (double)(list.Count - 1));
                    list.ToList().ForEach(photo => photo.Margin = new Thickness() {Bottom = added});
                }
            }
        }


        private static ObservableCollection<Photo> MinimunHeight()
        {
            var listMin = lists
                .Select(HeightList).ToList();
            Console.WriteLine("min =",  listMin.Min());
            return lists[listMin.ToList().IndexOf(listMin.Min())];
        }

        private static ObservableCollection<Photo> MaxHeight()
        {
            var list = lists
                .Select(HeightList);
            return lists[list.ToList().IndexOf(list.Max())];
        }

        private static double HeightList(ObservableCollection<Photo> list)
        {
            var t = list.Count == 0 ? 0 : list.Sum(photo => ((double)photo.Height / (double)photo.Width) * 250d);
            return t ;
        }

        public static void Reset()
        {
            ListTwo.Clear();
            ListOne.Clear();
            ListThree.Clear();
        }
    }
}