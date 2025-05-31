namespace CatShelter.ViewModels.CatViewModels
{
    public class CreateViewModel
    {
        public IdType Id { get; set; }
        public string Name { get; set; } = "";
        public int YearOfBirth { get; set; }
        public DateOnly ArriveDate { get; set; }
        public string Picture { get; set; } = "";

        public IdType? CarerId { get; set; }
        public UserViewModels.UserViewModel? Carer { get; set; }

        public List<CarerList> AvailableCarers { get; set; } = [];
    }
    public class CarerList
    {
        public IdType Id { get; set; }
        public required string Email { get; set; }
    }
}