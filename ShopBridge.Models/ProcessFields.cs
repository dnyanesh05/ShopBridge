using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Models
{
    public class ProcessFields : XMLCast
    {
        public int EntityID { get; set; }
        public long ChildEntityID { get; set; }
        public string EntityKey { get; set; }
        public string ChildEntityKey { get; set; }
        public int? UserID { get; set; }
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
}
