using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Member:GymUser
    {

        //Join Date = Created At of base entity
        public string? Photo { get; set; }

        #region Healthrecord
        public HealthRecord HealthRecord { get; set; } = null!;
        #endregion

        #region Member-Membership
        public ICollection<Membership> Memberships { get; set; } = null!;
        #endregion

        #region Member-MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
        #endregion
    }
}
