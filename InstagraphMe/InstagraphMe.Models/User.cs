﻿namespace InstagraphMe.Models
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public int? ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }

        public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();

        public ICollection<UserFollower> UsersFollower { get; set; } = new List<UserFollower>();

        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
