using System;
using System.Collections.Generic;
using Fusion.Face.WebAdmin.Models;

namespace Fusion.Face.WebAdmin.Biz
{
    public interface IGroupBiz : IBaseBiz
    {
        GroupInfo Find(Guid id);
        List<PermissionInfo> GetPermissions(Guid groupId);
        List<UserInfo> GetUsers(Guid groupId);
        SearchResult Search(SearchInfo searchInfo);
        void Update(GroupInfo groupInfo);
        void UpdatePermission(Guid groupId, List<PermissionInfo> permissions);
        void UpdateUsers(Guid groupId, List<UserInfo> users);
    }
}