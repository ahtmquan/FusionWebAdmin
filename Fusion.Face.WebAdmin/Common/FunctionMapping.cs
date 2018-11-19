using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Common
{
    public class FunctionMapping : Attribute
    {
        public FunctionMapping(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}