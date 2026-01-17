using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    public interface IMemberSessionService
    {
        //public IEnumerable<MemberSessionViewModel> GetAllMemberSessions();

        public IEnumerable<MemberSessionViewModel> GetMembersOfSession(int SessionId);

        public IEnumerable<MemberSelectViewModel> GetMemberForDropDown();

        public bool CreateMemberSession(int id, CreateMemberSessionViewModel createMemberSession);

    }
}
