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
                if (user != null)
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Profile", "Account", user.UserName);
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
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == model.Email && u.Password == model.Password);
                if (user == null) 
                {
                    _context.Users.Add(new User { UserName = model.Name, UserEmail = model.Email, Password = model.Password});
                    await _context.SaveChangesAsync();
                    await Authenticate(model.Email);
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "User already registered");
                }
            }
            return View(model);
        }

        [Authorize]
        public IActionResult Profile(string username)
        {
            ViewData["CurrentUser"] = username;
            return View();
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
