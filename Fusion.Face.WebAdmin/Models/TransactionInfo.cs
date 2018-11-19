using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class TransactionInfo
    {
        public Guid ID { get; set; }
        public string TranxType { get; set; }
        public string ObjectID { get; set; }
        public string ObjectName { get; set; }
        public DateTime RecordedDate { get; set; }
        public string RecordedDateString { get { return this.RecordedDate.ToString("dd/MM/yyyy HH:mm:ss"); } }
        public string ClientID { get; set; }
    }
}