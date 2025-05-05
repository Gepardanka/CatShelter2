using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.UserViewModels;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace CatShelter.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateViewModel> _createValidator;
        private readonly IValidator<EditViewModel> _editValidator;
        public UserController(IUserService userService, IValidator<CreateViewModel> createValidator, IValidator<EditViewModel> editValidator)
        {
            _userService = userService;
            _createValidator = createValidator;
            _editValidator = editValidator;
        }

      

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index(UserFilter userFilter)
        {
            return View(new IndexViewModel
            {
                Users = _userService.GetByName(userFilter.Surname, userFilter.Name).Select(x => x.Adapt<UserViewModel>()).ToList(),
                UserFilter = userFilter
            });
        }

        [HttpGet]
        public IActionResult Details(IdType id)
        {
            var user = _userService.GetById(id);
            if (user == null) { return NotFound(); }
            return View(user.Adapt<UserViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel userViewModel)
        {
            ValidationResult result = _createValidator.Validate(userViewModel);
            if (result.IsValid) {
                Console.WriteLine("valid");
                User user = userViewModel.Adapt<User>();
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _userService.Insert(user);
                return Redirect($"/user/details/{user.Id}");
            }
            result.AddToModelState(this.ModelState);
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Edit(IdType id)
        {
            var user = _userService.GetById(id);
            if (user == null) { return NotFound(); }
            return View(user.Adapt<EditViewModel>());
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel userViewModel)
        {
            ValidationResult result = _editValidator.Validate(userViewModel);
            if (result.IsValid) {
                User user = userViewModel.Adapt<User>();
                if(!user.Password.IsNullOrEmpty()){
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                _userService.Update(user);
                return Redirect($"/user/details/{user.Id}");
            }
            result.AddToModelState(this.ModelState);
            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Delete(IdType id)
        {
            _userService.Delete(id);
            return Redirect("/user");
        }

        [HttpGet]
        public IActionResult Employees()
        {
            return View(_userService.GetEmployees().Select(x => x.Adapt<UserViewModel>()));
        }

        [HttpGet]
        public IActionResult Admins()
        {
            return View(_userService.GetAdmins().Select(x => x.Adapt<UserViewModel>()));
        }

    }
    public static class Extensions
    {
        public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }

}

