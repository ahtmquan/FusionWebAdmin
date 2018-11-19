using Fusion.Face.WebAdmin.Biz;
using Fusion.Face.WebAdmin.Common;
using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fusion.Face.WebAdmin.Controllers
{
    [AuthorizedUser]
    public class GroupController : Controller
    {
        IGroupBiz gBiz;
        IUserBiz uBiz;

        public GroupController()
        {
            this.gBiz = new GroupBiz(new DataBoxes());
            this.uBiz = new UserBiz(new DataBoxes());
        }

        public GroupController(IGroupBiz gBiz, IUserBiz uBiz)
        {
            this.gBiz = gBiz;
            this.uBiz = uBiz;
        }


        [FunctionMapping("F.GROUP.SEARCH")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [FunctionMapping("F.GROUP.SEARCH")]
        public ActionResult Search(SearchInfo searchInfo)
        {
            return Json(gBiz.Search(searchInfo));
        }

        [HttpGet]
        [FunctionMapping("F.GROUP.EDIT")]
        public ActionResult FindUser(string keyword)
        {
            SearchInfo searchInfo = new SearchInfo();
            searchInfo.search = new DtSearch() { value = keyword };
            searchInfo.start = 0;
            searchInfo.length = 10;
            SearchResult result = uBiz.Search(searchInfo);

            return Json(result.data, JsonRequestBehavior.AllowGet);
        }

        [FunctionMapping("F.GROUP.EDIT")]
        public ActionResult Edit(Guid id)
        {
            EditGroupFormModel model = new EditGroupFormModel();
            model.Info = gBiz.Find(id);
            model.Permisisons = gBiz.GetPermissions(id);
            model.Users = gBiz.GetUsers(id);
            model.JsonUsers = JsonConvert.SerializeObject(model.Users);

            return View(model);
        }

        [HttpPost]
        [FunctionMapping("F.GROUP.EDIT")]
        public ActionResult SaveEdit(EditGroupFormModel model)
        {
            gBiz.Update(model.Info);
            gBiz.UpdatePermission(model.Info.ID, model.Permisisons);
            gBiz.UpdateUsers(model.Info.ID, model.Users);
            gBiz.SaveChanges();

            return Json(model);
        }
    }
}