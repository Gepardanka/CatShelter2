namespace CatShelter.ViewModels.AdoptionViewModels
{
    public class IndexViewModel
    {
        public required List<AdoptionViewModel> Adoptions { get; set; }
        public required AdoptionFilter AdoptionFilter { get; set; }
    }
    public class AdoptionFilter
    {
        public string Filter { get; set; } = "All";
    }
}