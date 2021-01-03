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

        public Reply Reply { get; set; }

        public string RepliersName { get; set; }

        public string PostersName { get; set; }
    }
}
