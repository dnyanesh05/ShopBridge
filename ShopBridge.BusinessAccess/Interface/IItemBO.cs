using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.BusinessAccess.Interface
{
    public interface IItemBO
    {
        IList<Item> GetItems(FilterCriteria filterCriteria);
        string UpdateItem(ProcessFields processFields);
        string DeleteItem(ProcessFields processFields);
    }
}
