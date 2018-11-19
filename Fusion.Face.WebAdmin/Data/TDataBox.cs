using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Fusion.Face.WebAdmin.Data
{
    public class TDataBox<T> : IDataBox<T> where T : class
    {
        internal FusionFaceDb context;
        internal DbSet<T> dbSet;

        public TDataBox(FusionFaceDb context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        /// <summary>
        /// Get Entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(object id)
        {
            return this.dbSet.Find(id);
        }


        /// <summary>
        /// Insert List of Entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Insert(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.dbSet.Add(entity);
            }
        }

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(T entity)
        {
            this.dbSet.Add(entity);
        }

        /// <summary>
        /// Delete Entity by ID
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            T entity = this.dbSet.Find(id); 
            this.Delete(entity);
        }

        /// <summary>
        /// Delete List of Entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Delete an Entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Update an Entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Return a Queryable for LINQ expression 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Queryable()
        {
            return this.dbSet;
        }
    }
}