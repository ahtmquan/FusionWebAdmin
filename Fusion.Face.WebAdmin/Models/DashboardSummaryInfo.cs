using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class DashboardSummaryInfo
    {
        public int TotalUsers { get; set; }
        public int TotalPeople { get; set; }
        public int TotalActivities { get; set; }
        public string LastLoginString { get; set; }
    }
}