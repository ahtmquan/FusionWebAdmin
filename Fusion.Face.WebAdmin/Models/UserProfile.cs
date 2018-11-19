using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class UserProfile
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public DateTime? LastLogin { get; set; }

        public List<string> Permissions { get; private set; }
        public List<string> Roles { get; private set; }
        public List<MenuItem> Menus { get; private set; }

        public UserProfile()
        {
            Permissions = new List<string>();
            Roles = new List<string>();
            Menus = new List<MenuItem>();
        }
    }
}