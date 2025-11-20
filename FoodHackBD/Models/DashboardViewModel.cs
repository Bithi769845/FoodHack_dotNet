namespace FoodHackBD.Models
{
    public class DashboardViewModel
    {
        public List<FoodLog> RecentLogs { get; set; }
        public int InventoryCount { get; set; }
        public List<Resource> Recommendations { get; set; }

        // Recent uploads (optional)
        public List<Upload> RecentUploads { get; set; } = new List<Upload>();
        public ApplicationUser UserProfile { get; set; }
    }

}
