using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Face.WebAdmin.Models
{
    public class MenuInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Position { get; set; }
        public string FunctionID { get; set; }
        public string Icon { get; set; }
        public string ParentID { get; set; }
        public string Url { get; set; }
    }
}
