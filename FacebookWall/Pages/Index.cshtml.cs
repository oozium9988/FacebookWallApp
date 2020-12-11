using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using FacebookWallDataAccessLibrary.DataAccess;
using FacebookWallDataAccessLibrary.Models;

namespace FacebookWall.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FacebookWallDataAccessLibrary.DataAccess.WallContext _context;

        public IndexModel(FacebookWallDataAccessLibrary.DataAccess.WallContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; }

        [BindProperty]
        public string PersonName { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Person = _context.People
                                .Where(p => p.Name == PersonName)
                                .FirstOrDefault();

            if (Person == null)
            {
                var newPerson = new Person { Name = PersonName };
                newPerson.Posts.Add(Post);
                _context.People.Add(newPerson);
            } else
            {
                Person.Posts.Add(Post);
            }

            _context.Posts.Add(Post);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Messageboard");
        }
    }
}
