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
    public class PersonController : Controller
    {
        [FunctionMapping("F.PERSON.SEARCH")]
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [FunctionMapping("F.PERSON.SEARCH")]
        public ActionResult Search(SearchInfo searchInfo)
        {
            PersonBiz biz = new PersonBiz();

            return Json(biz.Search(searchInfo));
        }

        [FunctionMapping("F.PERSON.DETAIL.VIEW")]
        public ActionResult View(Guid id)
        {
            PersonBiz biz = new PersonBiz();

            PersonInfo person = biz.Find(id);

            return View(person);
        }

        [FunctionMapping("F.PERSON.DETAIL.VIEW")]
        public ActionResult Photo(Guid id)
        {
            PersonBiz biz = new PersonBiz();

            byte[] data = biz.GetPhotoData(id);

            return File(data, "image/jpg");
        }

        [HttpPost]
        [FunctionMapping("F.PERSON.PHOTO.DELETE")]
        public ActionResult DeletePhoto(Guid id)
        {
            PersonBiz biz = new PersonBiz();
            PhotoInfo photoInfo = biz.GetPhoto(id);

            if (biz.DeletePhoto(id))
            {
                PersonInfo person = biz.Find(photoInfo.PersonID);
                return RedirectToAction("View", person);
            }

            return View();
        }
    }
}