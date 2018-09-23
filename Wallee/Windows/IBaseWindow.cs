using System;
using System.Windows;
using System.Windows.Controls;

namespace Wallee.Windows
{
    interface IBaseWindow
    {
        event EventHandler<UIElement> ChangePage;
    }
}