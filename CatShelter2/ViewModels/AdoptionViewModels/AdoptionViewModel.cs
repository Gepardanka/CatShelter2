using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CatShelter.ViewModels.AdoptionViewModels
{
    public class AdoptionViewModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CatViewModel")]
        public int? CatId { get; set; }
        public CatViewModels.CatViewModel? Cat { get; set; }
        [ForeignKey("UserViewModel")]
        public int? UserId { get; set; }
        public UserViewModels.UserViewModel? User { get; set; }
        public DateOnly Date { get; set; }
        public AdoptionType AdoptionType { get; set; }
    }
    public enum AdoptionType
    {
        Temporary = 0,
        LongTerm = 1
    }
}
