using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NoteContext _context;

        public HomeController(ILogger<HomeController> logger, NoteContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {

            var list = _context.Notes.Take(4).Where(a => a.IsPublish).ToList();
            foreach (var note in list)
            {
                note.Author = _context.Authors.Find(note.Authorid);
            }
            return View(list);



        }
        public IActionResult Privacy()
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
