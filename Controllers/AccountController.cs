using System.Threading.Tasks;
using MVCProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MVCProject.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger _logger;

		private async Task initAdmin()
		{
			bool x = await _roleManager.RoleExistsAsync("Admin");
			if (!x)
			{
				var role = new IdentityRole();
				role.Name = "Admin";
				await _roleManager.CreateAsync(role);           
				var user = new User();
				user.UserName = "Admin";
				var userPWD = "Admin1234;";
				var chkUser = await _userManager.CreateAsync(user, userPWD);
				if (chkUser.Succeeded) await _userManager.AddToRoleAsync(user, "Admin");
			}
		}

		public AccountController(
			UserManager<User> userManager,
			SignInManager<User> signInManager,
			RoleManager<IdentityRole> roleManager,
			ILoggerFactory loggerFactory)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_logger = loggerFactory.CreateLogger<AccountController>();
		}

		[HttpGet]
		[Route("/")]
		[Route("Login")]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Developers");
			return View();
		}

		[Route("Login")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			await initAdmin();
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					_logger.LogInformation(1, "User logged in.");
					return RedirectToAction("Index", "Developers");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View(model);
				}
			}

			return View(model);
		}

		[HttpGet]
		[Route("Register")]
		public IActionResult Register()
		{
			if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Games");
			return View();
		}

		[HttpPost]
		[Route("Register")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User { UserName = model.Login };
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					_logger.LogInformation(3, "User created a new account with password.");
					return RedirectToAction("Index", "Developers");
				}
				AddErrors(result);
			}

			return View(model);
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			await _signInManager.SignOutAsync();
			_logger.LogInformation(4, "User logged out.");
			return RedirectToAction("Index", "Developers");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		#region Helpers

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		private Task<User> GetCurrentUserAsync()
		{
			return _userManager.GetUserAsync(HttpContext.User);
		}

		#endregion
	}
}