using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacebookWallDataAccessLibrary.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; } = new List<Post>();

        public virtual List<Reply> Comments { get; set; } = new List<Reply>();
    }
}
