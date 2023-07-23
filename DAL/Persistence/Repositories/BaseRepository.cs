using DAL.Domain;
using DAL.Helpers.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace DAL.Persistence.Repositories
{
    #region Interface
    public interface IBaseRepository<T>
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        void Remove(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);
        IQueryable<T> Include(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeExpressions);
        Task<IEnumerable<T>> ExcuteQuerys(string query, params object[] parameters);
        public Task<dynamic> ExcuteQuery(string query, params object[] parameters);
        Task<IPagedList<T>> GetPagedList(
           PaginationParameters paginationParameters,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
           );
    }
    #endregion
    #region Implementation
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected InventoryContext _context { get; set; }
        protected DbSet<T> _DbSet { get; set; }

        public BaseRepository(InventoryContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _DbSet.AddAsync(entity);
        }

        public async Task<dynamic> ExcuteQuery(string query, params object[] parameters)
        {
            return await _DbSet.FromSqlRaw(query, parameters).ToListAsync();
        }

        public async Task<IEnumerable<T>> ExcuteQuerys(string query, params object[] parameters)
        {
            return await _DbSet.FromSqlRaw(query, parameters).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _DbSet.AsNoTracking().ToListAsync();
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> expression)
        {
            return _DbSet.Where(expression).AsNoTracking().AsQueryable();
        }

        public async Task<IPagedList<T>> GetPagedList(PaginationParameters paginationParameters, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _DbSet;


            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking()
                .ToPagedListAsync(paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public IQueryable<T> Include(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeExpressions)
        {
            DbSet<T> dbSet = _DbSet;
            includeExpressions.ToList().ForEach(x => dbSet.Include(x).Load());
            return dbSet;
        }

        public void Remove(T entity)
        {
            _DbSet.Remove(entity);
        }

        public  async Task UpdateAsync(T entity)
        {
             _DbSet.Attach(entity);
             _context.Entry(entity).State = EntityState.Modified;
        }
    }
    #endregion
}
