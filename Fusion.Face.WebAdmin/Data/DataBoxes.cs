using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Data
{
    public class DataBoxes : IDisposable, IDataBoxes
    {
        private FusionFaceDb context = new FusionFaceDb();
        private bool disposed = false;

        private IDataBox<UserMaster> userDataBox;
        private IDataBox<GroupMaster> groupDataBox;
        private IDataBox<UserGroup> userGroupDataBox;
        private IDataBox<Permission> permissionDataBox;
        private IDataBox<ModuleMaster> moduleDataBox;
        private IDataBox<FunctionMaster> functionDataBox;

        public IDataBox<UserMaster> Users {
            get {
                if (userDataBox == null)
                {
                    this.userDataBox = new TDataBox<UserMaster>(this.context);
                }

                return this.userDataBox;
            }
        }

        public IDataBox<GroupMaster> Groups
        {
            get
            {
                if (groupDataBox == null)
                {
                    this.groupDataBox = new TDataBox<GroupMaster>(this.context);
                }

                return this.groupDataBox;
            }
        }

        public IDataBox<UserGroup> UserGroups
        {
            get
            {
                if (userGroupDataBox == null)
                {
                    this.userGroupDataBox = new TDataBox<UserGroup>(this.context);
                }

                return this.userGroupDataBox;
            }
        }

        public IDataBox<Permission> Permissions
        {
            get
            {
                if (permissionDataBox == null)
                {
                    this.permissionDataBox = new TDataBox<Permission>(this.context);
                }

                return this.permissionDataBox;
            }
        }

        public IDataBox<ModuleMaster> Modules
        {
            get
            {
                if (moduleDataBox == null)
                {
                    this.moduleDataBox = new TDataBox<ModuleMaster>(this.context);
                }

                return this.moduleDataBox;
            }
        }

        public IDataBox<FunctionMaster> Functions
        {
            get
            {
                if (functionDataBox == null)
                {
                    this.functionDataBox = new TDataBox<FunctionMaster>(this.context);
                }

                return this.functionDataBox;
            }
        }

        public virtual IEnumerable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(query, parameters).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}