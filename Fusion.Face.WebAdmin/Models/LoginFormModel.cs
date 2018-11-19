using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class LoginFormModel
    {
        public string ErrorMessage { get; set; }

        public LoginInfo Info { get; set; }
    }
}