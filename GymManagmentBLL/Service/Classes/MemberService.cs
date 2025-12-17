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
        private readonly IGenericRepository<Member> _memeberRepository;
        private readonly IGenericRepository<Membership> _membershipRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepoistory;

        public MemberService(IGenericRepository<Member> memeberRepository,
            IGenericRepository<Membership> membershipRepository,
            IPlanRepository planRepository,
            IGenericRepository<HealthRecord> healthRecordRepoistory)
        {
            _memeberRepository = memeberRepository;
            _membershipRepository = membershipRepository;
            _planRepository = planRepository;
            _healthRecordRepoistory = healthRecordRepoistory;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try
            {
                //check if email is exist
                var emailExist = _memeberRepository.GetAll(x => x.Email == createMember.Email).Any();
                //check if phone is exist
                var phoneExist = _memeberRepository.GetAll(x => x.Phone == createMember.Phone).Any();

                //if one of them is exist return false

                if (emailExist || phoneExist) return false;

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
                return _memeberRepository.Add(member) > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModels> GetAllMembers()
        {
            var Members = _memeberRepository.GetAll();
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
            var member = _memeberRepository.GetById(MemberId);
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
            var ActiveMembership = _membershipRepository.GetAll(X => X.MemberId == MemberId && X.Status == "Active")
                .FirstOrDefault();

            if (ActiveMembership is not null)
            {
                viewModel.MembershipStartDate = ActiveMembership.CreatedAt.ToShortDateString();
                viewModel.MembershipEndDate = ActiveMembership.EndDate.ToShortDateString();
                var plan = _planRepository.GetById(ActiveMembership.PlanId);
                viewModel.PlanName = plan?.Name;
            }
            return viewModel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
           var MemberHealthRecord = _healthRecordRepoistory.GetById(MemberId);
            if(MemberHealthRecord == null) return null;
            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Note,
            };

        }
    }
}
