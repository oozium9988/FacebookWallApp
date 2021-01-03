using FacebookWall.ViewModels;
using FacebookWallDataAccessLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookWall.Controllers
{
    public class HomeController : Controller
    {

        private readonly FacebookWallDataAccessLibrary.DataAccess.WallContext _context;

        public HomeController(FacebookWallDataAccessLibrary.DataAccess.WallContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Post,PersonName")] IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(indexViewModel);
            }

            var Person = _context.People
                                .Where(p => p.Name == indexViewModel.PersonName)
                                .FirstOrDefault();

            if (Person == null)
            {
                var newPerson = new Person { Name = indexViewModel.PersonName };
                newPerson.Posts.Add(indexViewModel.Post);
                _context.People.Add(newPerson);
            }
            else
            {
                Person.Posts.Add(indexViewModel.Post);
            }

            _context.Posts.Add(indexViewModel.Post);

            await _context.SaveChangesAsync();

            return RedirectToAction("Messageboard", "Messageboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
