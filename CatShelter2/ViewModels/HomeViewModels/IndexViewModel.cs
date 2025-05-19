namespace CatShelter.ViewModels.HomeViewModels{
    public class IndexViewModel{
        public int TotalCats { get; set; }
        public int TotalUsers { get; set; }
        public int TotalAdoptions { get; set; }
        public required List<string> RecentActivities { get; set; }
    }
}