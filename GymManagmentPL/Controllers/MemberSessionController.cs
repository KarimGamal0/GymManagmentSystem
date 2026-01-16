using GymManagmentBLL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public MemberSessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        public ActionResult GetMembersForUpcomingSession()
        {
            return View();
        }



    }
}
