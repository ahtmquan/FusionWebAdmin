using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class EditGroupFormModel
    {
        public GroupInfo Info { get; set; }
        public List<PermissionInfo> Permisisons { get; set; }
        public List<UserInfo> Users { get; set; }

        public string JsonUsers { get; set; }

        public EditGroupFormModel()
        {
            this.Permisisons = new List<PermissionInfo>();
            this.Users = new List<UserInfo>();
        }
    }
}