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
    public class MessageboardController : Controller
    {
        private readonly FacebookWallDataAccessLibrary.DataAccess.WallContext _context;

        public MessageboardController(FacebookWallDataAccessLibrary.DataAccess.WallContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Messageboard()
        {
            MessageboardViewModel messageboardViewModel = new MessageboardViewModel 
            {
                Posts = await _context.Posts.ToListAsync(),
                People = await _context.People.ToListAsync(),
                Comments = await _context.Replies.ToListAsync()
            };           

            int maxPostId = -1;

            foreach (var item in messageboardViewModel.Posts)
            {
                maxPostId = Math.Max(maxPostId, item.Id);
            }

            int maxCommentId = -1;

            foreach (var comment in messageboardViewModel.Comments)
            {
                maxCommentId = Math.Max(maxCommentId, comment.Id);
            }

            messageboardViewModel.PostMap = new string[maxPostId + 1];
            messageboardViewModel.CommentMap = new string[maxCommentId + 1];
            
            foreach (var person in messageboardViewModel.People)
            {
                foreach (var post in person.Posts)
                {
                    messageboardViewModel.PostMap[post.Id] = person.Name;
                }
            }

            foreach (var person in messageboardViewModel.People)
            {
                foreach (var comment in person.Comments)
                {
                    messageboardViewModel.CommentMap[comment.Id] = person.Name;
                }
            }

            return View(messageboardViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Messageboard(int? id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            int? Id = id;

            await _context.SaveChangesAsync();

            return RedirectToAction("Comment", "Comment", new { id = Id });
        }
    }
}
