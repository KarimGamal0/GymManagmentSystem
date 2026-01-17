using GymManagmentBLL.Service.Classes;
using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberSessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly IMemberSessionService _memberSessionService;

        public MemberSessionController(ISessionService sessionService, IMemberSessionService memberSessionService)
        {
            _sessionService = sessionService;
            _memberSessionService = memberSessionService;
        }
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }


        public ActionResult GetMembersForUpcomingSession(int id)
        {
            ViewBag.SessionId = id;

            var sessions = _memberSessionService.GetMembersOfSession(id);

            return View(sessions);
        }

        public ActionResult GetMembersForOngoingSession(int id)
        {
            ViewBag.SessionId = id;

            var sessions = _memberSessionService.GetMembersOfSession(id);

            return View(sessions);
        }

        public ActionResult Create([FromRoute] int id)
        {
            LoadDropDownForMembers();
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int id, CreateMemberSessionViewModel createMemberSession)
        {
            if (!ModelState.IsValid)
            {
                return View(createMemberSession);
            }

            var result = _memberSessionService.CreateMemberSession(id, createMemberSession);

            if (result)
            {
                TempData["SuccessMessage"] = "MemberSession created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "MemberSession failed to create";
                return View(createMemberSession);
            }
        }

        private void LoadDropDownForMembers()
        {
            var members = _memberSessionService.GetMemberForDropDown();
            ViewBag.Members = new SelectList(members, "Id", "Name");
        }



    }
}
