using System.Diagnostics;
using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.HomeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CatShelter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ICatService _catService;
        private readonly IAdoptionService _adoptionService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ICatService catService, IAdoptionService adoptionService)
        {
            _logger = logger;
            _adoptionService = adoptionService;
            _userService = userService;
            _catService = catService;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel{
                TotalCats = _catService.GetAll().Count(),
                TotalAdoptions = _adoptionService.GetAll().Count(),
                TotalUsers = _userService.GetAll().Count(),
                RecentActivities = new List<string>{"Example activity", "Example activity"}
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
