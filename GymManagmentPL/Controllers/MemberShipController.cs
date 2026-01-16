using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberShipViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipService _membershipService;

        public MemberShipController(IMemberShipService membershipService)
        {
            _membershipService = membershipService;
        }

        public ActionResult Index()
        {
            var Memberships = _membershipService.GetAllMembership();
            return View(Memberships);
        }

        public ActionResult Create()
        {
            LoadDropDownForMembers();
            LoadDropDownForPlans();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateMemberShipViewModel createMembership)
        {
            if (!ModelState.IsValid)
            {
                return View(createMembership);
            }

            var result = _membershipService.CreateMembership(createMembership);

            if (result)
            {
                TempData["SuccessMessage"] = "Membership created successfully";
                return View(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Membership failed to create";
                return View(createMembership);
            }
        }

        private void LoadDropDownForMembers()
        {
            var members = _membershipService.GetMemberForDropDown();
            ViewBag.Members = new SelectList(members, "Id", "Name");
        }

        private void LoadDropDownForPlans()
        {
            var plans = _membershipService.GetPlanForDropDown();
            ViewBag.Plans = new SelectList(plans, "Id", "Name");
        }
    }
}
