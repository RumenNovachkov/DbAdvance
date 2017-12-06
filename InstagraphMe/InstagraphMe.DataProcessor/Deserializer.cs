namespace InstagraphMe.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Linq;
    using Newtonsoft.Json;
    using Microsoft.EntityFrameworkCore;
    using InstagraphMe.Data;
    using InstagraphMe.Models;
    using System.Linq;
    using InstagraphMe.DataProcessor.DtoModels;

    public class Deserializer
    {
        private static string errorMsg = "Error: Invalid data.";
        private static string successMsg = "Successfully imported {0}";

        public static string ImportPictures(InstagraphMeContext db, string jsonString)
        {
            var deserializedPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var pictures = new List<Picture>();

            foreach (var picture in deserializedPictures)
            {
                bool isValid = !String.IsNullOrWhiteSpace(picture.Path) && picture.Size > 0;

                bool pictureExists = db.Pictures.Any(p => p.Path == picture.Path) && pictures.Any(p => p.Path == picture.Path);

                if (!isValid || pictureExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                pictures.Add(picture);
                sb.AppendLine(string.Format(successMsg, $"Picture {picture.Path}"));
            }

            db.Pictures.AddRange(pictures);
            db.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(InstagraphMeContext db, string jsonString)
        {
            var deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var users = new List<User>();

            foreach (var userDto in deserializedUsers)
            {
                bool isValid = !String.IsNullOrWhiteSpace(userDto.UserName) && userDto.UserName.Length <= 30
                    && !String.IsNullOrWhiteSpace(userDto.Password) && userDto.Password.Length <= 20
                    && !String.IsNullOrWhiteSpace(userDto.ProfilePicture);

                bool isValidPath =  db.Pictures.Any(p => p.Path == userDto.ProfilePicture);

                bool userExists = users.Any(u => u.Username == userDto.UserName);

                if (!isValid || !isValidPath  || userExists)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var picture = db.Pictures.Where(p => p.Path == userDto.ProfilePicture).SingleOrDefault();
                users.Add(new User()
                {
                    Username = userDto.UserName,
                    Password = userDto.Password,
                    ProfilePicture = picture
                });
                sb.AppendLine(string.Format(successMsg, $"User {userDto.UserName}"));
            }
            db.Users.AddRange(users);
            db.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPosts(InstagraphMeContext db, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var posts = new List<Post>();

            StringBuilder sb = new StringBuilder();

            var elements = xDoc.Root.Elements();

            foreach (var e in elements)
            {
                string userNameX = e.Element("user").Value;
                bool isValidUser = db.Users.Any(u => u.Username == userNameX);

                string picturePathX = e.Element("picture").Value;
                bool isValidPath = db.Pictures.Any(p => p.Path == picturePathX);

                string captionsX = e.Element("caption").Value;
                bool isValidCaption = string.IsNullOrWhiteSpace(captionsX);

                if (!isValidUser || !isValidPath || isValidCaption)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var user = db.Users.Where(u => u.Username == userNameX).SingleOrDefault();
                var picture = db.Pictures.Where(p => p.Path == picturePathX).SingleOrDefault();

                posts.Add(new Post()
                {
                    User = user,
                    Picture = picture,
                    Caption = captionsX,
                });
                sb.AppendLine(string.Format(successMsg, $"Post {captionsX}."));

            }

            db.Posts.AddRange(posts);
            db.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportComments(InstagraphMeContext db, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var comments = new List<Comment>();

            StringBuilder sb = new StringBuilder();

            var elements = xDoc.Root.Elements();

            foreach (var e in elements)
            {
                string userNameX = e.Element("user")?.Value;
                bool isValidUser = db.Users.Any(u => u.Username == userNameX);

                string postIdX = e.Element("post")?.Attribute("id").Value;
                int postId = -1;
                bool isInt = Int32.TryParse(postIdX, out postId);
                bool isValidPost = db.Posts.Any(p => p.Id == postId);

                string content = e.Element("content").Value;
                bool isValidCaption = string.IsNullOrWhiteSpace(content);

                if (!isValidUser || !isValidPost || isValidCaption)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var user = db.Users.Where(u => u.Username == userNameX).SingleOrDefault();
                var post = db.Posts.Where(p => p.Id == postId).SingleOrDefault();

                comments.Add(new Comment()
                {
                    Post = post,
                    User = user,
                    Content = content,
                });

                sb.AppendLine(string.Format(successMsg, $"Comment {content}."));
            }

            db.Comments.AddRange(comments);
            db.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportedFollowers(InstagraphMeContext db, string jsonString)
        {
            var deserializeUsersFollowers = JsonConvert.DeserializeObject<FollowerDto[]>(jsonString);

            var usersFollowers = new List<UserFollower>();

            StringBuilder sb = new StringBuilder();

            foreach (var uf in deserializeUsersFollowers)
            {
                bool isValid = db.Users.Any(u => u.Username == uf.User) && db.Users.Any(u => u.Username == uf.Follower);
                bool isExistsDb = db.UsersFollowers.Any(fu => fu.User.Username == uf.User && fu.Follower.Username == uf.Follower);
                bool isExistsLocal = usersFollowers.Any(fu => fu.User.Username == uf.User && fu.Follower.Username == uf.Follower);

                if (!isValid || uf.User == uf.Follower || isExistsDb || isExistsLocal)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var user = db.Users.Where(u => u.Username == uf.User).SingleOrDefault();
                var follower = db.Users.Where(u => u.Username == uf.Follower).SingleOrDefault();

                var userFollower = new UserFollower()
                {
                    User = user,
                    Follower = follower
                };

                usersFollowers.Add(userFollower);
                sb.AppendLine(string.Format(successMsg, $"Follower {user.Username} to User {follower.Username}."));
            }

            db.UsersFollowers.AddRange(usersFollowers);
            db.SaveChanges();
            return sb.ToString().TrimEnd();
        }
    }
}
