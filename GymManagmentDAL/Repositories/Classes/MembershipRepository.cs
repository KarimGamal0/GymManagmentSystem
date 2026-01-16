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
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly GymDBContext _dBContext;

        public MembershipRepository(GymDBContext dBContext) : base(dBContext) 
        {
            _dBContext = dBContext;
        }

        public IEnumerable<Membership> GetAllMembershipWithMemberAndPlan()
        {
            return _dBContext.Memberships.Include(x => x.Member).Include(x => x.Plan).ToList();
        }
    }
}
