using System.Collections.ObjectModel;
using Didaktika.MVVM;
using Wallee.Models;

namespace Wallee.ViewModels
{
    public class ViewModelMain : ViewModelNavigation
    {
        public ViewModelMain()
        {
            #region RegisterCommands

            CommandPassTag = CustomCommand<ModelTile>.CreateCommand(this, Action_PassTag);

            #endregion
        }

        #region Commands

        #region Fields

        public CustomCommand<ModelTile> CommandPassTag { get; }

        #endregion

        #region Methods

        private void Action_PassTag(ModelTile tile)
        {
            base.OpenViewModel(new ViewModelMorePhoto() {TextSearch = tile.TextSearch, ListTags = ListTiles});
        }

        #endregion

        #endregion


        #region Property ListTiles(ObservableCollection<ModelTile>)

        private ObservableCollection<ModelTile> _listTiles = new ObservableCollection<ModelTile>()
        {
            new ModelTile("TEXTURES", "Textures"),
            new ModelTile("NATURE", "Nature"),
            new ModelTile("CURRENT EVENTS", "News"),
            new ModelTile("ARCHITECTURE", "Architecture"),
            new ModelTile("BUSINESS", "Business"),
            new ModelTile("FILM", "Film"),
            new ModelTile("ANIMALS", "Animals"),
            new ModelTile("TRAVEL", "Travel"),
            new ModelTile("FASHION", "Fashion"),
            new ModelTile("FOOD", "Food"),
            new ModelTile("SPIRITUALITY", "Spirituality"),
            new ModelTile("EXPERIMENTAL", "Experimental"),
            new ModelTile("PEOPLE", "People"),
            new ModelTile("HEALTH", "Health"),
            new ModelTile("ARTS", "Arts"),
        };

        public ObservableCollection<ModelTile> ListTiles
        {
            get { return _listTiles; }
            set
            {
                _listTiles = value;
                OnPropertyChanged(nameof(ListTiles));
            }
        }

        #endregion
    }
}