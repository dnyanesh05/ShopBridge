using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Models
{
    public class Item : XMLCast
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemCategoryID { get; set; }
        public double Price{ get; set; }
        public double Size { get; set; }
        public string Color { get; set; }
        public bool IsItemAvailable{ get; set; }
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
