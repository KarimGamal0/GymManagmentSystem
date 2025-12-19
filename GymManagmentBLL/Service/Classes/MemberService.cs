using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        #region old-way before unitofwork
        //private readonly IGenericRepository<Member> _memeberRepository;
        //private readonly IGenericRepository<Membership> _membershipRepository;
        //private readonly IPlanRepository _planRepository;
        //private readonly IGenericRepository<HealthRecord> _healthRecordRepoistory;
        //private readonly IGenericRepository<MemberSession> _memberSessionRepository;

        //public MemberService(IGenericRepository<Member> memeberRepository,
        //    IGenericRepository<Membership> membershipRepository,
        //    IPlanRepository planRepository,
        //    IGenericRepository<HealthRecord> healthRecordRepoistory,
        //    IGenericRepository<MemberSession> memberSessionRepository)
        //{
        //    _memeberRepository = memeberRepository;
        //    _membershipRepository = membershipRepository;
        //    _planRepository = planRepository;
        //    _healthRecordRepoistory = healthRecordRepoistory;
        //    _memberSessionRepository = memberSessionRepository;
        //}
        #endregion

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                if (IsEmailExist(createMember.Email) || IsPhoneExist(createMember.Phone)) return false;

                var member = new Member()
                {
                    Name = createMember.Name,
                    Email = createMember.Email,
                    Phone = createMember.Phone,
                    Gender = createMember.Gender,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address()
                    {
                        City = createMember.City,
                        Street = createMember.Street,
                        BuildingNo = createMember.BuildingNumber
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createMember.HealthRecord.Height,
                        Weight = createMember.HealthRecord.Weight,
                        BloodType = createMember.HealthRecord.BloodType,
                        Note = createMember.HealthRecord.Note
                    }

                };
                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModels> GetAllMembers()
        {
            //var Members = _memeberRepository.GetAll();

            var Members = _unitOfWork.GetRepository<Member>().GetAll();
            if (Members is null || !Members.Any())
            {
                return [];
            }

            #region Way01
            //var MemberViewModel = new List<MemberViewModels>();

            //foreach (var Member in Members)
            //{
            //    var memberviewmodel = new MemberViewModels()
            //    {
            //        Id = Member.Id,
            //        Name = Member.Name,
            //        Email = Member.Email,
            //        Phone = Member.Phone,
            //        Photo = Member.Photo,
            //        Gender = Member.Gender.ToString()
            //    };

            //    MemberViewModel.Add(memberviewmodel);
            //}
            #endregion

            #region Way02
            var MemberViewModel = Members.Select(member => new MemberViewModels
            {
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Gender = member.Gender.ToString()
            });
            #endregion
            return MemberViewModel;
        }

        public MemberViewModels? GetMemberDetails(int MemberId)
        {
            //var member = _memeberRepository.GetById(MemberId);
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;

            var viewModel = new MemberViewModels()
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNo} , {member.Address.Street} , {member.Address.City}",
                Photo = member.Photo
            };

            //active Membership
            var ActiveMembership = _unitOfWork.GetRepository<Membership>().GetAll(X => X.MemberId == MemberId && X.Status == "Active")
                .FirstOrDefault();

            if (ActiveMembership is not null)
            {
                viewModel.MembershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                viewModel.MembershipEndDate = ActiveMembership.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(ActiveMembership.PlanId);
                viewModel.PlanName = plan?.Name;
            }
            return viewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            //var MemberHealthRecord = _healthRecordRepoistory.GetById(MemberId);
            var MemberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);
            if (MemberHealthRecord == null) return null;
            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Note,
            };

        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int MemberId)
        {
            //var member = _memeberRepository.GetById(MemberId);
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member == null) return null;

            return new MemberToUpdateViewModel()
            {
                Email = member.Email,
                Name = member.Name,
                Phone = member.Phone,
                Photo = member.Photo,
                BuildingNumber = member.Address.BuildingNo,
                Street = member.Address.Street,
                City = member.Address.City
            };
        }

        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {

            try
            {

                if (IsEmailExist(memberToUpdate.Email) || IsPhoneExist(memberToUpdate.Phone)) return false;

                var MemberRepo = _unitOfWork.GetRepository<Member>();

                var Member = MemberRepo.GetById(memberId);
                if (Member == null) return false;
                Member.Email = memberToUpdate.Email;
                Member.Phone = memberToUpdate.Phone;
                Member.Address.City = memberToUpdate.City;
                Member.Address.Street = memberToUpdate.Street;
                Member.Address.BuildingNo = memberToUpdate.BuildingNumber;
                Member.UpdatedAt = DateTime.Now;

                MemberRepo.Update(Member);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMember(int memberId)
        {
            var MemberRepo = _unitOfWork.GetRepository<Member>();

            var member = MemberRepo.GetById(memberId);
            var HasActiveMemberSession = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(x => x.MemberId == memberId && x.Session.StartDate > DateTime.Now).Any();
            if (HasActiveMemberSession) return false;
            var MemberShipRepo = _unitOfWork.GetRepository<Membership>();
            var Membership = MemberShipRepo.GetAll(x => x.MemberId == memberId);

            try
            {
                if (Membership.Any())
                {
                    foreach (var membership in Membership)
                    {
                        MemberShipRepo.Delete(membership);
                    }
                }
                MemberRepo.Delete(member);
                return _unitOfWork.SaveChange() > 0;
            }
            catch { return false; }
        }

        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }
    }
}
