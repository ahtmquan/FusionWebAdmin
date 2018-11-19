using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class PersonInfo
    {
        public Guid ID { get; set; }
        public string Fullname { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime? Dob { get; set; }
        public string Nationality { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<PhotoInfo> Photos { get; set; }

        public PersonInfo()
        {
            Photos = new List<PhotoInfo>();
        }
    }
}