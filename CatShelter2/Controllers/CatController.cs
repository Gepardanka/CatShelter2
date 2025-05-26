using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.CatViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
namespace CatShelter.Controllers{
    public class CatController : Controller
    {
        readonly ICatService _catService;
        readonly IUserService _userService;
        readonly IStringLocalizer<CatController> _stringLocalizer;
        public CatController(ICatService service, IUserService userService, IStringLocalizer<CatController> stringLocalizer)
        {
            _catService = service;
            _userService = userService;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            System.Console.WriteLine(_stringLocalizer["Hello"]);

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
            return View(new CatViewModel {
                Id = cat.Id,
                Adoptions = cat.Adoptions.Select(x => new CatShelter.ViewModels.AdoptionViewModels.AdoptionViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    UserId = x.UserId,
                    AdoptionType = x.AdoptionType
                }).ToList(),
                ArriveDate = cat.ArriveDate,
                Carer = cat.Carer == null? null :
                    new ViewModels.UserViewModels.UserViewModel
                {
                    Id = cat.Carer.Id,
                    Email = cat.Carer.Email,
                },
                CarerId = cat.CarerId,
                Name = cat.Name,
                Picture = cat.Picture,
                YearOfBirth = cat.YearOfBirth
            });
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