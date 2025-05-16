using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.AdoptionViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
namespace CatShelter.Controllers
{
    public class AdoptionController : Controller
    {
        readonly AdoptionService _service;
        public AdoptionController(AdoptionService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Index(AdoptionFilter adoptionFilter)
        {
            IQueryable<Adoption> adoptions;
            if (adoptionFilter.OnlyLongTerm)
            {
                adoptions = _service.GetLongTerm();
            }
            else if (adoptionFilter.OnlyTemporary)
            {
                adoptions = _service.GetTemporary();
            }
            else
            {
                adoptions = _service.GetAll();
            }
            return View(new IndexViewModel
            {
                Adoptions = adoptions.Select(x => x.Adapt<AdoptionViewModel>()).ToList(),
                AdoptionFilter = adoptionFilter
            });
        }
        [HttpGet]
        public IActionResult Details(IdType id)
        {
            var adoption = _service.GetById(id);
            if (adoption == null) { return NotFound(); }
            return View(adoption.Adapt<AdoptionViewModel>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AdoptionViewModel adoptionViewModel)
        {
            var adoption = adoptionViewModel.Adapt<Adoption>();
            _service.Insert(adoption);
            return Redirect($"/adoption/details/{adoption.Id}");
        }
        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var adoption = _service.GetById(id);
            if (adoption == null) { return NotFound(); }
            return View(adoption.Adapt<AdoptionViewModel>());
        }
        [HttpPost]
        public IActionResult Edit(AdoptionViewModel adoptionViewModel)
        {
            var adoption = adoptionViewModel.Adapt<Adoption>();
            _service.Update(adoption);
            return Redirect($"/adoption/details/{adoption.Id}");
        }
                [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _service.Delete(id);
            return Redirect("/adoption");
        }
    }
}