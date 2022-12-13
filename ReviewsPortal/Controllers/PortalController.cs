using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Data;
using ReviewsPortal.Models;
using ReviewsPortal.ViewModels;
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

        public IActionResult Review()
        {
            int id = (int)TempData["ReviewId"];
            var review = _context.Reviews.Include(r => r.Comments).ThenInclude(c => c.User).FirstOrDefault(r => r.ReviewID == id);
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

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserEmail == User.Identity.Name);
            var review = new Review
            {
                UserID = user.UserID,
                GroupID = model.GroupId,
                ReviewTopic = model.Topic,
                ReviewText = model.Text
            };
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            TempData["ReviewId"] = review.ReviewID;
            return RedirectToAction("Review", "Portal");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}