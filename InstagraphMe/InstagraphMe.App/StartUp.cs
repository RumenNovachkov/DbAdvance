using InstagraphMe.Data;
using InstagraphMe.Models;
using Newtonsoft.Json;
using InstagraphMe.DataProcessor;
using System;
using System.IO;
using System.Text;

namespace InstagraphMe.App
{
    public class StartUp
    {
        static void Main()
        {
            //ResetDatabase();
            ExportData();
        }

        private static void ExportData()
        {
            using (var db = new InstagraphMeContext())
            {
                Console.WriteLine(ExportData(db));
            }
        }

        private static string ExportData(InstagraphMeContext db)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append(Serializer.GetUncommentedPosts(db));
            sb.Append(Serializer.GetPopularUsers(db));

            return sb.ToString().Trim();
        }

        private static void ResetDatabase()
        {
            using (var db = new InstagraphMeContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                Console.WriteLine(ImportData(db));
            }
        }

        public static string ImportData(InstagraphMeContext db)
        {
            StringBuilder sb = new StringBuilder();

            var picturesJson = File.ReadAllText("Import/pictures.json");
            //sb.AppendLine(Deserializer.ImportPictures(db, picturesJson));
            var userJson = File.ReadAllText("Import/users.json");
            //sb.Append(Deserializer.ImportUsers(db, userJson));
            var followersJson = File.ReadAllText("Import/users_followers.json");
            //sb.Append(Deserializer.ImportedFollowers(db, followersJson));
            var postsXml = File.ReadAllText("Import/posts.xml");
            //sb.Append(Deserializer.ImportPosts(db, postsXml));
            var commentsXml = File.ReadAllText("Import/comments.xml");
            sb.Append(Deserializer.ImportComments(db, commentsXml));

            return sb.ToString().Trim();
        }
        

        
    }
}
