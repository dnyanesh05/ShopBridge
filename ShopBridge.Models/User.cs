using System;

namespace ShopBridge.Models
{
    public class User : XMLCast
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Salutation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string HomePhoneNumber { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string MobileNumber { get; set; }
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
