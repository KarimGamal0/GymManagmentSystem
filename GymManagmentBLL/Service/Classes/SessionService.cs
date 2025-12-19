using GymManagmentBLL.Service.Interfaces;
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
    internal class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            //var sessions = _unitOfWork.GetRepository<Session>().GetAll();
            var sessions = _unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any()) return [];

            return sessions.Select(s => new SessionViewModel
            {
                Id = s.Id,
                Description = s.Description,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capcity,
                TrainerName = s.SessionTrainer.Name,
                CategoryName = s.SessionCategory.CategoryName.ToString(),
                AvailableSlot = s.Capcity - _unitOfWork.SessionRepository.GetCountOfBookedSlot(s.Id)
            });
        }
    }
}


