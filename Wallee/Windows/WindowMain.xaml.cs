using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using Wallee.Pages;

namespace Wallee.Windows
{
    /// <summary>
    /// Логика взаимодействия для WindowMain.xaml
    /// </summary>
    public partial class WindowMain : System.Windows.Window, IBaseWindow
    {
        private object _sourcePage = new PageMain();

        #region Events

        public event EventHandler<UIElement> ChangePage;

        #endregion

        #region Dependetcy property


        #endregion


        public WindowMain()
        {
            InitializeComponent();
        }

        public object SourcePage
        {
            get => _sourcePage;
            set
            {
                _sourcePage = value;
                ChangePage?.Invoke(this, (UIElement)value);
            }
        }
    }
}