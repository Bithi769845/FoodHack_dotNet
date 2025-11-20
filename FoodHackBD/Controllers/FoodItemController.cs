using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class FoodItemController : Controller
{
    private readonly AppDbContext _context;
    public FoodItemController(AppDbContext context) { _context = context; }

    public async Task<IActionResult> Index()
    {
        var items = await _context.FoodItem.ToListAsync();
        return View(items);
    }
}
