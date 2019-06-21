using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Unsplasharp.Models;

namespace Wallee.Views
{
    /// <summary>
    /// Interaction logic for ViewImages.xaml
    /// </summary>
    public partial class ViewImages : UserControl
    {
        public ViewImages()
        {
            CommandBindings.Add(new CommandBinding(DownloadCommand, Executed_CommadnDownload));
            CommandBindings.Add(new CommandBinding(PrintCommand, Executed_CommadnPrint));
            InitializeComponent();
        }

        private void Executed_CommadnPrint(object sender, ExecutedRoutedEventArgs e)
        {
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(PhotoShow.Urls.Full);
            bi.EndInit();

            var vis = new DrawingVisual();
            using (var dc = vis.RenderOpen())
            {
                dc.DrawImage(bi, new Rect {Width = bi.Width, Height = bi.Height});
            }

            var pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                pdialog.PrintVisual(vis, PhotoShow.Description);
            }
        }


        static ViewImages()
        {
            #region Override DependencyProperties

            #endregion

            #region Initialization DependencyProperties

            PhotoShowProperty = DependencyProperty.Register("PhotoShow",
                typeof(Photo), typeof(ViewImages),
                new PropertyMetadata(null, PhotoShow_DependencyChange));

            #endregion
        }


        #region DependencyProperties

        #region Fields DependencyProperties

        public static readonly DependencyProperty PhotoShowProperty;

        #endregion

        #region Properties DependencyProperties

        public Photo PhotoShow
        {
            get { return (Photo) GetValue(PhotoShowProperty); }
            set { SetValue(PhotoShowProperty, value); }
        }

        #endregion

        #region Methods DependencyProperties

        private static void PhotoShow_DependencyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ViewImages;
            var value = (Photo) e.NewValue;
            //if (value != null)
            //    control.PhotoShow = value;
        }

        #endregion

        #endregion

        #region Commands

        #region Methods Commands

        private void Executed_CommadnDownload(object sender, ExecutedRoutedEventArgs e)
        {
            var t = PhotoShow;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = t.Id + ".jpg";
            if ((bool) dialog.ShowDialog())
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(t.Urls.Full), dialog.FileName);
                }
            }
        }

        #endregion

        #region Fields Commands

        public static RoutedCommand DownloadCommand { get; } = new RoutedCommand();

        public static RoutedCommand PrintCommand { get; } = new RoutedCommand();

        #endregion

        #endregion
    }
}