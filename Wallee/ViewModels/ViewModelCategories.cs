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
        public ViewModelCategories(IServiceSetting serviceSetting,Action<object> executeCommandSendTag)
        {
            _serviceSetting = serviceSetting;
            CommandSendTag = new CustomCommand(executeCommandSendTag);
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