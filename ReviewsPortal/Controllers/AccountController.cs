using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Data;
using ReviewsPortal.ViewModels;
using ReviewsPortal.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ReviewsPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly ReviewsPortalContext _context;
        public AccountController(ReviewsPortalContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.UserEmail == model.Email && u.Password == model.Password);
                if (user != null && user.UserName == "admin" && user.Password == "admin")
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Admin", "Account");
                }
                if (user != null)
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Profile", "Account", user);
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == model.Email && u.Password == model.Password);
            if (user == null)
            {
                _context.Users.Add(new User { UserName = model.Name, UserEmail = model.Email, Password = model.Password });
                await _context.SaveChangesAsync();
                await Authenticate(model.Email);
                user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == model.Email && u.Password == model.Password);
                return RedirectToAction("Profile", "Account", user);
            }
            else
            {
                ModelState.AddModelError("", "User already registered");
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Profile(User user)
        {
            return View(user);
        }

        [Authorize(Policy = "Admin")]
        public IActionResult Admin()
        {
            var user = _context.Users.ToList();
            return View(user);
        }

        private async Task Authenticate(string userEmail)
        {
            var claims = new List<Claim>
            {
                new Claim("userEmail", userEmail)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultNameClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
