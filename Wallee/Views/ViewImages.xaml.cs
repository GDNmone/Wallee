using System;
using System.IO;
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
        public static BitmapImage GetBitmapImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + "p.jpg", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }

            image.Freeze();
            return image;
        }

        private async void Executed_CommadnPrint(object sender, ExecutedRoutedEventArgs e)
        {
            BitmapImage bi;
            try
            {
                using (WebClient client = new WebClient())
                {
                    var k = await client.DownloadDataTaskAsync(PhotoShow.Urls.Regular);
                    bi = GetBitmapImage(k);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error of download image, please check connect");
                return;
            }          
           


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