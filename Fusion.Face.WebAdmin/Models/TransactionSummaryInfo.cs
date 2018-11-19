using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class TransactionSummaryInfo
    {
        public string TranxType { get; set; }
        public DateTime RecordedDate { get; set; }
        public string RecordedDateString { get { return this.RecordedDate.ToString("dd/MM/yyyy"); } }
        public string ClientID { get; set; }
        public int Total { get; set; }
    }
}