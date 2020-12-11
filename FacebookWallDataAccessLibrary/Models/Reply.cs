using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacebookWallDataAccessLibrary.Models
{
    public class Reply
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }
        public DateTime DateAndTime { get; set; } = DateTime.Now;
    }
}
