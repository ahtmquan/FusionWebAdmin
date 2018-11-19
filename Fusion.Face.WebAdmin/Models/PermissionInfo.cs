using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class PermissionInfo
    {
        public Guid ID { get; set; }
        public Guid GroupID { get; set; }
        public string FunctionID { get; set; }
        public string FunctionName { get; set; }
        public string ModuleID { get; set; }
        public string ModuleName { get; set; }
        public bool Enabled { get; set; }
    }
}