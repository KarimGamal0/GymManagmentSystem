using System.Diagnostics;
using GymManagmentBLL.Service.Interfaces;
using GymManagmentDAL.Entities;
using GymManagmentPL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        public ViewResult Index()
        {
            var data = _analyticsService.GetAnalyticsData();

            return View(data);
        }


    }
}
