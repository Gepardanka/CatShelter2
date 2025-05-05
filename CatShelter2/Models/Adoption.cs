using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatShelter.Models
{
    public class Adoption
    {
        [Key]
        public IdType Id { get; set; }
        [ForeignKey("Cat")]
        public IdType? CatId { get; set; }
        public Cat? Cat { get; set; }
        [ForeignKey("User")]
        public IdType? UserId { get; set; }
        public User? User { get; set; }
        public DateOnly Date { get; set; }
        public AdoptionType AdoptionType { get; set; }
    }

    public enum AdoptionType
    {
        Temporary = 0,
        LongTerm = 1
    }
}
