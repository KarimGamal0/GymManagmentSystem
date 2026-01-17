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
    public class MemberSessionRepository : GenericRepository<MemberSession>, IMemberSessionRepository
    {
        private readonly GymDBContext _dBContext;

        public MemberSessionRepository(GymDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }

        public IEnumerable<MemberSession> GetSessionMembers(int sessionId)
        {
            return _dBContext.MemberSessions.Include(x=> x.Session).Include(x=> x.Member).Where(x=> x.SessionId == sessionId).ToList();
        }
    }
}

