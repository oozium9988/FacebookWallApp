using FacebookWallDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookWall.ViewModels
{
    public class MessageboardViewModel
    {
        public IList<Comment> Comments { get; set; }
        public IList<Post> Posts { get; set; }
        public IList<Person> People { get; set; }
    }
}
