using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unsplasharp;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");
            getImg();
            Console.WriteLine("stop");
            Console.ReadLine();
            // while (expression)
            // {
            // }
        }

        private static async void getImg()
        {
            UnsplasharpClient client = new UnsplasharpClient(
                "93123f0db401f8367e061a60e9b0976b9bc9c3cafe5133f344bba4010c97a4de",
                "ec8401ec0727226a41f9fea4ef184c10f7efef4b009ee910dbf3ca386a");
            await client.SearchPhotos("new");
         
        }
    }
}