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
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            try
            {
                if (IsEmailExist(createTrainer.Email) || IsPhoneExist(createTrainer.Phone)) return false;

                var trainer = new Trainer()
                {
                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    Gender = createTrainer.Gender,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Address = new Address()
                    {
                        City = createTrainer.City,
                        Street = createTrainer.Street,
                        BuildingNo = createTrainer.BuildingNumber
                    }

                };
                _unitOfWork.GetRepository<Trainer>().Add(trainer);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMember(int trainerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var Trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (Trainers == null || !Trainers.Any())
                return [];

            var TrainerViewModel = Trainers.Select(trainer => new TrainerViewModel()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString()
            });

            return TrainerViewModel;

        }

        public TrainerViewModel? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer == null) return null;

            var viewModel = new TrainerViewModel()
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender.ToString()
            };

            return viewModel;
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId)
        {
            //var member = _memeberRepository.GetById(MemberId);
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            if (trainer == null) return null;

            return new TrainerToUpdateViewModel()
            {
                Email = trainer.Email,
                Name = trainer.Name,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address.BuildingNo,
                Street = trainer.Address.Street,
                City = trainer.Address.City
            };
        }

        public bool UpdateMember(int trainerId, TrainerToUpdateViewModel trainerToUpdate)
        {
            try
            {

                if (IsEmailExist(trainerToUpdate.Email) || IsPhoneExist(trainerToUpdate.Phone)) return false;

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
