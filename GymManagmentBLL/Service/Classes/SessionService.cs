using AutoMapper;
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
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {

            try
            {
                //Check if Trainer is Exist
                if (!IsTrainerExist(createSession.TrainerId)) return false;
                //Check if Category is Exist
                if (!IsCategoryExist(createSession.CategoryId)) return false;
                //Check if StartDate is Before EndDate
                if (!IsDateTimeValid(createSession.StartDate, createSession.EndDate)) return false;


                if (createSession.Capcity > 25 || createSession.Capcity < 0) return false;

                var SessionEntity = _mapper.Map<Session>(createSession);

                _unitOfWork.GetRepository<Session>().Add(SessionEntity);
                return _unitOfWork.SaveChange() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Faild {ex}");
                return false;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            //var sessions = _unitOfWork.GetRepository<Session>().GetAll();
            var sessions = _unitOfWork.SessionRepository.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any()) return [];

            #region OldWay Mapping
            //return sessions.Select(s => new SessionViewModel
            //{
            //    Id = s.Id,
            //    Description = s.Description,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    Capacity = s.Capcity,
            //    TrainerName = s.SessionTrainer.Name,
            //    CategoryName = s.SessionCategory.CategoryName.ToString(),
            //    AvailableSlot = s.Capcity - _unitOfWork.SessionRepository.GetCountOfBookedSlot(s.Id)
            //});
            #endregion

            var MappedSession = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in MappedSession)
            {
                session.AvailableSlot = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlot(session.Id);
            }

            return MappedSession;
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);
            if (session == null) return null;

            #region oldWay Mapping
            //return new SessionViewModel()
            //{
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    TrainerName = session.SessionTrainer.Name,
            //    CategoryName = session.SessionCategory.CategoryName.ToString(),
            //    AvailableSlot = session.Capcity - _unitOfWork.SessionRepository.GetCountOfBookedSlot(session.Id)
            //};
            #endregion

            var MappedSession = _mapper.Map<Session, SessionViewModel>(session);
            MappedSession.AvailableSlot = session.Capcity - _unitOfWork.SessionRepository.GetCountOfBookedSlot(MappedSession.Id);

            return MappedSession;
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.SessionRepository.GetById(sessionId);
            if(!IsSessionAvailableToUpdate(session!)) return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel updateSession)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);
                if(!IsSessionAvailableToUpdate(session!)) return false;
                if(!IsTrainerExist(updateSession.TrainerId)) return false;

                if(!IsDateTimeValid(updateSession.StartDate,updateSession.EndDate)) return false;

                _mapper.Map(updateSession, session);
                session!.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Session>().Update(session);
                return _unitOfWork.SaveChange() > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"update session failed {ex}");
                return false;
            }
        }

        public bool RemoveSession(int sessionId)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(sessionId);
                if(!IsSessionAvailableForRemove(session!)) return false ;
                _unitOfWork.SessionRepository.Delete(session!);
                return _unitOfWork.SaveChange() > 0;
            }
            catch (Exception ex) {
                Console.WriteLine($"Remove Session Failed {ex}");
                return false;
            }
        }
        #region Helper

        private bool IsTrainerExist(int trainerId)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        }

        private bool IsCategoryExist(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }

        private bool IsDateTimeValid(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }



        private bool IsSessionAvailableToUpdate(Session session)
        {
            if (session is not null) return false;
            // if session compeleted
            if(session.EndDate < DateTime.Now) return false;
            //if session started
            if(session.StartDate <= DateTime.Now) return false;

            //if session have active booking
            var HasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlot(session.Id) > 0;

            if (HasActiveBooking) return false;
            return true;
        }

        private bool IsSessionAvailableForRemove(Session session)
        {
            if(session is not null) return false;
            //if session Started
            if (session.StartDate <= DateTime.Now && session.EndDate <= DateTime.Now) return false;
            //if session is upcoming
            if (session.StartDate > DateTime.Now) return false;
            //if session have active booking
            var hasActiveBooking = _unitOfWork.SessionRepository.GetCountOfBookedSlot(session.Id) > 0;
            if(hasActiveBooking) return false;

            return true;
        }

        public IEnumerable<TrainerSelectViewModel> GetTrainerForDropDown()
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll();
            return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainer);
        }

        public IEnumerable<CategorySelectViewModel> GetCategoryForDropDown()
        {
            var category = _unitOfWork.GetRepository<Category>().GetAll();
            return _mapper.Map<IEnumerable<CategorySelectViewModel>>(category);
        }






        #endregion
    }
}


