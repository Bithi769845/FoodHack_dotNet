using FoodHackBD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<FoodLog> FoodLogs { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<FoodItem> FoodItem {  get; set; }
    public DbSet<Resource> Resource { get; set; }
    public DbSet<Upload> Uploads { get; set; }


}
