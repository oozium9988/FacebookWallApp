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
    public class MessageboardModel : PageModel
    {
        private readonly FacebookWallDataAccessLibrary.DataAccess.WallContext _context;

        public MessageboardModel(FacebookWallDataAccessLibrary.DataAccess.WallContext context)
        {
            _context = context;
        }

        public IList<Reply> Comments { get; set; }
        public IList<Post> Posts { get; set; }
        public IList<Person> People { get; set; }
        public string[] PostMap { get; set; }
        public string[] CommentMap { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _context.Posts.ToListAsync();
            People = await _context.People.ToListAsync();
            Comments = await _context.Replies.ToListAsync();

            int maxPostId = -1;

            foreach (var item in Posts)
            {
                maxPostId = Math.Max(maxPostId, item.Id);
            }

            int maxCommentId = -1;

            foreach (var comment in Comments)
            {
                maxCommentId = Math.Max(maxCommentId, comment.Id);
            }

            PostMap = new string[maxPostId + 1];
            CommentMap = new string[maxCommentId + 1];
            
            foreach (var person in People)
            {
                foreach (var post in person.Posts)
                {
                    PostMap[post.Id] = person.Name;
                }
            }

            foreach (var person in People)
            {
                foreach (var comment in person.Comments)
                {
                    CommentMap[comment.Id] = person.Name;
                }
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int? Id = id;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Comment", new { id = Id });
        }
    }
}
