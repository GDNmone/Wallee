using System.ComponentModel;

namespace Wallee.Utils.MVVM
{
    public class Model : INotifyPropertyChanged
    {
        #region System

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}