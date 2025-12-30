using AutoMapper;
using AutoMapper.Execution;
using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers == null || !trainers.Any())
                return [];

            #region old-mapping
            //var TrainerViewModel = Trainers.Select(trainer => new TrainerViewModel()
            //{
            //    Id = trainer.Id,
            //    Name = trainer.Name,
            //    Email = trainer.Email,
            //    Phone = trainer.Phone,
            //    Gender = trainer.Gender.ToString(),
            //    Speciality = trainer.Speciality,
            //});
            #endregion

            return _mapper.Map<IEnumerable<Trainer>, IEnumerable<TrainerViewModel>>(trainers);
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone)) return false;

                #region Old-Mapping
                //var trainer = new Trainer()
                //{
                //    Name = createTrainer.Name,
                //    Email = createTrainer.Email,
                //    Phone = createTrainer.Phone,
                //    Gender = createTrainer.Gender,
                //    DateOfBirth = createTrainer.DateOfBirth,
                //    Address = new Address()
                //    {
                //        City = createTrainer.City,
                //        Street = createTrainer.Street,
                //        BuildingNo = createTrainer.BuildingNumber
                //    },
                //    Speciality = createTrainer.Speciality,

                //};
                #endregion

                var trainer = _mapper.Map<CreateTrainerViewModel,Trainer>(createTrainer);

                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }

        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer == null) return null;

            #region Old-Mapping
            //var viewModel = new TrainerViewModel()
            //{
            //    Id = trainer.Id,
            //    Name = trainer.Name,
            //    Email = trainer.Email,
            //    Phone = trainer.Phone,
            //    Gender = trainer.Gender.ToString(),
            //    Speciality = trainer.Speciality,
            //    DateOfBirth = trainer.DateOfBirth.ToString(),
            //    Address = $"{trainer.Address.City} - {trainer.Address.Street} - {trainer.Address.BuildingNo}"
            //};
            #endregion

            return _mapper.Map<Trainer, TrainerViewModel>(trainer);
        }

        public bool DeleteTrainer(int trainerId)
        {
            var TrainerRepo = _unitOfWork.GetRepository<Trainer>();

            var trainer = TrainerRepo.GetById(trainerId);

            TrainerRepo.Delete(trainer);
            return _unitOfWork.SaveChange() > 0;
        }

        

        

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            //var member = _memeberRepository.GetById(MemberId);
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer == null) return null;

            #region old-mapping
            //return new TrainerToUpdateViewModel()
            //{
            //    Email = trainer.Email,
            //    Name = trainer.Name,
            //    Phone = trainer.Phone,
            //    BuildingNumber = trainer.Address.BuildingNo,
            //    Street = trainer.Address.Street,
            //    City = trainer.Address.City
            //};
            #endregion

            return _mapper.Map<Trainer,TrainerToUpdateViewModel>(trainer);
        }

        public bool UpdateTrainer(int trainerId, TrainerToUpdateViewModel trainerToUpdate)
        {
            try
            {

                var phoneExist = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == trainerToUpdate.Phone && x.Id != trainerId);
                var emailExist = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == trainerToUpdate.Email && x.Id != trainerId);

                var TrainerRepo = _unitOfWork.GetRepository<Trainer>();

                var Trainer = TrainerRepo.GetById(trainerId);
                if (Trainer == null) return false;

                Trainer.Email = trainerToUpdate.Email;
                Trainer.Phone = trainerToUpdate.Phone;
                Trainer.Address.City = trainerToUpdate.City;
                Trainer.Address.Street = trainerToUpdate.Street;
                Trainer.Address.BuildingNo = trainerToUpdate.BuildingNumber;
                Trainer.UpdatedAt = DateTime.Now;

                TrainerRepo.Update(Trainer);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }

        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        }

        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
        }
    }
}
