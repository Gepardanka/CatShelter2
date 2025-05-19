using CatShelter.ViewModels.AdoptionViewModels;
using CatShelter.ViewModels.CatViewModels;
using CatShelter.ViewModels.UserViewModels;

namespace CatShelter.ViewModels.AdoptionViewModels
{
    public class CreateViewModel
    {
        public IdType Id { get; set; }
        public IdType? CatId { get; set; }
        public CatViewModel? Cat { get; set; }
        public IdType? UserId { get; set; }
        public UserViewModel? User { get; set; }
        public DateOnly Date { get; set; }
        public Models.AdoptionType AdoptionType { get; set; }
        public required List<CatList> AvaliableCats { get; set; }
        public required List<UserList> AvaliableUsers { get; set; }
    }
    public class CatList
    {
        public IdType Id { get; set; }
        public required string Name { get; set; }
    }
    public class UserList
    {
        public IdType Id { get; set; }
        public required string Email { get; set; }
    }
}
