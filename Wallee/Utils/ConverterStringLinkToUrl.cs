using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Unsplasharp.Models;

namespace Wallee.Utils
{
    internal class ConverterStringLinkToUrl
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object p, CultureInfo ci) =>
            new BitmapImage(new Uri(((Urls) value).Small));

        public object ConvertBack(object value, Type targetType, object p, CultureInfo ci) =>
            throw new NotSupportedException();
    }
}