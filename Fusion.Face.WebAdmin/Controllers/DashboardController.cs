using Fusion.Face.WebAdmin.Biz;
using Fusion.Face.WebAdmin.Common;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Face.WebAdmin.Controllers
{
    [AuthorizedUser]
    public class DashboardController : Controller
    {
        [FunctionMapping("F.DASHBOARD.MAIN")]
        public ActionResult Main()
        {
            MainDashboardModel model = new MainDashboardModel() { TotalActiveUsers = 25, TotalActiveGroups = 10 };

            return View(model);
        }


        [FunctionMapping("F.DASHBOARD.MAIN")]
        public ActionResult DashboardSummary()
        {
            DashboardBiz biz = new DashboardBiz();
            return Json(biz.GetDashboardSummary(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [FunctionMapping("F.DASHBOARD.MAIN")]
        public ActionResult TransactionSummary()
        {
            ReportBiz biz = new ReportBiz();

            SearchInfo searchInfo = new SearchInfo();
            searchInfo.search = new DtSearch() { value = "" };
            searchInfo.search.fields = new string[] { "startDate", "endDate" };
            searchInfo.search.values = new string[] { DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd") };

            return Json(biz.SearchReportTransactionSummary(searchInfo));
        }
    }
}