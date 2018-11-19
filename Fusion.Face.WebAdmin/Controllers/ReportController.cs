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
    public class ReportController : Controller
    {
        [FunctionMapping("F.REPORT.REPORTA")]
        public ActionResult ReportA()
        {
            return View();
        }

        [HttpPost]
        [FunctionMapping("F.REPORT.REPORTA")]
        public ActionResult SearchReportA(SearchInfo searchInfo)
        {
            ReportBiz biz = new ReportBiz();

            return Json(biz.SearchReportA(searchInfo));
        }

        [FunctionMapping("F.REPORT.TRANSACTION")]
        public ActionResult Transaction()
        {
            return View();
        }

        [HttpPost]
        [FunctionMapping("F.REPORT.TRANSACTION")]
        public ActionResult SearchTransaction(SearchInfo searchInfo)
        {
            ReportBiz biz = new ReportBiz();

            return Json(biz.SearchReportTransaction(searchInfo));
        }

        [FunctionMapping("F.REPORT.TRANSACTION.SUMMARY")]
        public ActionResult TransactionSummary()
        {
            return View();
        }

        [HttpPost]
        [FunctionMapping("F.REPORT.TRANSACTION.SUMMARY")]
        public ActionResult SearchTransactionSummary(SearchInfo searchInfo)
        {
            ReportBiz biz = new ReportBiz();

            return Json(biz.SearchReportTransactionSummary(searchInfo));
        }
    }
}