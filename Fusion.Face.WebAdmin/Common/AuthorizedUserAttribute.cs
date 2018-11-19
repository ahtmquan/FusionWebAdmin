using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fusion.Face.WebAdmin.Common
{
    public class AuthorizedUserAttribute : AuthorizeAttribute
    {
        AuthorizationContext Context;
        UserProfile CurrentUser;

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            this.Context = filterContext;

            string controller = this.Context.RouteData.Values["Controller"] as string;
            string action = this.Context.RouteData.Values["Action"] as string;

            this.CurrentUser = FusionAppContext.Current.User;

            if (this.CurrentUser == null)
            {
                this.Context.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(
                                new
                                {
                                    controller = "Account",
                                    action = "Login"
                                })
                            );
            }
            else
            {
                string fCode = GetFunctionCode(controller, action);

                if (!string.IsNullOrEmpty(fCode))
                {
                    if (!this.CurrentUser.Permissions.Contains(fCode))
                    {
                        this.Context.Result = new RedirectToRouteResult(
                                new RouteValueDictionary(
                                    new
                                    {
                                        controller = "Account",
                                        action = "Error"
                                    })
                                );
                    }
                }
            }
        }

        private string GetFunctionCode(string controller, string action)
        {
            string functionCode = "";

            IEnumerable<MethodInfo> methods = Assembly.GetExecutingAssembly().GetTypes()
                   .Where(t => (t.Name == controller + "Controller" && typeof(Controller).IsAssignableFrom(t)))
                   .SelectMany(
                        type =>
                        type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                            .Where(a => a.Name == action && a.ReturnType == typeof(ActionResult))
                     );

            foreach (MethodInfo m in methods)
            {
                IEnumerable<FunctionMapping> mFunctions =
                        m.GetCustomAttributes(typeof(FunctionMapping), false)
                        .Cast<FunctionMapping>();

                foreach (FunctionMapping f in mFunctions)
                {
                    functionCode = f.Code;
                }
            }

            return functionCode;
        }
    }

}