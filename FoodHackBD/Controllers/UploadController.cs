using FoodHackBD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class UploadController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly UserManager<ApplicationUser> _userManager;

    public UploadController(AppDbContext context, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _env = env;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var uploads = await _context.Uploads.Where(u => u.UserId == user.Id).ToListAsync();
        return View(uploads);
    }

    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Please select a file!";
            return RedirectToAction("Index");
        }

        var user = await _userManager.GetUserAsync(User);
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var upload = new Upload
        {
            FileName = file.FileName,
            FilePath = "/uploads/" + file.FileName,
            UserId = user.Id
        };

        _context.Uploads.Add(upload);
        await _context.SaveChangesAsync();

        TempData["Success"] = "File uploaded successfully!";
        return RedirectToAction("Index");
    }
}
