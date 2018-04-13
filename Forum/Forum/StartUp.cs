using Forum.Data;
using Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace Forum
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var db = new ForumDbContext();

            var tags = new[]
            {
                new Tag {Name = "C#" },
                new Tag {Name = "Programming" },
                new Tag {Name = "Prais" },
                new Tag {Name = "Microsoft" }
            };

            var postTags = new[]
            {
                new PostTag() { PostId = 1, Tag = tags[0]},
                new PostTag() { PostId = 1, Tag = tags[1]},
                new PostTag() { PostId = 1, Tag = tags[2]},
                new PostTag() { PostId = 1, Tag = tags[3]},
            };

            db.PostsTags.AddRange(postTags);

            db.SaveChanges();

            ResetDatabase(db);

            //var categories = db.Categories
            //    .Include(c => c.Posts)
            //    .ThenInclude(p => p.Author)
            //    .Include(c => c.Posts)
            //    .ThenInclude(p => p.Replies)
            //    .ToArray();

            var categories = db.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Posts = c.Posts.Select(p => new
                    {
                        Title = p.Title,
                        Content = p.Content,
                        AuthorUsername = p.Author.Username,
                        Replies = p.Replies.Select(r => new
                        {
                            Content = r.Content,
                            AuthorUsername = r.Author.Username
                        }),
                        Tags = p.PostTags.Select(t => t.Tag.Name)
                        .ToArray()
                    })
                    .ToArray()
                })
                .ToArray();

            foreach (var cat in categories)
            {
                Console.WriteLine($"Category: {cat.Name} has ({cat.Posts.Count()}) posts");
                foreach (var post in cat.Posts)
                {
                    Console.WriteLine($"--{post.Title}: {post.Content}");
                    Console.WriteLine($"by {post.AuthorUsername}");

                    Console.WriteLine(string.Join(", ", post.Tags));

                    foreach (var rep in post.Replies)
                    {
                        Console.WriteLine($"----{rep.Content} by {rep.AuthorUsername}");
                    }
                }
            }

        }

        private static void ResetDatabase(ForumDbContext db)
        {
            db.Database.EnsureDeleted();

            db.Database.Migrate();

            Seed(db);
        }

        private static void Seed(ForumDbContext db)
        {
            var users = new[]
            {
                new User("Gosho", "123"),
                new User("Pesho", "123"),
                new User("Ivan", "123"),
                new User("Mimi", "123")
            };

            db.Users.AddRange(users);

            var categories = new[]
            {
                new Category("C#"),
                new Category("Support"),
                new Category("Pyton"),
                new Category("EF KOP")

            };

            db.Categories.AddRange(categories);

            var posts = new[]
            {
                new Post("C# Rulz", "Verno", categories[0], users[0]),
                new Post("Pyton Rulz", "Pak verno", categories[2], users[1]),
                new Post("Vladi is Babaloo", "Mnogo verno", categories[1], users[2]),
                new Post("Boli me g*za", "Adski verno", categories[3], users[3])
            };

            db.Posts.AddRange(posts);

            var replies = new[]
            {
                new Reply("Vladi is Mongoloid", posts[2], users[1]),
                new Reply("Kak nqma da te boli s toq Core", posts[3], users[2])
            };

            db.Replies.AddRange(replies);

            db.SaveChanges();
        }
    }
}
