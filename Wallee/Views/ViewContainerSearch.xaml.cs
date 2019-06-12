using System.Windows;
using System.Windows.Controls;

namespace Wallee.Views
{
    /// <summary>
    /// Interaction logic for ViewContainerSearch.xaml
    /// </summary>
    public partial class ViewContainerSearch : UserControl
    {
        public ViewContainerSearch()
        {
            InitializeComponent();
        }

        static ViewContainerSearch()
        {
            #region Initialize Dependency

            TemplateContentProperty = DependencyProperty.Register("TemplateContent",
                typeof(DataTemplate), typeof(ViewContainerSearch),
                new PropertyMetadata(null, TemplateContent_DependencyChange));

            #endregion
        }


        #region DepentencyPropertys

        public DataTemplate TemplateContent
        {
            get { return (DataTemplate) GetValue(TemplateContentProperty); }
            set { SetValue(TemplateContentProperty, value); }
        }

        public static readonly DependencyProperty TemplateContentProperty;

        private static void TemplateContent_DependencyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ViewContainerSearch;
            var value = (DataTemplate) e.NewValue;
            //if (value != null)
            //    control.TemplateContent = value;
        }

        #endregion
    }
}