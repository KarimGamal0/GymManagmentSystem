using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.AnalyticsViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Session = _unitOfWork.GetRepository<Session>().GetAll();

            return new AnalyticsViewModel()
            {
                ActiveMemebers = _unitOfWork.GetRepository<Membership>().GetAll(x => x.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainer = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = Session.Where(x => x.StartDate > DateTime.Now).Count(),
                OngoingSessions = Session.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).Count(),
                CompeleteSessions = Session.Where(x => x.EndDate < DateTime.Now).Count()
            };
        }
    }
}
