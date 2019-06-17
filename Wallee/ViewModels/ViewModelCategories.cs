using System.Collections.Generic;
using Didaktika.MVVM;
using Wallee.Models;

namespace Wallee.ViewModels
{
    public class ViewModelCategories : ViewModelNavigation
    {
        #region Property ListTiles(List<ModelTile>)

        private List<ModelTile> _listTiles = new List<ModelTile>()
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

        public List<ModelTile> ListTiles
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