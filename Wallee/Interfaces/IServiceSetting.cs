using System.Collections.Generic;
using Wallee.Models;

namespace Wallee.Interfaces
{
    public interface IServiceSetting
    {
        List<ModelTile> ListTags { get; set; }
    }
}