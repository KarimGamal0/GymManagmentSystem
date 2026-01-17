using AutoMapper;
using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class MemberSessionService : IMemberSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateMemberSession(int id, CreateMemberSessionViewModel createMemberSession)
        {
            try
            {
                if (!isMemberExist(createMemberSession.MemberId)) return false;


                var MemberSessionEntity = new MemberSession()
                {
                    SessionId = id,
                    MemberId = createMemberSession.MemberId
                };

                _unitOfWork.GetRepository<MemberSession>().Add(MemberSessionEntity);
                return _unitOfWork.SaveChange() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create MemberSession Failed");
                return false;
            }
        }

        public IEnumerable<MemberSelectViewModel> GetMemberForDropDown()
        {
            var Member = _unitOfWork.GetRepository<Member>().GetAll();
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberSelectViewModel>>(Member);
        }


        public IEnumerable<MemberSessionViewModel> GetMembersOfSession(int id)
        {
            var sessions = _unitOfWork.MemberSessionRepository.GetSessionMembers(id);

            return _mapper.Map<IEnumerable<MemberSession>, IEnumerable<MemberSessionViewModel>>(sessions);

        }

        private bool isMemberExist(int memberId)
        {
            return _unitOfWork.GetRepository<Member>().GetById(memberId) is not null;
        }

    }
}
