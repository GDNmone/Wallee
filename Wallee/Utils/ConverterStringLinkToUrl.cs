using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Unsplasharp.Models;

namespace Wallee.Utils
{
    internal class ConverterStringLinkToUrl : DependencyObject, IValueConverter
    {
        public FlagSize Flag
        {
            get { return (FlagSize) GetValue(FlagProperty); }
            set { SetValue(FlagProperty, value); }
        }

        public static readonly DependencyProperty FlagProperty = DependencyProperty.Register("Flag",
            typeof(FlagSize), typeof(ConverterStringLinkToUrl),
            new PropertyMetadata(FlagSize.Small));


        public enum FlagSize
        {
            Raw,

            Full,

            Regular,

            Small,

            Thumbnail,

            Custom
        }


        public object Convert(object value, Type targetType, object p, CultureInfo ci) =>
            new BitmapImage(GetUrlImageOfFlag((Urls) value));

        public object ConvertBack(object value, Type targetType, object p, CultureInfo ci) =>
            throw new NotSupportedException();


        private Uri GetUrlImageOfFlag(Urls value)
        {
            string link = "";

            switch (Flag)
            {
                case FlagSize.Custom:
                    link = value.Custom;
                    break;
                case FlagSize.Small:
                    link = value.Small;
                    break;
                case FlagSize.Full:
                    link = value.Full;
                    break;
                case FlagSize.Raw:
                    link = value.Raw;
                    break;
                case FlagSize.Regular:
                    link = value.Regular;
                    break;
                case FlagSize.Thumbnail:
                    link = value.Thumbnail;
                    break;
               
            }
            return  new Uri(link);
        }
    }
}