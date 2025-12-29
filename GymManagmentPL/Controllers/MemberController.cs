using GymManagmentBLL.Service.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymManagmentPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public ActionResult Index()
        {
            var member = _memberService.GetAllMembers();
            return View(member);
        }

        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of member cant be 0 or negative";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of member cant be 0 or negative";
                return RedirectToAction(nameof(Index));
            }

            var healthrecord = _memberService.GetMemberHealthRecordDetails(id);

            if (healthrecord is null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }

            return View(healthrecord);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            //Check if there is data or not
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "check data and Missing Field");
                return View(nameof(Create), createMember);
            }

            bool result = _memberService.CreateMember(createMember);
            if (result)
            {
                TempData["SuccessMessage"] = "Member created successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member failed to create";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of member cant be 0 or negative";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberToUpdate(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id, MemberToUpdateViewModel memberToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(memberToUpdate);
            }
            var result = _memberService.UpdateMemberDetails(id, memberToUpdate);

            if (result)
            {
                TempData["SuccessMessage"] = "Member updated successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member failed to update";
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of member cant be 0 or negative";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirm([FromForm]int id)
        {
            var result = _memberService.RemoveMember(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Member deleted successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member failed to delete";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
