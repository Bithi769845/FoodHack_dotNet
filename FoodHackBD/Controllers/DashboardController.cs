using FoodHackBD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DashboardController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        // Recent 5 FoodLogs
        var recentLogs = await _context.FoodLogs
                                .Where(f => f.UserId == user.Id)
                                .OrderByDescending(f => f.LogDate)
                                .Take(5)
                                .ToListAsync();

        // Inventory count
        var inventoryCount = await _context.Inventories
                                    .Where(i => i.UserId == user.Id)
                                    .CountAsync();

        // Rule-based recommendations
        var categoriesLogged = recentLogs.Select(f => f.Category).Distinct().ToList();
        var recommendations = await _context.Resource
                                    .Where(r => categoriesLogged.Contains(r.Category))
                                    .ToListAsync();

        // Recent uploads
        var recentUploads = await _context.Uploads
                            .Where(u => u.UserId == user.Id)
                            .OrderByDescending(u => u.Id)
                            .Take(5)
                            .ToListAsync();

        // Pass all to view
        var model = new DashboardViewModel
        {
            RecentLogs = recentLogs,
            InventoryCount = inventoryCount,
            Recommendations = recommendations,
            RecentUploads = recentUploads,
            UserProfile = user
        };

        return View(model);
    }

}
