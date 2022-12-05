﻿using Microsoft.AspNetCore.Mvc;
using ReviewsPortal.Data;
using ReviewsPortal.Models;
using System.Diagnostics;

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

        public IActionResult Review()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View(_context.Users.ToList());
        }
        public IActionResult Movies()
        {
            return View();
        }

        public IActionResult Games()
        {
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }

        public IActionResult Music()
        {
            return View();
        }

        public IActionResult Theatre()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}