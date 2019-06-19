using System.Collections.Generic;
using Wallee.Interfaces;
using Wallee.Models;

namespace Wallee.Services
{
    public class ServiceSettingStatic : IServiceSetting
    {
        public List<ModelTile> ListTags { get; set; } = new List<ModelTile>()
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
    }
}