using Fusion.Face.WebAdmin.Data;
using Fusion.Face.WebAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fusion.Face.WebAdmin.Biz
{
    public class SecurityBiz
    {
        /// <summary>
        /// To Validate Login and return UserProfile for Authorization
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public UserProfile Authenticate(LoginInfo loginInfo)
        {
            UserProfile result = null;

            if (!string.IsNullOrEmpty(loginInfo.Password)
                && !string.IsNullOrEmpty(loginInfo.Username))
            {

                string hashedPassword = Hash(loginInfo.Password);

                using (FusionFaceDb db = new FusionFaceDb())
                {
                    DateTime now = DateTime.Now;

                    var q = from u in db.UserMasters
                            where u.Username == loginInfo.Username
                            && u.Password == hashedPassword
                            && !u.IsDeleted
                            && (u.AccountBlockedExpiry == null || u.AccountBlockedExpiry > now)
                            select u;

                    if (q.Count() > 0)
                    {
                        UserMaster uMaster = q.First();



                        result = new UserProfile()
                        {
                            ID = uMaster.ID,
                            Username = uMaster.Username,
                            LastLogin = uMaster.LastLoginTime
                        };

                        //Record LastLoginTime
                        uMaster.LastLoginTime = DateTime.Now;
                        uMaster.PasswordTryCount = 0;

                        db.SaveChanges();

                        #region Permissions
                        var z = from ur in db.UserGroups
                                join p in db.Permissions on ur.GroupID equals p.GroupID
                                where ur.UserID == uMaster.ID
                                select p;

                        if (z.Count() > 0)
                        {
                            List<Permission> permissions = z.Distinct().ToList();

                            foreach (Permission p in permissions)
                            {
                                if (!result.Permissions.Contains(p.FunctionID))
                                {
                                    result.Permissions.Add(p.FunctionID);
                                }
                            }
                        }
                        #endregion

                        #region Menus
                        result.Menus.AddRange(GetMenuItemList(result.Permissions));
                        #endregion

                        #region Roles
                        var gr = from ur in db.UserGroups
                                 join g in db.GroupMasters on ur.GroupID equals g.ID
                                 where ur.UserID == uMaster.ID
                                 select g;

                        if (gr.Count() > 0)
                        {
                            List<GroupMaster> groups = gr.Distinct().ToList();

                            foreach (GroupMaster grp in groups)
                            {
                                if (!result.Roles.Contains(grp.Name))
                                {
                                    result.Roles.Add(grp.Name);
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {

                    }
                }
            }

            return result;
        }


        public List<MenuItem> GetMenuItemList(List<string> functionCodes)
        {
            List<MenuItem> result = new List<MenuItem>();

            if (functionCodes != null
                && functionCodes.Count() > 0)
            {
                using (FusionFaceDb db = new FusionFaceDb())
                {
                    var q = from m in db.MenuMasters
                            where (functionCodes.Contains(m.FunctionID)
                                || m.FunctionID == null)
                                && m.Enabled
                            orderby m.Position
                            select new MenuInfo()
                            {
                                ID = m.ID.ToString(),
                                Name = m.Name,
                                Description = m.Description,
                                FunctionID = m.FunctionID,
                                Icon = m.Icon,
                                ParentID = m.ParentID.HasValue ? m.ParentID.ToString() : "",
                                Position = m.Position,
                                Url = m.Url
                            };

                    List<MenuInfo> menus = q.ToList();

                    if (menus != null
                        && menus.Count() > 0)
                    {
                        List<MenuItem> newMenus =
                            menus.Where(m => m.ParentID == null || m.ParentID == "")
                            .Select(m => new MenuItem() { Info = m })
                            .OrderBy(m => m.Info.Position)
                            .ToList();

                        result.AddRange(newMenus);

                        while (newMenus.Count() > 0)
                        {
                            MenuItem currentItem = newMenus.First();
                            newMenus.Remove(currentItem);

                            currentItem.Children.AddRange(
                                    menus.Where(m => m.ParentID == currentItem.Info.ID)
                                    .Select(m => new MenuItem() { Info = m, Parent = currentItem })
                                    .OrderBy(m => m.Info.Position)
                                    .ToList()
                                );

                            newMenus.AddRange(currentItem.Children);
                        }
                    }
                }

                result.RemoveAll(m => m.Parent != null && !m.Children.Any());
            }

            return result;
        }

        /// <summary>
        /// MD5 Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
