using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatShelter.Models
{
    public class Cat
    {
        [Key]
        public IdType Id { get; set; }
        public string Name { get; set; } = "";
        public int YearOfBirth { get; set; }
        public DateOnly ArriveDate { get; set; }
        public string? Picture { get; set; }

        [ForeignKey("User")]
        public IdType? CarerId { get; set; }
        public User? Carer { get; set; }

        public IList<Adoption> Adoptions { get; set; } = [];


    }
}
