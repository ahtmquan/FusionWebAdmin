using Fusion.Face.WebAdmin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Biz
{
    public class BaseBiz : IBaseBiz
    {
        internal IDataBoxes Dbs;

        public BaseBiz(IDataBoxes databoxes)
        {
            this.Dbs = databoxes;
        }

        /// <summary>
        /// Save Changes context | Unit of Work | Dbs
        /// </summary>
        public void SaveChanges()
        {
            this.Dbs.Save();
        }
    }
}