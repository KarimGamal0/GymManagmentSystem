using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.SessionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagmentPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid id";
                return View(nameof(Index));
            }

            var session = _sessionService.GetSessionById(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return View(nameof(Index));
            }

            return View(session);
        }

        public ActionResult Create()
        {
            LoadDropDownForCategories();
            LoadDropDownForTrainers();
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateSessionViewModel createSession)
        {
            if (!ModelState.IsValid)
            {
                return View(createSession);
            }

            var result = _sessionService.CreateSession(createSession);

            if (result)
            {
                TempData["SuccessMessage"] = "Session created sucessfully";
                return View(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session failed to create";
                LoadDropDownForCategories();
                LoadDropDownForTrainers();
                return View(createSession);
            }

        }

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionToUpdate(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "No session found";
                return RedirectToAction(nameof(Index));
            }

            LoadDropDownForTrainers();
            return View(session);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel updateSession)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid id";
            }

            var result = _sessionService.UpdateSession(id, updateSession);

            if (result)
            {
                TempData["SucessMessage"] = "Session updated sucessfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Session failed to update";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid id";
                return RedirectToAction(nameof(Index));
            }

            var session = _sessionService.GetSessionById(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = session.Id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            var result = _sessionService.RemoveSession(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Session deleted successfully";

            }
            else
            {
                TempData["ErrorMessage"] = "Session Failed to delete";
            }

            return RedirectToAction(nameof(Index));
        }
        private void LoadDropDownForCategories()
        {
            var categories = _sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private void LoadDropDownForTrainers()
        {
            var trainers = _sessionService.GetTrainerForDropDown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
    }
}
