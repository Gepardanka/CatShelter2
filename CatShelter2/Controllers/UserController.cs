using CatShelter.Data;
using CatShelter.Models;
using CatShelter.Services;
using CatShelter.ViewModels.UserViewModels;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace CatShelter.Controllers
{
    [Authorize(Policy = "Basic")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateViewModel> _createValidator;
        private readonly IValidator<EditViewModel> _editValidator;
        private readonly UserManager<User> _userManager;
        public UserController(IUserService userService, IValidator<CreateViewModel> createValidator, IValidator<EditViewModel> editValidator
            , UserManager<User> userManager)
        {
            _userService = userService;
            _createValidator = createValidator;
            _editValidator = editValidator;
            _userManager = userManager;
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

        [Authorize(Policy = "ElevatedPrivileges")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [Authorize(Policy = "ElevatedPrivileges")]
        [HttpPost]
        public IActionResult Create(CreateViewModel userViewModel)
        {
            ValidationResult result = _createValidator.Validate(userViewModel);
            if (result.IsValid)
            {
                Console.WriteLine("valid");
                User user = userViewModel.Adapt<User>();
                _userService.Insert(user);
                _userManager.PasswordHasher.HashPassword(user, userViewModel.Password);
                ManageRoles(user);
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
        public async Task<IActionResult> Edit(EditViewModel userViewModel)
        {
            ValidationResult result = _editValidator.Validate(userViewModel);
            if (result.IsValid)
            {

                User user = _userService.GetById(userViewModel.Id)!;
                user.Name = userViewModel.Name;
                user.Surname = userViewModel.Surname;
                user.Email = userViewModel.Email;
                user.PhoneNumber = userViewModel.PhoneNumber;
                user.IsAdmin = userViewModel.IsAdmin;
                user.IsEmployee = userViewModel.IsEmployee;
                await _userManager.UpdateAsync(user);
                ManageRoles(user);
                return Redirect($"/user/details/{user.Id}");
            }
            result.AddToModelState(this.ModelState);
            return View(userViewModel);
        }

        [Authorize(Roles = "Admin")]
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

        private void ManageRoles(User user)
        {
            
            if (user.IsAdmin)
            {
                AddRole(user.Id, "Admin");
            }
            if (user.IsEmployee)
            {
                AddRole(user.Id, "Employee");
            }
            AddRole(user.Id, "User");

            if(!user.IsAdmin){
                RemoveRole(user.Id, "Admin");
            }
            if(!user.IsEmployee){
                RemoveRole(user.Id, "Employee");
            }
        }
        private void AddRole(IdType userId, string role){
            var roleId = _userService.GetRoleId(role);
                var hasRole = _userService.GetUserRole(userId, roleId);
                if(hasRole == null){
                    _userService.AddUserRole(userId, roleId);
                }
        }
        private void RemoveRole(IdType userId, string role){
            var roleId = _userService.GetRoleId(role);
            var hasRole = _userService.GetUserRole(userId, roleId);
                if(hasRole != null){
                    _userService.RemoveUserRole(userId, roleId);
                }
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

