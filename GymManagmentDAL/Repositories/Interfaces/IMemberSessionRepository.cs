using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IMemberSessionRepository
    {
        public IEnumerable<MemberSession> GetSessionMembers(int sessionId);

    }
}
