using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Unsplasharp.Models;

namespace Wallee.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlViewImage.xaml
    /// </summary>
    public partial class UserControlViewImage : UserControl
    {
        public UserControlViewImage()
        {
            CommandBindings.Add(new CommandBinding(DownloadCommand, Executed_CommadnDownload));
            DataContextChanged += (sender, args) => { sender = sender; };
            InitializeComponent();
        }


        static UserControlViewImage()
        {
            #region Override DependencyProperties

            #endregion

            #region Initialization DependencyProperties

            PhotoShowProperty = DependencyProperty.Register("PhotoShow",
                typeof(Photo), typeof(UserControlViewImage),
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
            var control = d as UserControlViewImage;
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

        #endregion

        #endregion
    }
}