using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Unsplasharp.Models;

namespace Wallee.Utils
{
    class ConverterStringLinkToUrl
    :IValueConverter
        {
            public object Convert(object value, Type targetType, object p, CultureInfo ci) =>
                new BitmapImage(new Uri(((Urls)value).Small));

            public object ConvertBack(object value, Type targetType, object p, CultureInfo ci) =>
                throw new NotSupportedException();
        
    }
}
