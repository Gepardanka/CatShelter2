using CatShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace CatShelter.ViewModels.UserViewModels
{
    public class EditViewModel
    {
        public required IdType Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Employee")]
        public bool IsEmployee { get; set; }
        public string? ConcurrencyStamp { get; set; }

    }

    public class EditViewModelValidator : AbstractValidator<EditViewModel>
    {
        public EditViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.PhoneNumber).Custom((phone, context) =>
            {
                if (phone.IsNullOrEmpty()) { return; }
                if (phone.Any(x => !char.IsAsciiDigit(x)))
                {
                    context.AddFailure("A phone number must be digits only");
                }
            });
            //When(x => !x.Password.IsNullOrEmpty(), () => {
            //    RuleFor(x => x.Password).MinimumLength(8);
            //    RuleFor(x => x.Password).Custom((password, context) => {
            //        if (!password.Any(x => char.IsLower(x)))
            //        {
            //            context.AddFailure("A password must contain a lower letter");
            //        }
            //        if (!password.Any(x => char.IsUpper(x)))
            //        {
            //            context.AddFailure("A password must contain an upper letter");
            //        }
            //        if (!password.Any(x => char.IsAsciiDigit(x)))
            //        {
            //            context.AddFailure("A password must contain a digit");
            //        }
            //    });
            //});

        }

    }
}
