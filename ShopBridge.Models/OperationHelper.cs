using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace ShopBridge.Models
{
    public static class OperationHelper
    {
        public static void GetFilterCriteria(IEnumerable<KeyValuePair<string, string>> queryStringParams, FilterCriteria filterCriteria)
        {
            if (queryStringParams != null)
            {
                var filters = from s in queryStringParams
                              where s.Key.Contains("filter.filters")
                              select s;
                var i = 0;
                foreach (var item in filters)
                {
                    if (item.Key.Contains("filter.filters[" + i + "].field"))
                    {
                        if (item.Value.ToLower().Contains("date"))
                            filterCriteria.FilterCollection.Add(new KeyValuePair() { Key = item.Value, Value = filters.FirstOrDefault(x => x.Key.Contains("filter.filters[" + i + "].value")).Value.Substring(4, 20) });
                        else
                            filterCriteria.FilterCollection.Add(new KeyValuePair() { Key = item.Value, Value = filters.FirstOrDefault(x => x.Key.Contains("filter.filters[" + i + "].value")).Value });

                        i++;
                    }
                }

                foreach (var item in queryStringParams)
                {
                    switch (item.Key)
                    {
                        case "search":
                            filterCriteria.Search = item.Value;
                            break;
                        case "take":
                            filterCriteria.NumberOfRecords = Convert.ToInt32(item.Value);
                            break;
                        case "skip":
                            filterCriteria.SkipRecords = Convert.ToInt32(item.Value);
                            break;
                        case "page":
                            filterCriteria.PageNumber = Convert.ToInt32(item.Value);
                            break;
                        case "pageSize":
                            filterCriteria.PageSize = Convert.ToInt32(item.Value);
                            break;
                        case "sort[0].field":
                            filterCriteria.SortByField = item.Value;
                            break;
                        case "sort[0].dir":
                            filterCriteria.OrderBy = item.Value;
                            break;
                        case "processFields":
                            JavaScriptSerializer oJS = new JavaScriptSerializer();
                            ProcessFields processFields = new ProcessFields();
                            filterCriteria.ProcessFields = oJS.Deserialize<ProcessFields>(item.Value);
                            break;
                    }
                }
            }
        }
    }
}
