namespace CatShelter.ViewModels.UserViewModels
{
    public class IndexViewModel
    {
        public required List<UserViewModel> Users { get; set; }
        public required UserFilter UserFilter { get; set; }
    }        
    public class UserFilter
    {
        public string? Surname { get; set; }
        public string? Name { get; set; }

    }
}
