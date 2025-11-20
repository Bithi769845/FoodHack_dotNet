using Microsoft.AspNetCore.Identity;
using System;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; set; }
    public int? HouseholdSize { get; set; }
    public string? DietPreference { get; set; }
    public string? Budget { get; set; }
    public string? Location { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
