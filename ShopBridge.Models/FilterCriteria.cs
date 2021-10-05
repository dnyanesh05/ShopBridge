using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Models
{
    public class FilterCriteria : XMLCast
    {
        public FilterCriteria()
        {
            FilterCollection = new List<KeyValuePair>();
        }
        public int NumberOfRecords { get; set; }
        public int SkipRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortByField { get; set; }
        public string OrderBy { get; set; }
        public string Search { get; set; }
        public List<KeyValuePair> FilterCollection { get; set; }
        public ProcessFields ProcessFields { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string ToXML()
        {
            return CreateXML(this);
        }
    }

    public class KeyValuePair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
