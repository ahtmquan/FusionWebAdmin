using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class PhotoInfo
    {
        public Guid ID { get; set; }
        public byte[] Data { get; set; }
        public Guid PersonID { get; set; }
    }
}