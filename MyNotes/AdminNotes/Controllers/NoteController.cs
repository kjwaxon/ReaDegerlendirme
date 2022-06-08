using AdminNotes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdminNotes.Controllers
{
    public class NoteController : Controller
    {

        private readonly ILogger<NoteController> _logger;
        private readonly NoteContext _context;

        public NoteController(ILogger<NoteController> logger, NoteContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {

            var list = _context.Notes.ToList();

            return View(list);

        }
        public IActionResult Publish(int Id)
        {
            var note = _context.Notes.Find(Id);
            note.IsPublish = true;
            _context.Update(note);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Note()
        {
            ViewBag.Categories = _context.Categories.Select(w =>
            new SelectListItem
            {
                Text = w.Name,
                Value = w.Id.ToString()
            }
            ).ToList();
            return View();

        }


        public async Task<IActionResult> Save(Note model)
        {
            if (model != null)
            {
                var file = Request.Form.Files.First();
                //C:\Users\Asus\source\repos\MyNotes\MyNotes\wwwroot\img
                string savePath = Path.Combine("C:", "Users", "Asus", "source", "repos", "MyNotes", "MyNotes",  "wwwroot", "img");
                var fileName = $"{DateTime.Now:MMddHHmmss}.{file.FileName.Split(".").Last()}";
                var fileUrl = Path.Combine(savePath, fileName);
                using (var fileStream = new FileStream(fileUrl, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.ImagePath = fileName;
                model.Authorid = (int)HttpContext.Session.GetInt32("id");
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return Json(true);



            }



            return Json(false);



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
