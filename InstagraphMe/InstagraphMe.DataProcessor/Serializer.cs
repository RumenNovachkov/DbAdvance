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
    using System.IO;

    public class Serializer
    {
        public static string GetUncommentedPosts(InstagraphMeContext db)
        {
            var posts = db.Posts.Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .Select(p => new
            {
                Id = p.Id,
                Picture = p.Picture.Path,
                User = p.User.Username
            }).ToArray();

            var counter = posts.Length;

            var json = JsonConvert.SerializeObject(posts, Formatting.Indented);

            File.WriteAllText("Export/UncommentedPosts.json", json);

            return $"{counter} posts are not commented";
        }

        public static string GetPopularUsers(InstagraphMeContext db)
        {
            return "";
        }
    }
}
