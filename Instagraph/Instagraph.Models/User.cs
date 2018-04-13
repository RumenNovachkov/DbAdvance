﻿namespace Instagraph.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        public int ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }
        public ICollection<UserFollower> Followers { get; set; }
        public ICollection<UserFollower> UsersFollowing { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}

