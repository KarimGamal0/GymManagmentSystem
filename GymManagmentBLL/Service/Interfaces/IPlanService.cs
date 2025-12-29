using GymManagmentBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanViewModel? GetPlanById(int planId);
        UpdatePlanViewModel? GetPlanToUpdate(int planId);
        bool UpdatePlan(int planId, UpdatePlanViewModel planToUpdate);
        bool ToggleStatus(int planId);
    }
}
