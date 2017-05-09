using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SlavStore.Data.Interfaces;

namespace SlavStore.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly IDbContext context;
        private IDbSet<T> entities;

        public Repository(IDbContext context)
        {
            this.context = context;
        }

        protected IDbSet<T> Entities
        {
            get
            {
                if (this.entities == null)
                    this.entities = this.context.Set<T>();

                return this.entities;
            }
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var entity in entities)
                this.Entities.Remove(entity);

            this.context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.Entities.Remove(entity);

            this.context.SaveChanges();
        }

        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException();

            foreach (var entity in entities)
                this.Entities.Add(entity);

            this.context.SaveChanges();
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            this.Entities.Add(entity);

            this.context.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException();

            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            this.context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return this.Entities;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Where(predicate);
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.Any(predicate);
        }

        public bool Exists()
        {
            return this.Entities.Any();
        }

        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.FirstOrDefault(predicate);
        }

        public T GetFirstOrNull(Expression<Func<T, bool>> predicate)
        {
            return this.Entities.FirstOrDefault(predicate);
        }
    }
}
