using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacebookWallDataAccessLibrary.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }
        public DateTime DateAndTime { get; set; } = DateTime.Now;
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
