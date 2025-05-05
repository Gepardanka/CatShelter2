using CatShelter.Models;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CatShelter.ViewModels.UserViewModels
{
    public class CreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Employee")]
        public bool IsEmployee { get; set; }

    }

    public class CreateViewModelValidator : AbstractValidator<CreateViewModel>
    {
        public CreateViewModelValidator() {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Phone).Custom((phone, context) =>
            {
                if (phone.IsNullOrEmpty()) { return; }
                if (phone.Any(x => !char.IsAsciiDigit(x)))
                {
                    context.AddFailure("A phone number must be digits only");
                }
            });
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x => x.Password).Custom((password, context) => {
                if (password.IsNullOrEmpty()) { return; }
                if (!password.Any(x => char.IsLower(x)))
                {
                    context.AddFailure("A password must contain a lower letter");
                }
                if(!password.Any(x => char.IsUpper(x)))
                {
                    context.AddFailure("A password must contain an upper letter");
                }
                if (!password.Any(x => char.IsAsciiDigit(x)))
                {
                    context.AddFailure("A password must contain a digit");
                }
            });
        }

    } 
}
