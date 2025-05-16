using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatShelter.ViewModels.CatViewModels
{
    public class CatViewModel
    {
        [Key]
        public IdType Id { get; set; }
        public string Name { get; set; } = "";
        public int YearOfBirth { get; set; }
        public DateOnly ArriveDate { get; set; }
        public string Picture { get; set; } = "";

        [ForeignKey("UserViewModel")]
        public IdType? CarerId { get; set; }
        public UserViewModels.UserViewModel? Carer { get; set; }

        public IList<AdoptionViewModels.AdoptionViewModel> Adoptions { get; set; } = [];
    }
}
