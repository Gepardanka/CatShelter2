using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.CatViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace CatShelter.Controllers{
    public class CatController : Controller
    {
        readonly ICatService _catService;
        readonly IUserService _userService;
        public CatController(ICatService service, IUserService userService)
        {
            _catService = service;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                Cats = _catService.GetAll().Select(x => x.Adapt<CatViewModel>()).ToList()
            });
        }
        [HttpGet]
        public IActionResult Details(IdType id)
        {
            var cat = _catService.GetById(id);
            if (cat == null) { return NotFound(); }
            return View(cat.Adapt<CatViewModel>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CarerId = _userService.GetEmployees().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Email,
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CatViewModel catViewModel)
        {
            var cat = catViewModel.Adapt<Cat>();
            _catService.Insert(cat);
            return Redirect($"/cat/details/{cat.Id}");
        }
        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var cat = _catService.GetById(id);
            if (cat == null) { return NotFound(); }
            return View(cat.Adapt<CatViewModel>());
        }
        [HttpPost]
        public IActionResult Edit(CatViewModel catViewModel)
        {
            var cat = catViewModel.Adapt<Cat>();
            _catService.Update(cat);
            return Redirect($"/cat/details/{cat.Id}");
        }
        [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _catService.Delete(id);
            return Redirect("/cat");
        }
    }
}