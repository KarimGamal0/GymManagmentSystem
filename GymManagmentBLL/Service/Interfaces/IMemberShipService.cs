using GymManagmentBLL.ViewModels.MemberShipViewModel;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.SessionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    public interface IMemberShipService
    {

        public bool CreateMembership(CreateMemberShipViewModel createMemberShipViewModel);

        public IEnumerable<MemberShipViewModel> GetAllMembership();

        public IEnumerable<MemberSelectViewModel> GetMemberForDropDown();

        public IEnumerable<PlanSelectViewModel> GetPlanForDropDown();

        public bool RemoveMembership(int membershipId);
    }
}
