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
                    .Include(x => x.Cat)
                    .Include(x => x.User)
                    .Select(x => new AdoptionViewModel
                    {
                        Id = x.Id,
                        CatId = x.CatId,
                        Cat = new ViewModels.CatViewModels.CatViewModel
                        {
                            Id = x.Cat!.Id,
                            Name = x.Cat!.Name
                        },
                        UserId = x.UserId,
                        User = new ViewModels.UserViewModels.UserViewModel
                        {
                            Id = x.User!.Id,
                            Email = x.User!.Email!
                        },
                        Date = x.Date,
                        AdoptionType = x.AdoptionType
                    }).ToList(),
                    
                AdoptionFilter = adoptionFilter
            });
        }
        [HttpGet]
        public IActionResult Details(IdType id)
        {
            var adoption = _adoptionService.GetById(id);
            if (adoption == null) { return NotFound(); }
            return View(adoption.Adapt<AdoptionViewModel>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateViewModel
            {
                AvaliableCats = _catService
                    .GetAll()
                    .Include(x => x.Adoptions)
                    .Where(x => x.Adoptions.Count == 0)
                    .Select(x => new CatList
                        {
                            Id = x.Id,
                            Name = x.Name,
                        }).ToList(),
                AvaliableUsers = _userService.GetAll()
                    .Select(x => new UserList
                    {
                        Id = x.Id,
                        Email = x.Email!
                    }).ToList(),
                Date = DateOnly.FromDateTime(DateTime.Now),
            });
        }
        [HttpPost]
        public IActionResult Create(AdoptionViewModel adoptionViewModel)
        {
            var adoption = adoptionViewModel.Adapt<Adoption>();
            _adoptionService.Insert(adoption);
            return Redirect($"/adoption/details/{adoption.Id}");
        }
        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var adoption = _adoptionService.GetById(id);
            if (adoption == null) { return NotFound(); }
            return View(adoption.Adapt<AdoptionViewModel>());
        }
        [HttpPost]
        public IActionResult Edit(AdoptionViewModel adoptionViewModel)
        {
            var adoption = adoptionViewModel.Adapt<Adoption>();
            _adoptionService.Update(adoption);
            return Redirect($"/adoption/details/{adoption.Id}");
        }
        [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _adoptionService.Delete(id);
            return Redirect("/adoption");
        }
    }
}