using Wallee.Utils.MVVM;

namespace Wallee.Models
{
    public class ModelTile : Model
    {
        #region Property Text(string)

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        #endregion
    }
}