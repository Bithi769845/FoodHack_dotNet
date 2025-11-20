using System.ComponentModel.DataAnnotations;

namespace FoodHackBD.Models
{
    public class Upload
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        // যদি ApplicationUser.Id Guid type হয়:
        public Guid UserId { get; set; }
    }
}
