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
    internal class SessionRepository : GenericRepository<Session>
    {
        private readonly GymDBContext _dbContext;

        public SessionRepository(GymDBContext dBContext) : base(dBContext)
        {

        }

        #region old-way
        //public int Add(Session session)
        //{
        //    _dbContext.Sessions.Add(session);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Session session)
        //{
        //    _dbContext.Sessions.Remove(session);
        //    return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Session> GetAll() => _dbContext.Sessions.ToList();

        //public Session? GetById(int id) => _dbContext.Sessions.Find(id);

        //public int Update(Session session)
        //{
        //    _dbContext.Sessions.Update(session);
        //    return _dbContext.SaveChanges();
        //}
        #endregion
    }
}
