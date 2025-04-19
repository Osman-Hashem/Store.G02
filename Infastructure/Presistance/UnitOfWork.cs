using System.Collections.Concurrent;
using Domain.Contracts;
using Domain.Models;
using Presistance.Data;
using Presistance.Repositories;

namespace Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        #region MyRegion
        //public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_repositories.ContainsKey(type))
        //    {
        //        var repository = new GenericRepository<TEntity, TKey>(_context);
        //        _repositories.Add(type, repository);
        //    }
        //    return (IGenericRepository<TEntity, TKey>) _repositories[type];
        //} 
        #endregion

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            => (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name , new GenericRepository<TEntity , TKey>(_context));
        

        public async Task<int> SavesChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
