using CatShelter.Services;
using CatShelter.ViewModels.AdoptionViewModels;
using Microsoft.AspNetCore.Mvc;
namespace CatShelter.Controllers
{
    public class AdoptionController : Controller
    {
        readonly AdoptionService _service;
        public AdoptionController(AdoptionService service){
            _service = service;
        }

        // [HttpGet]
        // public IActionResult Index(){
        //     return View(new IndexViewModel{})
        // }
    }
}