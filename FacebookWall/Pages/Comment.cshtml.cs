using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FacebookWallDataAccessLibrary.DataAccess;
using FacebookWallDataAccessLibrary.Models;

namespace FacebookWall.Pages
{
    public class CommentModel : PageModel
    {
        private readonly FacebookWallDataAccessLibrary.DataAccess.WallContext _context;

        public CommentModel(FacebookWallDataAccessLibrary.DataAccess.WallContext context)
        {
            _context = context;
        }

        public Post Post { get; set; }

        [BindProperty]
        public Reply Reply { get; set; }

        [BindProperty]
        public string RepliersName { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);

            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Person = _context.People
                                .Where(p => p.Name == RepliersName)
                                .FirstOrDefault();

            Post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            Post.Replies.Add(Reply);

            if (Person == null)
            {
                var newPerson = new Person { Name = RepliersName };
                newPerson.Comments.Add(Reply);
                _context.People.Add(newPerson);
            }
            else
            {
                Person.Comments.Add(Reply);
            }

            _context.Replies.Add(Reply);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Messageboard");
        }
    }
}
