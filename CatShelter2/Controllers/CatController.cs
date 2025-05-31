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
            return View(new CatViewModel
            {
                Id = cat.Id,
                Adoptions = cat.Adoptions.Select(x => new CatShelter.ViewModels.AdoptionViewModels.AdoptionViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    UserId = x.UserId,
                    AdoptionType = x.AdoptionType
                }).ToList(),
                ArriveDate = cat.ArriveDate,
                Carer = cat.Carer == null ? null :
                    new ViewModels.UserViewModels.UserViewModel
                    {
                        Id = cat.Carer.Id,
                        Email = cat.Carer.Email!,
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
            return View(ModelToCreateViewModel(null));
        }
        [HttpPost]
        public IActionResult Create(CreateViewModel createViewModel)
        {
            var cat = CreateViewModelToModel(createViewModel);
            _catService.Insert(cat);
            return Redirect($"/cat/details/{cat.Id}");
        }
        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var cat = _catService.GetById(id);
            if (cat == null) { return NotFound(); }
            return View(ModelToCreateViewModel(cat));
        }
        [HttpPost]
        public IActionResult Edit(CreateViewModel createViewModel)
        {
            var cat = CreateViewModelToModel(createViewModel);
            _catService.Update(cat);
            return Redirect($"/cat/details/{cat.Id}");
        }
        [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _catService.Delete(id);
            return Redirect("/cat");
        }
        private CreateViewModel ModelToCreateViewModel(Cat? model)
        {
            var viewModel = new CreateViewModel
            {
                AvailableCarers = _userService.GetEmployees().Select(x => new CarerList
                {
                    Id = x.Id,
                    Email = x.Email!,
                }).ToList(),
            };
            if (model == null) return viewModel;
            viewModel.Id = model.Id;
            viewModel.ArriveDate = model.ArriveDate;
            viewModel.Name = model.Name;
            viewModel.YearOfBirth = model.YearOfBirth;
            viewModel.Picture = model.Picture;
            viewModel.CarerId = model.CarerId;
            viewModel.Carer = model.Carer == null ? null : new ViewModels.UserViewModels.UserViewModel
            {
                Id = model.Carer.Id,
                Email = model.Carer.Email!
            };
            return viewModel;
        }
        private Cat CreateViewModelToModel(CreateViewModel createViewModel)
        {
            return new Cat
            {
                Id = createViewModel.Id,
                Name = createViewModel.Name,
                YearOfBirth = createViewModel.YearOfBirth,
                Picture = createViewModel.Picture,
                ArriveDate = createViewModel.ArriveDate,
                CarerId = createViewModel.CarerId,
                Carer = createViewModel.Carer == null ? null : new User
                {
                    Id = createViewModel.Carer.Id,
                }
            };
        }
    }
}