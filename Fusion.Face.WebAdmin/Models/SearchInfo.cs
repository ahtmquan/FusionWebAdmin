using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Models
{
    public class SearchInfo
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public int ActType { get; set; }

        public DtSearch search { get; set; }
        public DtColumn[] columns { get; set; }
        public DtOrder[] order { get; set; }
    }

    public class DtSearch
    {
        public string value { get; set; }
        public string regex { get; set; }
        public string[] fields { get; set; }
        public string[] values { get; set; }
    }

    public class DtColumn
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DtSearch search { get; set; }
    }

    public class DtOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}