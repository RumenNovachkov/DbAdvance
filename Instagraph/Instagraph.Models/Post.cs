﻿namespace Instagraph.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public   class Post
    {
        [Key]
        public int Id { get; set; }


        public string Caption { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}

