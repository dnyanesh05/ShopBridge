using ShopBridge.BusinessAccess.Interface;
using ShopBridge.DataAccess;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class ItemController : ApiController
    {
        readonly IItemBO itemBO;

        public ItemController(IItemBO _itemBO)
        {
            itemBO = _itemBO;
        }

        private void LogOperation(Exception ex, string location, int userID = 0)
        {
            using (var dbLogger = new DBLogger())
            {
                dbLogger.LogOperation(ex, location);
            }
        }

        [HttpGet, Route("api/item/all")]
        public IHttpActionResult GetAllItems()
        {
            var queryStringParams = this.Request.GetQueryNameValuePairs();
            var filterCriteria = new FilterCriteria();
            OperationHelper.GetFilterCriteria(queryStringParams, filterCriteria);
            IList<Item> result = new List<Item>();
            try
            {
                result = itemBO.GetItems(filterCriteria);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var location = this.GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return Ok(result);
            }
        }

        [HttpPost, Route("api/item/updateItem")]
        public IHttpActionResult UpdateItem(ProcessFields processFields)
        {
            string result = "";
            try
            {
                result = itemBO.UpdateItem(processFields);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var location = GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return null;
            }
        }

        [HttpPost, Route("api/item/deleteItem")]
        public IHttpActionResult DeleteItem(ProcessFields processFields)
        {
            string result = "";
            try
            {
                result = itemBO.DeleteItem(processFields);

                return Ok(result);
            }
            catch (Exception ex)
            {
                var location = this.GetType().Name + "/" + MethodBase.GetCurrentMethod().Name;
                LogOperation(ex, location);
                return Ok(result);
            }
        }
    }
}