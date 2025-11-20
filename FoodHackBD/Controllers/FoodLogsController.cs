using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class FoodLogsController: Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public FoodLogsController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // ===================== Food Logs =====================
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var logs = await _context.FoodLogs
                        .Where(f => f.UserId == user.Id)
                        .OrderByDescending(f => f.LogDate)
                        .ToListAsync();
        return View("Index", logs);
    }

    public async Task<IActionResult> AddFoodLog()
    {
        var user = await _userManager.GetUserAsync(User);
        ViewBag.Uploads = await _context.Uploads
                            .Where(u => u.UserId == user.Id)
                            .ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddFoodLog(FoodLog model)
    {
        if (!ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Uploads = await _context.Uploads
                                .Where(u => u.UserId == user.Id)
                                .ToListAsync();
            return View(model);
        }

        var currentUser = await _userManager.GetUserAsync(User);
        model.UserId = currentUser.Id;

        _context.FoodLogs.Add(model);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Food log added successfully!";
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> EditFoodLog(int id)
    {
        var log = await _context.FoodLogs.FindAsync(id);
        if (log == null) return NotFound();

        var user = await _userManager.GetUserAsync(User);
        if (log.UserId != user.Id) return Unauthorized();

        ViewBag.Uploads = await _context.Uploads
                            .Where(u => u.UserId == user.Id)
                            .ToListAsync();

        return View(log);
    }

    [HttpPost]
    public async Task<IActionResult> EditFoodLog(FoodLog model)
    {
        if (!ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Uploads = await _context.Uploads
                                .Where(u => u.UserId == user.Id)
                                .ToListAsync();
            return View(model);
        }

        var userDb = await _userManager.GetUserAsync(User);
        if (model.UserId != userDb.Id) return Unauthorized();

        _context.FoodLogs.Update(model);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Food log updated successfully!";
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> DeleteFoodLog(int id)
    {
        var log = await _context.FoodLogs.FindAsync(id);
        if (log != null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (log.UserId != user.Id) return Unauthorized();

            _context.FoodLogs.Remove(log);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Food log deleted successfully!";
        }
        return RedirectToAction("Index");
    }


}
