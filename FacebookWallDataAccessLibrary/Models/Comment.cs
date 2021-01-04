using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacebookWallDataAccessLibrary.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }
        public DateTime DateAndTime { get; set; } = DateTime.Now;
        public int? PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int? PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
