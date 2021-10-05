using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Models
{
    public class Role : XMLCast
    {
        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string RoleCode { get; set; }
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
