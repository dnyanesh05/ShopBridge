using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Models
{
    public class Page : XMLCast
    {
        public int PageID { get; set; }

        public string PageName { get; set; }
        public string AliasName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public bool Isconfigurable { get; set; }
        public string ToXML()
        {
            return CreateXML(this);
        }
    }
}
