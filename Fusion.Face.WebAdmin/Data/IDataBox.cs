using System.Collections.Generic;
using System.Linq;

namespace Fusion.Face.WebAdmin.Data
{
    public interface IDataBox<T> where T : class
    {
        void Delete(object id);
        void Delete(T entity);
        T Get(object id);
        void Insert(IEnumerable<T> entities);
        void Insert(T entity);
        IQueryable<T> Queryable();
        void Delete(IEnumerable<T> entities);
        void Update(T entity);
    }
}