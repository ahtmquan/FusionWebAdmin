using System.Collections.Generic;

namespace Fusion.Face.WebAdmin.Data
{
    public interface IDataBoxes
    {
        IDataBox<UserMaster> Users { get; }
        IDataBox<GroupMaster> Groups { get; }
        IDataBox<UserGroup> UserGroups { get; }
        IDataBox<Permission> Permissions { get; }
        IDataBox<ModuleMaster> Modules { get; }
        IDataBox<FunctionMaster> Functions { get; }

        IEnumerable<T> SqlQuery<T>(string query, params object[] parameters);

        void Dispose();
        void Save();
    }
}