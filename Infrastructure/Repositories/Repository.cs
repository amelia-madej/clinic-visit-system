using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public IList<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Set<TEntity>().Where(expression).ToList();
        }
        public void Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
    }
}
