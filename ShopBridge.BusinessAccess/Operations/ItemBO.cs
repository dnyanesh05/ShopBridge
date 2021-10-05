using DataAccess;
using ShopBridge.BusinessAccess.Interface;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ShopBridge.BusinessAccess.Operations
{
    public class ItemBO : DBOperations, IItemBO
    {
        public string DeleteItem(ProcessFields processFields)
        {
            try
            {
                return CoreOperation("USP_ITEM_DELETE_BY_ID", processFields.ToXML());
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return null;
            }
        }

        public IList<Item> GetItems(FilterCriteria filterCriteria)
        {
            try
            {
                return GetOperation<Item>("USP_ITEMS_GET", filterCriteria.ToXML());
            }
            catch(Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return null;
            }
        }

        public string UpdateItem(ProcessFields processFields)
        {
            try
            {
                return CoreOperation("USP_ITEM_UODATE_BY_ID", processFields.ToXML());
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return null;
            }
        }
    }
}
