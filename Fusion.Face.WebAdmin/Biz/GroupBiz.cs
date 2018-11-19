using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Biz
{
    public class GroupBiz : BaseBiz, IGroupBiz
    {
        public GroupBiz(IDataBoxes databoxes) : base(databoxes) { }

        /// <summary>
        /// Search Group | DataTable parameters
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

            //Filter
            var q = (from u in Dbs.Groups.Queryable()
                     where (u.Name.Contains(keyword)
                     || u.Description.Contains(keyword))
                     select new GroupInfo()
                     {
                         ID = u.ID,
                         Name = u.Name,
                         Description = u.Description
                     });

            int total = q.Count();

            //Order by
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
                q = q.OrderBy(x => x.Name);
            }

            //Paging
            List<GroupInfo> groups = q.Skip(searchInfo.start).Take(searchInfo.length).ToList();

            SearchResult result = new SearchResult()
            {
                draw = searchInfo.draw,
                recordsTotal = total,
                recordsFiltered = total,
                data = groups.ToArray()
            };

            return result;
        }


        /// <summary>
        /// Get Group by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GroupInfo Find(Guid id)
        {
            var p = this.Dbs.Groups.Get(id);

            return new GroupInfo()
            {
                ID = p.ID,
                Name = p.Name,
                Description = p.Description
            };
        }


        /// <summary>
        /// Get list of permisisons by Group ID
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<PermissionInfo> GetPermissions(Guid groupId)
        {
            var q = from m in Dbs.Modules.Queryable()
                    join f in Dbs.Functions.Queryable() on m.ID equals f.ModuleID
                    join p in Dbs.Permissions.Queryable() on f.ID equals p.FunctionID into ps
                    from p in ps.DefaultIfEmpty()
                    where (p.GroupID == groupId) || (p.GroupID == null)
                    select new PermissionInfo()
                    {
                        ID = (p == null ? Guid.NewGuid() : p.ID),
                        FunctionID = f.ID,
                        FunctionName = f.Name,
                        ModuleID = m.ID,
                        ModuleName = m.Name,
                        Enabled = (p != null) ? p.Enabled : false,
                        GroupID = groupId
                    };
            return q.ToList();
        }

        /// <summary>
        /// Get List of Users by Group ID
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<UserInfo> GetUsers(Guid groupId)
        {
            var q = from m in this.Dbs.UserGroups.Queryable()
                    join u in this.Dbs.Users.Queryable() on m.UserID equals u.ID
                    where (m.GroupID == groupId && u.Status == "ACTIVE")
                    select new UserInfo()
                    {
                        ID = u.ID,
                        Username = u.Username,
                        Fullname = u.Fullname
                    };

            return q.ToList();
        }

        /// <summary>
        /// Update Group Info
        /// </summary>
        /// <param name="groupInfo"></param>
        public void Update(GroupInfo groupInfo)
        {
            GroupMaster group = Dbs.Groups.Get(groupInfo.ID);
            group.Name = groupInfo.Name;
            group.Description = groupInfo.Description;
            Dbs.Groups.Update(group);
        }

        /// <summary>
        /// Update Group Permisisons
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="permissions"></param>
        public void UpdatePermission(Guid groupId, List<PermissionInfo> permissions)
        {
            List<Permission> toberemoved = Dbs.Permissions.Queryable().Where(p => p.GroupID == groupId).ToList();
            Dbs.Permissions.Delete(toberemoved);

            foreach (PermissionInfo p in permissions)
            {
                Dbs.Permissions.Insert(new Permission()
                {
                    ID = Guid.NewGuid(),
                    GroupID = groupId,
                    FunctionID = p.FunctionID,
                    Enabled = true
                });
            }
        }

        /// <summary>
        /// Update Group's Users
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="users"></param>
        public void UpdateUsers(Guid groupId, List<UserInfo> users)
        {
            //Remove users
            List<Guid> ids = users.Select(u => u.ID).ToList();
            List<UserGroup> tobeRemoved = Dbs.UserGroups.Queryable().Where(u => u.GroupID == groupId && !ids.Contains(u.UserID)).ToList();
            Dbs.UserGroups.Delete(tobeRemoved);

            //Add users
            List<Guid> cIds = Dbs.UserGroups.Queryable().Where(u => u.GroupID == groupId).Select(u => u.UserID).ToList();
            List<Guid> newIds = users.Where(u => !cIds.Contains(u.ID)).Select(u => u.ID).ToList();
            Dbs.UserGroups.Insert(newIds.Select(u => new UserGroup() { ID = Guid.NewGuid(), GroupID = groupId, UserID = u }));
        }
    }
}