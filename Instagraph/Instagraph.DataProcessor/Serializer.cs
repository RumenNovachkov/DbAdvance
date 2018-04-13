using System;

using Instagraph.Data;
using System.Linq;
using Newtonsoft.Json;
using Instagraph.DataProcessor.DtoModels;
using AutoMapper;
using System.Collections.Generic;
using Instagraph.Models;
using AutoMapper.QueryableExtensions;
using System.Xml.Linq;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var unComPosts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .Select(p => new
                {
                    Id = p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(unComPosts, Formatting.Indented);

            return json;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var users = context.Users
                .Where(user => user.Posts.Any(post => post.Comments.Any(comment => user.Followers.Any(follower => follower.FollowerId == comment.UserId))))
                .OrderBy(u => u.Id)
                .ProjectTo<PopularUserDto>()
                .ToArray();

            string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);

            return jsonString;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
               .Select(u => new
               {
                   Username = u.Username,
                   PostsCommentCount = u.Posts.Select(p => p.Comments.Count).ToArray()
               });

            var userDtos = new List<UserTopPostDto>();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach (var u in users)
            {
                int mostComments = 0;
                if (u.PostsCommentCount.Any())
                {
                    mostComments = u.PostsCommentCount.OrderByDescending(c => c).First();
                }

                var userDto = new UserTopPostDto()
                {
                    Username = u.Username,
                    MostComments = mostComments
                };

                userDtos.Add(userDto);
            }

            userDtos = userDtos.OrderByDescending(u => u.MostComments)
                .ThenBy(u => u.Username).ToList();

            foreach (var u in userDtos)
            {
                xDoc.Root.Add(new XElement("user",
                    new XElement("Username", u.Username),
                    new XElement("MostComments", u.MostComments)
                    ));
            }

            string result = xDoc.ToString();
            return result;
        }
    }
}
