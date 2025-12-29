using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        public ActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan id";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanById(id);

            if (plan == null)
            {
                TempData["ErrorMesssage"] = "plan not found";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);

        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan id";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanToUpdate(id);

            if (plan == null)
            {
                TempData["ErrorMesssage"] = "plan can not be updated";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdatePlanViewModel updatePlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Wrong Data", "check data validation");
                return View(updatePlan);
            }

            var result = _planService.UpdatePlan(id, updatePlan);

            if (result)
            {
                TempData["ErrorMessage"] = "plan updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "plan failed to update";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Activate(int id)
        {
            var result = _planService.ToggleStatus(id);

            if (result)
            {
                TempData["SuccessMessage"] = "plan status changed";
            }
            else
            {
                TempData["ErrorMessage"] = "failed to change plan status";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
