using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDBContext _dbContext;

        public GenericRepository(GymDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region old-way before unitofwork
        //public int Add(TEntity entity)
        //{
        //    _dbContext.Set<TEntity>().Add(entity);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(TEntity entity)
        //{
        //    _dbContext.Set<TEntity>().Remove(entity);
        //    return _dbContext.SaveChanges();
        //}

        //public int Update(TEntity entity)
        //{
        //    _dbContext.Set<TEntity>().Update(entity);
        //    return _dbContext.SaveChanges();
        //}
        #endregion

        //public IEnumerable<TEntity> GetAll() => _dbContext.Set<TEntity>().ToList();

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
            {
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
            }
        }

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
