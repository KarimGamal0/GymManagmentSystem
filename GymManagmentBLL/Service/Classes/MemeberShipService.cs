using AutoMapper;
using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.SessionViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GymManagmentBLL.Service.Classes
{
    public class MemeberShipService : IMemberShipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemeberShipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<MemberShipViewModel> GetAllMembership()
        {
            var memberships = _unitOfWork.MembershipRepository.GetAllMembershipWithMemberAndPlan();
            if (!memberships.Any()) return [];

            var MappedMemberShip = _mapper.Map<IEnumerable<Membership>, IEnumerable<MemberShipViewModel>>(memberships);

            return MappedMemberShip;
        }

        public bool CreateMembership(CreateMemberShipViewModel createMembership)
        {
            try
            {
                //check if member Exist
                if (!isMemberExist(createMembership.MemberId)) return false;
                //check if PlanExist;
                if (!isPlanExist(createMembership.PlanId)) return false;

                var membershipEntity = _mapper.Map<CreateMemberShipViewModel, Membership>(createMembership);

                _unitOfWork.GetRepository<Membership>().Add(membershipEntity);
                return _unitOfWork.SaveChange() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create Membership Failed");
                return false;
            }
        }

        

        public bool RemoveMembership(int membershipId)
        {
            var membershipRepo = _unitOfWork.GetRepository<Membership>();
            var Membership = membershipRepo.GetById(membershipId);
            if (Membership != null)
                membershipRepo.Delete(Membership);

            return _unitOfWork.SaveChange() > 0;


        }

        public IEnumerable<MemberSelectViewModel> GetMemberForDropDown()
        {
            var Member = _unitOfWork.GetRepository<Member>().GetAll();
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberSelectViewModel>>(Member);
        }

        public IEnumerable<PlanSelectViewModel> GetPlanForDropDown()
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetAll();
            return _mapper.Map<IEnumerable<Plan>, IEnumerable<PlanSelectViewModel>>(plan);
        }


        #region Helper-Methods
        private bool isMemberExist(int memberId)
        {
            return _unitOfWork.GetRepository<Member>().GetById(memberId) is not null;
        }

        private bool isPlanExist(int planId)
        {
            return _unitOfWork.GetRepository<Plan>().GetById(planId) is not null;
        }
        #endregion
    }
}
