using Fusion.Face.WebAdmin.Common;
using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Biz
{
    public class DashboardBiz
    {
        public DashboardSummaryInfo GetDashboardSummary()
        {
            using (FusionFaceDb db = new FusionFaceDb())
            {
                DashboardSummaryInfo info = new DashboardSummaryInfo();
                info.TotalUsers = db.UserMasters.Where(u => u.Status == "ACTIVE").Count();
                info.TotalPeople = db.People.Where(u => u.Status == "ACTIVE").Count();
                info.TotalActivities = db.TransactionSummaries.Sum(i => i.Total);
                info.LastLoginString = FusionAppContext.Current.User.LastLogin.HasValue ?
                    FusionAppContext.Current.User.LastLogin.Value.ToString("yyyy-MM-dd HH:mm:ss") : "";

                return info;
            }
        }
    }
}