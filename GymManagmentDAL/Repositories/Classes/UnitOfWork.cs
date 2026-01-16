using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDBContext _dbContext;

        public UnitOfWork(GymDBContext dbContext, ISessionRepository sessionRepository, IMembershipRepository membershipRepository
            ,IMemberSessionRepository memberSessionRepository)
        {
            _dbContext = dbContext;
            SessionRepository = sessionRepository;
            MembershipRepository = membershipRepository;
            MemberSessionRepository = memberSessionRepository;

        }

        public ISessionRepository SessionRepository { get; }

        public IMembershipRepository MembershipRepository { get; }

        public IMemberSessionRepository MemberSessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out var repo))
                return (IGenericRepository<TEntity>)repo;
            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;
        }

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }
    }
}
