namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string[] data)
        {
            var albumName = data[1];
            var newTag = data[2];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums.Where(a => a.Name == albumName).SingleOrDefault();
                var tag = db.Tags.Where(t => t.Name == newTag).SingleOrDefault();

                if (album == null || tag == null)
                {
                    throw new ArgumentException($"Either tag or album do not exist!");
                }
                var albumTag = new AlbumTag()
                {
                    Album = album,
                    Tag = tag
                };

                db.AlbumTags.Add(albumTag);
                db.SaveChanges();

                return $"Tag #{newTag} added to {albumName}!";
            }
        }
    }
}
