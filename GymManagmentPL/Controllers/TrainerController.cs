using GymManagmentBLL.Service.Classes;
using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "check data and Missing Field");
                return View(nameof(Create), createTrainer);
            }

            bool result = _trainerService.CreateTrainer(createTrainer);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer created successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer failed to create";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid id";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer == null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);


        }

        public ActionResult TrainerEdit(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of trainer cant be 0 or negative";
                return RedirectToAction(nameof(Index));
            }

            var member = _trainerService.GetTrainerToUpdate(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "trainer not found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }
        [HttpPost]
        public ActionResult TrainerEdit([FromRoute] int id, TrainerToUpdateViewModel trainerToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(trainerToUpdate);
            }
            var result = _trainerService.UpdateTrainer(id, trainerToUpdate);

            if (result)
            {
                TempData["SuccessMessage"] = "Trainer updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer failed to update";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of trainer cant be 0 or negative";
                return RedirectToAction(nameof(Index));
            }

            var member = _trainerService.GetTrainerDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Trainer not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirm([FromForm] int id)
        {
            var result = _trainerService.DeleteTrainer(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Trainer deleted successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer failed to delete";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
