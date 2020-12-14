using System;
using Xunit;
using FacebookWall;
using FacebookWallDataAccessLibrary.Models;
using Moq;
using FacebookWall.Pages;
using Autofac.Extras.Moq;
using Microsoft.EntityFrameworkCore;
using FacebookWallDataAccessLibrary.DataAccess;
using System.Threading.Tasks;
using System.Linq;

namespace FacebookWall.Tests
{
    public class IndexModelTests
    {
        [Fact]
        public async Task OnPostAsync_ShouldAddPostToContext()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<WallContext>()
                .UseInMemoryDatabase(databaseName: "IndexWallDataBase")
                .Options;

            using (var context = new WallContext(options))
            {
                var post = new Post() { Body = "Test post" };
                string personName = "Post Test Name";
                //Act
                IndexModel indexModel = new IndexModel(context);
                indexModel.PersonName = personName;
                indexModel.Post = post;
                await indexModel.OnPostAsync();

                //Assert
                Assert.Equal(personName, context.People.Where(p => p.Name == personName).FirstOrDefault().Name);
                Assert.Equal(post, context.People.Where(p => p.Name == personName).FirstOrDefault().Posts[0]);
                Assert.Equal(post, context.Posts.Where(p => p.Id == post.Id).FirstOrDefault());
            }
        }     
    }
}
