using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.CatViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
namespace CatShelter.Controllers{
    public class CatController : Controller
    {
        readonly CatService _service;
        public CatController(CatService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new IndexViewModel
            {
                Cats = _service.GetAll().Select(x => x.Adapt<CatViewModel>()).ToList()
            });
        }
        [HttpGet]
        public IActionResult Details(IdType id)
        {
            var cat = _service.GetById(id);
            if (cat == null) { return NotFound(); }
            return View(cat.Adapt<CatViewModel>());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CatViewModel catViewModel)
        {
            var cat = catViewModel.Adapt<Cat>();
            _service.Insert(cat);
            return Redirect($"/adoption/details/{cat.Id}");
        }
        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var cat = _service.GetById(id);
            if (cat == null) { return NotFound(); }
            return View(cat.Adapt<CatViewModel>());
        }
        [HttpPost]
        public IActionResult Edit(CatViewModel catViewModel)
        {
            var cat = catViewModel.Adapt<Cat>();
            _service.Update(cat);
            return Redirect($"/adoption/details/{cat.Id}");
        }
        [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _service.Delete(id);
            return Redirect("/cat");
        }
    }
}