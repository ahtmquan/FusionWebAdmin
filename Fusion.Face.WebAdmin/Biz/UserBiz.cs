using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Biz
{
    public class UserBiz : BaseBiz, IUserBiz
    {
        public UserBiz(IDataBoxes databoxes) : base(databoxes) { }


        /// <summary>
        /// Search User | DataTable
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        public SearchResult Search(SearchInfo searchInfo)
        {
            if (searchInfo.search.value == null)
            {
                searchInfo.search.value = "";
            }

            string keyword = searchInfo.search.value;

            var q = (from u in Dbs.Users.Queryable()
                     where (u.Username.Contains(keyword)
                     || u.Fullname.Contains(keyword))
                     && !u.IsDeleted
                     select new UserInfo()
                     {
                         ID = u.ID,
                         Username = u.Username,
                         Email = u.Email,
                         Status = u.Status,
                         Fullname = u.Fullname
                     });

            int total = q.Count();



            if (searchInfo.order != null && searchInfo.order.Length > 0)
            {
                string orderBy = "";
                bool orderDesc = false;

                orderBy = searchInfo.columns[searchInfo.order[0].column].data;
                orderDesc = searchInfo.order[0].dir == "desc";

                q = q.OrderByDynamic(orderBy, orderDesc);
            }
            else
            {
                q = q.OrderBy(x => x.Username);
            }

            List<UserInfo> users = q.Skip(searchInfo.start).Take(searchInfo.length).ToList();

            SearchResult result = new SearchResult()
            {
                draw = searchInfo.draw,
                recordsTotal = total,
                recordsFiltered = total,
                data = users.ToArray()
            };

            return result;

        }
    }
}