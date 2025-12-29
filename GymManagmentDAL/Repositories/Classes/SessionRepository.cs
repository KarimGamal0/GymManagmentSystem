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
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDBContext _dbContext;

        public SessionRepository(GymDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Session> GetAllSessionWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer).Include(x => x.SessionCategory).ToList();
        }

        public int GetCountOfBookedSlot(int sessionId)
        {
            return _dbContext.MemberSessions.Where(x => x.SessionId == sessionId).Count();
        }

        public Session? GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(x => x.SessionTrainer).Include(x => x.SessionCategory).FirstOrDefault();
        }
    }
}
