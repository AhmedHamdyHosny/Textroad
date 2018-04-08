using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static GenericApiController.Utilities.GenericDataFormat;

namespace GenericApiController.Utilities
{
    public partial class Repository<TEntity> where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
            //context.Database.CommandTimeout = 180;
        }

        public Repository() {
        }

        // The code Expression<Func<TEntity, bool>> filter means 
        //the caller will provide a lambda expression based on the TEntity type,
        //and this expression will return a Boolean value.
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            
            if(!string.IsNullOrEmpty(includeProperties))
            {
                // applies the eager-loading expressions after parsing the comma-delimited list
                foreach (var includeProperty in includeProperties.Split
                    (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            

            return orderBy != null
                ? orderBy(query)
                : query;

        }

        public virtual dynamic GetWithOptions(
            IEnumerable<string> includeProperties = null,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<SortItems> thenByOrders = null,
            string includeReferences = "",
            int? pageNumber = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (!string.IsNullOrEmpty(includeReferences))
            {
                // applies the eager-loading expressions after parsing the comma-delimited list
                foreach (var includeProperty in includeReferences.Split
                    (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            query = orderBy != null ? orderBy(query) : query;

            if (thenByOrders != null )
            {
                var orderdQuery = ((IOrderedQueryable<TEntity>)query);
                foreach (var thenByItem in thenByOrders)
                {
                    orderdQuery = thenByItem.SortType == SortType.Asc ?
                        orderdQuery.ThenBy(GetSelector(thenByItem.Property)) :
                        orderdQuery.ThenByDescending(GetSelector(thenByItem.Property));
                }
                query = orderdQuery;
            }

            if(orderBy != null && pageNumber != null && pageSize != null)
            {
                int? skpItmsCount = (pageNumber - 1) * pageSize;
                query = query.Skip((int)skpItmsCount).Take((int)pageSize);
            }

            if(includeProperties != null)
            {
                return query.SelectProperties(includeProperties).ToList<object>();
            }

            return query;
        }
        public virtual dynamic GetReferenceForImport(
            IEnumerable<string> includeProperties = null,
            dynamic filter = null)
        {
            var result = GetWithOptions(includeProperties: includeProperties, filter: (Expression<Func<TEntity, bool>>)filter);
            if(!(result is List<object>))
            {
                return ((IQueryable<TEntity>)result).ToList<TEntity>();
            }
            return result;
        }
        public virtual async Task<ICollection<TResult>> GetAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            if (selector == null)
                throw new ArgumentNullException("selector");
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // applies the eager-loading expressions after parsing the comma-delimited list

            return await (orderBy != null
                ? orderBy(query).Select(selector).ToListAsync()
                : query.Select(selector).ToListAsync());
        }
        public virtual TEntity GetByID(Guid id, Expression<Func<TEntity, bool>> filter = null)
        {
            var item = DbSet.Find(id);
            if (filter != null && item != null)
            {
                List<TEntity> items = new List<TEntity>();
                items.Add(item);
                item = items.AsQueryable<TEntity>().Where(filter).SingleOrDefault();
            }
            return item;
        }
        public virtual async Task<TEntity> GetByAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual TEntity Insert(TEntity entity)
        {
            return DbSet.Add(entity);
        }
        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRange(entities);
        }
        public bool BulkInsert(List<TEntity> entities, int batchSize)
        {
            try
            {
                int count = 0;
                do
                {
                    var items = entities.Skip(count).Take(batchSize);
                    count += batchSize;
                    // This is optional
                    _context.Configuration.AutoDetectChangesEnabled = false;
                    _context.Set<TEntity>().AddRange(items);
                    _context.SaveChanges();
                    System.Data.Entity.Core.Objects.ObjectContext objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_context).ObjectContext;
                    //_context.Dispose();
                    _context = new DbContext(objectContext, false);

                } while (count < entities.Count);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            return true;
        }

        public virtual void Delete(Guid id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public virtual void Detach(TEntity entityToUpdate)
        {
            _context.Entry(entityToUpdate).State = EntityState.Detached;
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Update(TEntity entityToUpdate, List<string> excluded)
        {
            DbSet.Attach(entityToUpdate);
            var entry = _context.Entry(entityToUpdate);
            entry.State = EntityState.Modified;

            if (excluded != null)
            {
                foreach (var name in excluded)
                {
                    entry.Property(name).IsModified = false;
                }
            }
        }
        public virtual void DeleteRange(IQueryable<TEntity> entitiesToDelete)
        {
            foreach (var entity in entitiesToDelete)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }
            DbSet.RemoveRange(entitiesToDelete.AsEnumerable());
        }
        public virtual IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters) as IQueryable<TEntity>; //DbSet.SqlQuery to connect directly to the database
        }

        public virtual int ExceuteSql(string query)
        {
            return _context.Database.ExecuteSqlCommand(query); 
        }
    }
}