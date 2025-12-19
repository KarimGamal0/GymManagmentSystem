using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModel;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null || !plans.Any()) return [];

            return plans.Select(p => new PlanViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActive = p.IsActive,
            });
        }

        public PlanViewModel? GetPlanById(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan == null) return null;
            return new PlanViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Price = plan.Price,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                IsActive = plan.IsActive,
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null || HasActivemembership(planId)) return null;

            return new UpdatePlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                PlanName = plan.Name,
                Price = plan.Price
            };
        }

        public bool ToggleStatus(int planId)
        {
            var repo = _unitOfWork.GetRepository<Plan>();
            var plan = repo.GetById(planId);
            if (plan is null || HasActivemembership(planId)) return false;
            plan.IsActive = plan.IsActive == true? false:true;
            plan.UpdatedAt = DateTime.Now;
            try
            {
                repo.Update(plan);
                return _unitOfWork.SaveChange() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdatePlan(int planId, UpdatePlanViewModel planToUpdate)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null || HasActivemembership(planId)) return false;

            (plan.Description, plan.Price, plan.DurationDays, plan.Name) =
                (planToUpdate.Description, planToUpdate.Price, planToUpdate.DurationDays, planToUpdate.PlanName);

            _unitOfWork.GetRepository<Plan>().Update(plan);
            return _unitOfWork.SaveChange() > 0;

        }

        #region Helper
        private bool HasActivemembership(int planid)
        {
            var ActiveMemberShip = _unitOfWork.GetRepository<Membership>().GetAll(x => x.PlanId == planid && x.Status == "Active");

            return ActiveMemberShip.Any();
        }

        #endregion
    }
}
