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

        public async Task<IActionResult> Comment([Bind("Reply, RepliersName")] CommentViewModel commentViewModel, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            commentViewModel.Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            var person = await _context.People.FirstOrDefaultAsync(p => p.Posts.Contains(commentViewModel.Post));
            commentViewModel.PostersName = person.Name;

            if (commentViewModel.Post == null)
            {
                return NotFound();
            }

            return View(commentViewModel);
        }

        [HttpPost]
        [ActionName("Comment")]
        public async Task<IActionResult> CommentPost([Bind("Reply", "RepliersName")] CommentViewModel commentViewModel, int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var Person = _context.People
                                .Where(p => p.Name == commentViewModel.RepliersName)
                                .FirstOrDefault();

            commentViewModel.Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            commentViewModel.Post.Replies.Add(commentViewModel.Reply);

            if (Person == null)
            {
                var newPerson = new Person { Name = commentViewModel.RepliersName };
                newPerson.Comments.Add(commentViewModel.Reply);
                _context.People.Add(newPerson);
            }
            else
            {
                Person.Comments.Add(commentViewModel.Reply);
            }

            _context.Replies.Add(commentViewModel.Reply);

            await _context.SaveChangesAsync();

            return RedirectToAction("Messageboard", "Messageboard");
        }
    }
}
