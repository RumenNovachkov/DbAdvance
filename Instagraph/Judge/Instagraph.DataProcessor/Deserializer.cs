using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.DtoModels;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var jsonPictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            var sb = new StringBuilder();


            var pictures = new List<Picture>();

            foreach (var p in jsonPictures)
            {
                bool isValidPath = string.IsNullOrWhiteSpace(p.Path);

                bool isValidSize = p.Size > 0;

                bool isExistPath = context.Pictures.Any(pi => pi.Path == p.Path) || pictures.Any(pi => pi.Path == p.Path);

                if (isValidPath || !isValidSize || isExistPath)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                pictures.Add(p);
                sb.AppendLine($"Successfully imported Picture {p.Path}.");
            }

            context.Pictures.AddRange(pictures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            //A user must have a valid profile picture, username and password.

            var jsonUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var sb = new StringBuilder();

            var users = new List<User>();

            foreach (var ju in jsonUsers)
            {
                bool isValidProfilPictures = context.Pictures.Any(p => p.Path == ju.ProfilePicture);
                bool isExistUsername = context.Users.Any(u => u.Username == ju.Username) || users.Any(u => u.Username == ju.Username);
                bool isValidUsername = string.IsNullOrWhiteSpace(ju.Username);
                bool isValidPassword = string.IsNullOrWhiteSpace(ju.Password) || ju.Password.Length > 20;

                if (!isValidProfilPictures || isExistUsername || isValidPassword || isValidUsername)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var picture = context.Pictures.Where(p => p.Path == ju.ProfilePicture).SingleOrDefault();

                var user = new User
                {
                    Username = ju.Username,
                    Password = ju.Password,
                    ProfilePicture = picture

                };

                sb.AppendLine($"Successfully imported User {ju.Username}.");
                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return sb.ToString().Trim();

        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var jsonFollowers = JsonConvert.DeserializeObject<UserFollowDto[]>(jsonString);

            var sb = new StringBuilder();

            var userFollowers = new List<UserFollower>();

            foreach (var jsf in jsonFollowers)
            {
                bool isValidUsers = context.Users.Any(u => u.Username == jsf.User) && context.Users.Any(u => u.Username == jsf.Follower);
                bool areSame = jsf.User == jsf.Follower;

                if (!isValidUsers || areSame)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }
                var user = context.Users.Where(u => u.Username == jsf.User).SingleOrDefault();
                var follower = context.Users.Where(u => u.Username == jsf.Follower).SingleOrDefault();

                bool areAdded = userFollowers.Any(uf => uf.UserId == user.Id && uf.FollowerId == follower.Id);

                if (areAdded)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var userFollower = new UserFollower()
                {
                    UserId = user.Id,
                    User = user,
                    FollowerId = follower.Id,
                    Follower = follower
                };


                sb.AppendLine($"Successfully imported Follower {jsf.Follower} to User {jsf.User}.");

                userFollowers.Add(userFollower);
            }
            context.UsersFollowers.AddRange(userFollowers);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var posts = new List<Post>();

            foreach (var element in elements)
            {
                string caption = element.Element("caption")?.Value;
                string username = element.Element("user")?.Value;
                string picturePath = element.Element("picture")?.Value;

                bool inputIsValid = !String.IsNullOrWhiteSpace(caption) &&
                    !String.IsNullOrWhiteSpace(username) &&
                    !String.IsNullOrWhiteSpace(picturePath);

                if (!inputIsValid)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                int? userId = context.Users.FirstOrDefault(u => u.Username == username)?.Id;
                int? pictureId = context.Pictures.FirstOrDefault(p => p.Path == picturePath)?.Id;

                if (userId == null || pictureId == null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var post = new Post()
                {
                    Caption = caption,
                    UserId = userId.Value,
                    PictureId = pictureId.Value
                };

                posts.Add(post);
                sb.AppendLine($"Successfully imported Post {caption}.");
            }

            context.Posts.AddRange(posts);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var comments = new List<Comment>();

            foreach (var e in elements)
            {
                bool existsContentX = e.Elements("content").Any();
                bool existsUserX = e.Elements("user").Any();
                bool existsPostX = e.Elements("post").Any();
                bool existsPostXId = e.Elements("post").Attributes("id").Any();

                if (!existsContentX || !existsUserX || !existsPostX || !existsPostXId)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var contentX = e.Element("content").Value;
                var userX = e.Element("user").Value;
                var postIdStr = e.Element("post").Attribute("id").Value;

                int postIdInt = 0;
                bool isIdInt = Int32.TryParse(postIdStr, out postIdInt);
                bool isPostExists = context.Posts.Any(p => p.Id == postIdInt);
                bool isUserExists = context.Users.Any(u => u.Username == userX);
                bool isContentValid = string.IsNullOrWhiteSpace(contentX);

                if (!isPostExists || !isUserExists || isContentValid)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var user = context.Users.Where(u => u.Username == userX).SingleOrDefault();

                var comment = new Comment()
                {
                    Content = contentX,
                    User = user,
                    PostId = postIdInt
                };

                comments.Add(comment);
                sb.AppendLine($"Successfully imported Comment {contentX}.");
            }

            context.Comments.AddRange(comments);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
    }
}
