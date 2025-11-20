using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class ResourceController : Controller
{
    private readonly AppDbContext _context;
    public ResourceController(AppDbContext context) { _context = context; }

    public async Task<IActionResult> Index()
    {
        var Resource = await _context.Resource.ToListAsync();
        return View(Resource);
    }
}
