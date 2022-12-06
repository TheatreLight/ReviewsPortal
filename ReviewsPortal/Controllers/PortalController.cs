using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Data;
using ReviewsPortal.Models;
using System.Diagnostics;
using System.Dynamic;

namespace ReviewsPortal.Controllers
{
    public class PortalController : Controller
    {
        private readonly ReviewsPortalContext _context;

        public PortalController(ReviewsPortalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Review(int id)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.ReviewID == id);
            ViewData["Author"] = _context.Users.FirstOrDefault(u => u.UserID == review.UserID).UserName;
            return View(review);
        }

        public IActionResult Admin()
        {
            return View(_context.Users.ToList());
        }

        public IActionResult Group(int id)
        {
            ViewData["GroupName"] = _context.Groups.FirstOrDefault(g => g.GroupID == id).Name;
            return View(_context.Reviews.Where(
                    c => c.GroupID == id
                        ).ToList());
        }
        /*public IActionResult Movies(int id)
        {
            return View(_context.Reviews.Where(
                    c => c.GroupID == id
                        ).ToList());
        }

        public IActionResult Games(int id)
        {
            return View(_context.Reviews.Where(
                    c => c.GroupID == id
                        ).ToList());
        }

        public IActionResult Books(int id)
        {
            return View(_context.Reviews.Where(
                    c => c.GroupID == id
                        ).ToList());
        }

        public IActionResult Music(int id)
        {
            return View(_context.Reviews.Where(
                    c => c.GroupID == id
                        ).ToList());
        }

        public IActionResult Theatre(int id)
        {
            return View(_context.Reviews.Where(
                    c => c.GroupID == id
                        ).ToList());
        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}