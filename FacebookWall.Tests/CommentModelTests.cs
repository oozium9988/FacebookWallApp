using FacebookWall.Pages;
using FacebookWallDataAccessLibrary.DataAccess;
using FacebookWallDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FacebookWall.Tests
{
    public class CommentModelTests
    {
        [Fact]
        public async Task OnPostAsync_ShouldAddCommentToContext()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<WallContext>()
                .UseInMemoryDatabase(databaseName: "CommentWallDataBase")
                .Options;

            using (var context = new WallContext(options))
            {
                var post = new Post() { Body = "Test post" };
                var reply = new Comment() { Body = "Test comment" };
                context.Posts.Add(post);
                context.SaveChanges();
                string repliersName = "Mr Test";
                //Act
                CommentController commentController = new CommentController(context);
                /*commentController.RepliersName = repliersName;
                commentModel.Post = post;
                commentModel.Reply = reply;
                await commentModel.OnPostAsync(1);*/

                //Assert
                Assert.Equal(repliersName, context.People.Where(p => p.Name == repliersName).FirstOrDefault().Name);
                Assert.Equal(reply, context.People.Where(p => p.Name == repliersName).FirstOrDefault().Comments[0]);
                Assert.Equal(reply, context.Comments.Where(r => r.Body == reply.Body).FirstOrDefault());
                var post1 = context.Posts.Where(x => x.Comments.Contains(reply)).FirstOrDefault();
                Assert.Equal(reply, post1.Comments[0]);
            }
        }
    }
}
