using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CatShelter.Models
{
    public class User : IdentityUser<IdType>
    {
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public bool IsAdmin { get; set; }
        public bool IsEmployee { get; set; }


        public IList<Adoption> Adoptions { get; set; } = [];
        public IList<Cat> CaredForCats { get; set; } = [];

        public User() { }

    }
}
