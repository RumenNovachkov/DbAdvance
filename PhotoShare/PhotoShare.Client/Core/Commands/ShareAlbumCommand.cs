namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string[] data)
        {
            var albumId = int.Parse(data[1]);
            var otherUsername = data[2];
            var permission = data[3];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums.Where(a => a.Id == albumId).FirstOrDefault();

                if (album == null)
                {
                    throw new ArgumentException($"Album with ID: {albumId} not found!");
                }

                var user = db.Users.Where(u => u.Username == otherUsername).FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException($"User {otherUsername} not found!");
                }

                var role = new Role();

                switch (permission.ToLower())
                {
                    case "owner": role = Role.Owner; break;
                    case "viewer": role = Role.Viewer; break;
                    default: throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                        break;
                };

                var albumRole = new AlbumRole()
                {
                    Album = album,
                    User = user,
                    Role = role
                };

                db.AlbumRoles.Add(albumRole);
                db.SaveChanges();
                
                return $"Username {otherUsername} added to album {album.Name} ({permission})";
            }
        }
    }
}
