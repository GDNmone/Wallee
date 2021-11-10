using Wallee.Utils.MVVM;

namespace Wallee.Models
{
    public class ModelTile : Model
    {
        public ModelTile(string text, string textSearch)
        {
            _text = text;
            _textSearch = textSearch;
        }

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

        #region Property TextSearch(string)

        private string _textSearch;

        public string TextSearch
        {
            get { return _textSearch; }
            set
            {
                _textSearch = value;
                OnPropertyChanged(nameof(TextSearch));
            }
        }

        #endregion
    }
}