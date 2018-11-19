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
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            LoginFormModel model = new LoginFormModel();
            model.ErrorMessage = "";

            return View(model);
        }

        public ActionResult Logout()
        {
            FusionAppContext.Current.User = null;

            LoginFormModel model = new LoginFormModel();
            model.ErrorMessage = "";

            return View("Login", model);
        }

        public ActionResult DoLogin(LoginFormModel model)
        {
            SecurityBiz zSecurity = new SecurityBiz();

            UserProfile userProfile = zSecurity.Authenticate(model.Info);

            if (userProfile != null)
            {
                FusionAppContext.Current.User = userProfile;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.ErrorMessage = "Invalid Username and Password";

                return View("Login", model);
            }
        }
    }
}