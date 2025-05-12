using CatShelter.Models;
using System.ComponentModel;

namespace CatShelter.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        [DisplayName("Admin")]
        public bool IsAdmin { get; set; }
        [DisplayName("Employee")]
        public bool IsEmployee { get; set; }
    }
}
