using Fusion.Face.WebAdmin.Biz;
using Fusion.Face.WebAdmin.Common;
using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Face.WebAdmin.Controllers
{
    [AuthorizedUser]
    public class UserController : Controller
    {
        private IUserBiz uBiz;

        public UserController()
        {
            this.uBiz = new UserBiz(new DataBoxes());
        }

        public UserController(IUserBiz uBiz)
        {
            this.uBiz = uBiz;
        }


        [FunctionMapping("F.USER.SEARCH")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [FunctionMapping("F.USER.SEARCH")]
        public ActionResult Search(SearchInfo searchInfo)
        {
            return Json(uBiz.Search(searchInfo));
        }
    }
}