using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using StyleFluentWpf.CustomControls.ControlBlackOut;
using Wallee.Views;

namespace Wallee.CustomControls
{
    /// <summary>
    /// Логика взаимодействия для WindowMain.xaml
    /// </summary>
    public partial class WindowMain : System.Windows.Window, INotifyPropertyChanged
    {
        #region Property IsOverlay(bool)

        private bool _IsOverlay;

        public bool IsOverlay
        {
            get { return _IsOverlay; }
            set
            {
                _IsOverlay = value;
                OnPropertyChanged(nameof(IsOverlay));
            }
        }

        #endregion


        public static RoutedUICommand OpenViewModel = new RoutedUICommand();
        public static RoutedUICommand OpenHelp = new RoutedUICommand();

        public WindowMain()
        {
            CommandBindings.Add(new CommandBinding(OpenHelp, Executed_OpenHelp));
            CommandBindings.Add(new CommandBinding(OpenViewModel,
                (sender, args) => { Content = args.Parameter; }));

            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand,
                this.OnMaximizeWindow,
                this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow,
                this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow,
                this.OnCanResizeWindow));

            InputBindings.Add(new InputBinding(OpenHelp, new KeyGesture(Key.F1)));
            InitializeComponent();
        }

        private void Executed_OpenHelp(object sender, ExecutedRoutedEventArgs e)
        {
            // ControlBlackOut.TurnOverlayCommand.Execute(true, this);
            IsOverlay = true;
            
            new AboutView()
            {
                Left = this.Left +  (this.ActualWidth-350) / 2,
                Top = this.Top +  (this.ActualHeight-267) / 2
            }.ShowDialog();
            IsOverlay = false;
            // ControlBlackOut.TurnOverlayCommand.Execute(false, this);
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }


        #region System

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}