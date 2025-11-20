using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class InventoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public InventoryController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // ===================== Inventory =====================
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var inventory = await _context.Inventories
                                .Where(i => i.UserId == user.Id)
                                .OrderByDescending(i => i.AddedDate)
                                .ToListAsync();
        return View(inventory);
    }

    // GET: Add Inventory
    public async Task<IActionResult> AddInventory()
    {
        var user = await _userManager.GetUserAsync(User);

        // Populate uploads for dropdown
        ViewBag.Uploads = await _context.Uploads
                                .Where(u => u.UserId == user.Id)
                                .ToListAsync();

        return View();
    }

    // POST: Add Inventory
    [HttpPost]
    public async Task<IActionResult> AddInventory(Inventory model)
    {
        var user = await _userManager.GetUserAsync(User);

        if (!ModelState.IsValid)
        {
            ViewBag.Uploads = await _context.Uploads
                                    .Where(u => u.UserId == user.Id)
                                    .ToListAsync();
            return View(model);
        }

        model.UserId = user.Id;

        _context.Inventories.Add(model);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Inventory item added!";
        return RedirectToAction("Index");
    }

    // GET: Edit Inventory
    public async Task<IActionResult> EditInventory(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var item = await _context.Inventories
                    .Where(i => i.Id == id && i.UserId == user.Id)
                    .FirstOrDefaultAsync();

        if (item == null) return NotFound();

        // Populate uploads for dropdown
        ViewBag.Uploads = await _context.Uploads
                                .Where(u => u.UserId == user.Id)
                                .ToListAsync();

        return View(item);
    }

    // POST: Edit Inventory
    [HttpPost]
    public async Task<IActionResult> EditInventory(Inventory model)
    {
        var user = await _userManager.GetUserAsync(User);

        if (!ModelState.IsValid)
        {
            ViewBag.Uploads = await _context.Uploads
                                    .Where(u => u.UserId == user.Id)
                                    .ToListAsync();
            return View(model);
        }

        model.UserId = user.Id;

        _context.Inventories.Update(model);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Inventory updated successfully!";
        return RedirectToAction("Index");
    }

    // Delete Inventory
    public async Task<IActionResult> DeleteInventory(int id)
    {
        var item = await _context.Inventories.FindAsync(id);
        if (item != null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (item.UserId != user.Id) return Unauthorized();

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Inventory deleted successfully!";
        }
        return RedirectToAction("Index");
    }
}
