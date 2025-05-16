namespace CatShelter.ViewModels.AdoptionViewModels
{
    public class IndexViewModel
    {
        public required List<AdoptionViewModel> Adoptions { get; set; }
        public required AdoptionFilter AdoptionFilter { get; set; }
    }
    public class AdoptionFilter
    {
        public bool OnlyTemporary { get; set; } = false;
        public bool OnlyLongTerm { get; set; } = false;
    }
}