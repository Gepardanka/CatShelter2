namespace CatShelter.ViewModels.CatViewModels
{
    public class CatViewModel
    {
        public IdType Id { get; set; }
        public string Name { get; set; } = "";
        public int YearOfBirth { get; set; }
        public DateOnly ArriveDate { get; set; }
        public string Picture { get; set; } = "";

        public IdType? CarerId { get; set; }
        public UserViewModels.UserViewModel? Carer { get; set; }

        public IList<AdoptionViewModels.AdoptionViewModel> Adoptions { get; set; } = [];
    }
}
