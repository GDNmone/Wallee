using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Unsplasharp;
using Unsplasharp.Models;

namespace Wallee.Utils
{
    public static class ServiceUnsplash
    {
        public static UnsplasharpClient client { get; set; } =
            //   new UnsplasharpClient("7c508cce62ff5e555102ef45c3a33854cd106e8fd6d46999a0b33f5e92001844");
            new UnsplasharpClient("93123f0db401f8367e061a60e9b0976b9bc9c3cafe5133f344bba4010c97a4de",
                "ec8401ec0727226a41f9fea4ef184c10f7efef4b009ee910dbf3ca386a");

        public static async Task<IEnumerable<Photo>> GetPhoto(int numPage, string searchText)
        {
            //var te = Stopwatch.StartNew();
            // Console.WriteLine("1/start/" + nameof(GetPhoto) + '/' + te.ElapsedMilliseconds);

            return await client.SearchPhotos(searchText, numPage, 40);

            //Console.WriteLine("2/fin/" + nameof(GetPhoto) + '/' + te.ElapsedMilliseconds);
            //await Task<List<Photo>>.Factory.StartNew(() => StructurPhotoToColumns(photosFound, columnsPhotos.Select(photos => photos.ToList()).ToList() ));
            //Console.WriteLine("3/end/" + nameof(GetPhoto) + '/' + te.ElapsedMilliseconds);
        }

        //private static List<List<Photo>> StructurPhotoToColumns(List<Photo> photosFound, List<List<Photo>> columnsPhotos)
        //{
        //    var te = Stopwatch.StartNew();
        //
        //    if (photosFound.Count == 0) return null;
        //
        //    foreach (var photo in photosFound)
        //    {
        //        photo.ActualWidth = 250d;
        //        var line = MinHeightLine();
        //        columnsPhotos.Invoke(() => line.ListPhoto.Add(photo));
        //        line.Height += photo.ActualHeight;
        //    }
        //
        //    var MaxList = MaxHeightLine();
        //
        //    var MaxHeightValue = HeightList(MaxList.ListPhoto);
        //
        //    MaxList.ListPhoto.ToList().ForEach(photo => photo.Margin = new Thickness(0));
        //
        //    foreach (var list in listsHeight)
        //    {
        //        if (list != MaxList)
        //        {
        //            var height = HeightList(list.ListPhoto);
        //            var added = (((double) MaxHeightValue) - (height)) / (double) (list.ListPhoto.Count - 1);
        //            list.ListPhoto.ToList().ForEach(photo => photo.Margin = new Thickness() {Bottom = added});
        //        }
        //    }
        //
        //    Console.WriteLine("1/end/" + nameof(StructurPhotoToColumns) + '/' + te.ElapsedMilliseconds);
        //}

        //private static HeightPhoto MinHeightLine()
        //{
        //    var heigthMin = listsHeight
        //        .Min(photo => photo.Height);
        //    return listsHeight.Find(photo => photo.Height.Equals(heigthMin));
        //}
        //
        //private static HeightPhoto MaxHeightLine()
        //{
        //    var heightMax = listsHeight
        //        .Max(photo => photo.Height);
        //    return listsHeight.Find(photo => photo.Height.Equals(heightMax));
        //}
        //
        //private static double HeightList(ObservableCollection<Photo> list)
        //{
        //    return listsHeight.Find(photo => photo.ListPhoto.Equals(list)).Height;
        //    ;
        //}
        //
        //public static void Reset()
        //{
        //    ListTwo.Clear();
        //    ListOne.Clear();
        //    ListThree.Clear();
        //}

    }
}