using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Face.WebAdmin.Models
{
    public class MenuItem
    {
        public MenuInfo Info { get; set; }
        public MenuItem Parent { get; set; }
        public List<MenuItem> Children { get; set; }

        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
    }    
}
