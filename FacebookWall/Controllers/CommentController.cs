using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FacebookWallDataAccessLibrary.DataAccess;
using FacebookWallDataAccessLibrary.Models;
using FacebookWall.ViewModels;

namespace FacebookWall.Pages
{
    public class CommentController : Controller
    {
        private readonly FacebookWallDataAccessLibrary.DataAccess.WallContext _context;

        public CommentController(FacebookWallDataAccessLibrary.DataAccess.WallContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Comment([Bind("Comment, CommentersName")] CommentViewModel commentViewModel, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            commentViewModel.Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            commentViewModel.PostersName = commentViewModel.Post.Person.Name;

            if (commentViewModel.Post == null)
            {
                return NotFound();
            }

            return View(commentViewModel);
        }

        [HttpPost]
        [ActionName("Comment")]
        public async Task<IActionResult> CommentPost([Bind("Comment, CommentersName")] CommentViewModel commentViewModel, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var Person = _context.People
                                .Where(p => p.Name == commentViewModel.CommentersName)
                                .FirstOrDefault();

            var Post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            Post.Comments.Add(commentViewModel.Comment);

            if (Person == null)
            {
                var newPerson = new Person { Name = commentViewModel.CommentersName };
                newPerson.Comments.Add(commentViewModel.Comment);
                _context.People.Add(newPerson);
            }
            else
            {
                Person.Comments.Add(commentViewModel.Comment);
            }

            _context.Comments.Add(commentViewModel.Comment);

            await _context.SaveChangesAsync();

            return RedirectToAction("Messageboard", "Messageboard");
        }
    }
}
