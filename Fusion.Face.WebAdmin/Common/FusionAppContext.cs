using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Fusion.Face.WebAdmin.Common
{
    public class FusionAppContext
    {
        HttpSessionState Session = null;

        public FusionAppContext()
        {
            Session = HttpContext.Current.Session;
        }

        public UserProfile User
        {
            get { return Session["CurrentUser"] as UserProfile; }
            set { Session["CurrentUser"] = value; }
        }

        public string SecurityCode
        {
            get { return Session["Captcha"] as string; }
            set { Session["Captcha"] = value; }
        }

        public int PasswordTryCount
        {
            get
            {
                if (Session["PasswordTryCount"] == null)
                {
                    Session["PasswordTryCount"] = 0;
                }

                return (int)Session["PasswordTryCount"];
            }
            set { Session["PasswordTryCount"] = value; }
        }

        static FusionAppContext CurrentContext;
        public static FusionAppContext Current
        {
            get
            {
                if (CurrentContext == null)
                {
                    CurrentContext = new FusionAppContext();
                }

                return CurrentContext;
            }
        }
    }
}