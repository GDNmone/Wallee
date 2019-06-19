using System;
using System.Collections.Generic;
using Didaktika.MVVM;
using Wallee.Interfaces;
using Wallee.Models;

namespace Wallee.ViewModels
{
    public class ViewModelCategories : ViewModelNavigation
    {
        private IServiceSetting _serviceSetting;
        Action<object> Execute_CommandSendTag=null;
        public ViewModelCategories(IServiceSetting serviceSetting,Action<object> execute_CommandSendTag)
        {
            _serviceSetting = serviceSetting;
            Execute_CommandSendTag = execute_CommandSendTag;
            CommandSendTag = new CustomCommand(Execute_CommandSendTag);
        }

        #region Property ListTiles(List<ModelTile>)

        public List<ModelTile> ListTiles
        {
            get { return _serviceSetting.ListTags; }
            set
            {
                _serviceSetting.ListTags = value;
                OnPropertyChanged(nameof(ListTiles));
            }
        }

        #endregion

        public CustomCommand CommandSendTag { get; }
    }
}