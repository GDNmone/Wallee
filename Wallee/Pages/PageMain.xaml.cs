using System.Windows;
using System.Windows.Controls;
using Wallee.Windows;

namespace Wallee.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : UserControl
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnClick_Popular(object sender, RoutedEventArgs e)
        {
            CommandsWindow.OpenControl.Execute(new PageMorePhoto() {TextSerch = "Popular"}, this);
        }

        private void Hyperlink_OnClick_New(object sender, RoutedEventArgs e)
        {
            CommandsWindow.OpenControl.Execute(new PageMorePhoto() { TextSerch = "New" }, this);
        }
    }
}