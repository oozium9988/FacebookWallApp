using FacebookWallDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookWall.ViewModels
{
    public class CommentViewModel
    {
        public Post Post { get; set; }

        public Comment Comment { get; set; }

        public string CommentersName { get; set; }

        public string PostersName { get; set; }
    }
}
