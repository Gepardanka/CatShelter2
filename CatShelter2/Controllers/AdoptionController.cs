using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.AdoptionViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CatShelter.Controllers
{
    public class AdoptionController : Controller
    {
        readonly IAdoptionService _adoptionService;
        readonly IUserService _userService;
        readonly ICatService _catService;
        public AdoptionController(IAdoptionService service, IUserService userService, ICatService catService)
        {
            _adoptionService = service;
            _userService = userService;
            _catService = catService;
        }
        [HttpGet]
        public IActionResult Index(AdoptionFilter adoptionFilter)
        {
            IQueryable<Adoption> adoptions;
            if (adoptionFilter.Filter == "OnlyLongTerm")
            {
                adoptions = _adoptionService.GetLongTerm();
            }
            else if (adoptionFilter.Filter == "OnlyTemporary")
            {
                adoptions = _adoptionService.GetTemporary();
            }
            else
            {
                adoptions = _adoptionService.GetAll();
            }
            return View(new IndexViewModel
            {
                Adoptions = adoptions
                    .Select(x => ModelToAdoptionViewModel(x)).ToList(),
                AdoptionFilter = adoptionFilter
            });
        }
        [HttpGet]
        public IActionResult Details(IdType id)
        {
            var adoption = _adoptionService.GetById(id);
            if (adoption == null) { return NotFound(); }
            return View(ModelToAdoptionViewModel(adoption));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(ModelToCreateViewModel(null));
        }
        [HttpPost]
        public IActionResult Create(CreateViewModel createViewModel)
        {
            var adoption = CreateViewModelToModel(createViewModel);
            _adoptionService.Insert(adoption);
            return Redirect($"/adoption/details/{adoption.Id}");
        }
        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var adoption = _adoptionService.GetById(id);
            if (adoption == null) { return NotFound(); }
            return View(ModelToCreateViewModel(adoption));
        }
        [HttpPost]
        public IActionResult Edit(CreateViewModel createViewModel)
        {
            var adoption = CreateViewModelToModel(createViewModel);
            _adoptionService.Update(adoption);
            return Redirect($"/adoption/details/{adoption.Id}");
        }
        [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _adoptionService.Delete(id);
            return Redirect("/adoption");
        }
        private static Models.Adoption CreateViewModelToModel(CreateViewModel viewModel)
        {
            return new Models.Adoption
            {
                Id = viewModel.Id,
                AdoptionType = viewModel.AdoptionType,
                Date = viewModel.Date,
                CatId = viewModel.CatId,
                Cat = viewModel.Cat == null ? null : new Cat
                {
                    Id = viewModel.Cat.Id,
                    Name = viewModel.Cat.Name,
                },
                UserId = viewModel.UserId,
                User = viewModel.User == null ? null : new User
                {
                    Id = viewModel.User.Id,
                    Email = viewModel.User.Email,
                }
            };
        }
        private CreateViewModel ModelToCreateViewModel(Models.Adoption? model)
        {
            var viewModel = new CreateViewModel
            {
                AvailableCats = _catService
                    .GetAll()
                    .Where(x => x.Adoptions.Count == 0)
                    .Select(x => new CatList
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList(),
                AvailableUsers = _userService.GetAll()
                    .Select(x => new UserList
                    {
                        Id = x.Id,
                        Email = x.Email!
                    }).ToList(),
                Date = DateOnly.FromDateTime(DateTime.Now)
            };
            if (model == null) return viewModel;
            viewModel.AdoptionType = model.AdoptionType;
            viewModel.CatId = model.CatId;
            viewModel.Cat = model.Cat == null ? null : new ViewModels.CatViewModels.CatViewModel
            {
                Id = model.Cat.Id,
                Name = model.Cat.Name,
            };
            viewModel.UserId = model.UserId;
            viewModel.User = model.User == null ? null : new ViewModels.UserViewModels.UserViewModel
            {
                Id = model.User.Id,
                Email = model.User.Email!,
            };
            var cat = _catService.GetById(model.Cat!.Id);
            viewModel.AvailableCats.Add(new CatList
            {
                Id = cat!.Id,
                Name = cat.Name,
            }
            );
            return viewModel;
        }
        private static AdoptionViewModel ModelToAdoptionViewModel(Adoption adoption)
        {
            return new AdoptionViewModel
            {
                Id = adoption.Id,
                CatId = adoption.CatId,
                Cat = adoption.Cat == null? null : new ViewModels.CatViewModels.CatViewModel
                {
                    Id = adoption.Cat.Id,
                    Name = adoption.Cat.Name,
                    CarerId = adoption.Cat.CarerId,
                    Carer = adoption.Cat.Carer == null? null : new ViewModels.UserViewModels.UserViewModel
                    {
                        Id = adoption.Cat.Carer.Id,
                        Email = adoption.Cat.Carer.Email!
                    }
                },
                UserId = adoption.UserId,
                User = adoption.User == null? null : new ViewModels.UserViewModels.UserViewModel
                {
                    Id = adoption.User.Id,
                    Email = adoption.User.Email!
                },
                Date = adoption.Date,
                AdoptionType = adoption.AdoptionType
            };
        }
    }

}