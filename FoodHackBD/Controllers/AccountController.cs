using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // REGISTER GET
    public IActionResult Register() => View();

    // REGISTER POST
    [HttpPost]
    public async Task<IActionResult> Register(ApplicationUser model, string password)
    {
        if (!ModelState.IsValid) return View(model);

        var newUser = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            HouseholdSize = model.HouseholdSize,
            DietPreference = model.DietPreference,
            Budget = model.Budget,
            Location = model.Location
        };

        var result = await _userManager.CreateAsync(newUser, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(newUser, false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var err in result.Errors)
            ModelState.AddModelError("", err.Description);

        return View(model);
    }

    // LOGIN GET
    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home"); // already logged in

        return View();
    }


    // LOGIN POST
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        // 1. Check if user exists
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError("", "User not found. Please register first.");
            return View();
        }

        // 2. Check password and sign-in
        var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Invalid password. Try again.");
        return View();
    }


    // LOGOUT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }


    // PROFILE GET
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
    }

    // PROFILE POST
    [HttpPost]
    public async Task<IActionResult> Profile(ApplicationUser model)
    {
        var user = await _userManager.GetUserAsync(User);

        user.FullName = model.FullName;
        user.HouseholdSize = model.HouseholdSize;
        user.DietPreference = model.DietPreference;
        user.Budget = model.Budget;
        user.Location = model.Location;

        await _userManager.UpdateAsync(user);
        // Success notification
        TempData["SuccessMessage"] = "Profile updated successfully!";

        return RedirectToAction("Profile");
    }
}
