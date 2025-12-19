using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    internal interface IMemberService
    {
        IEnumerable<MemberViewModels> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);

        MemberViewModels? GetMemberDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId);

        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);

        bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate);

        bool DeleteMember(int memberId);
    }
}
