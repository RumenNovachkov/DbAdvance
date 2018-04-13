namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Utilities;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CreateAlbumCommand
    {
        // CreateAlbum <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string[] data, User user)
        {
            var albumTitle = data[1];
            var givenColour = data[2];
            string[] tags = data.Skip(3).ToArray();

            Color color = new Color();

            string[] colours = new[] { "White", "Green", "Blue", "Pink", "Yellow", "Black", "Cyan", "Magenta", "Red", "Purple", "WhiteBlackGradient" };

            var albumTags = new List<Tag>();

            using (var db = new PhotoShareContext())
            {
                var currentUser = db.Users.SingleOrDefault(u => u.Id == user.Id);

                //Check User
                if (user == null)
                {
                    throw new ArgumentException($"User {user.Username} not found!");
                }

                //Check Album Name
                if (db.Albums.Any(a => a.Name == albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!");
                }

                //Check and take Colour
                switch (givenColour.ToLower())
                {
                    case "white": color = Color.White; break;
                    case "green": color = Color.Green; break;
                    case "blue": color = Color.Blue; break;
                    case "pink": color = Color.Pink; break;
                    case "yellow": color = Color.Yellow; break;
                    case "black": color = Color.Black; break;
                    case "cyan": color = Color.Cyan; break;
                    case "magenta": color = Color.Magenta; break;
                    case "red": color = Color.Red; break;
                    case "purple": color = Color.Purple; break;
                    case "whiteblackgradient": color = Color.WhiteBlackGradient; break;
                    default:
                        throw new ArgumentException($"Color {givenColour} not found!" + Environment.NewLine +
              "You may use one of this: " + string.Join(", ", colours));
                }

                //TagsCheck
                foreach (var t in tags)
                {
                    Tag tag = db.Tags.Where(ta => ta.Name == t).FirstOrDefault();
                    if (tag == null)
                    {
                        throw new ArgumentException($"Invalid tags!" + Environment.NewLine + $"{t} not exist." + Environment.NewLine
                            + "Please add any new tags before to use them.");
                    }
                    albumTags.Add(tag);
                }

                Album album = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = color,
                    AlbumRoles = new List<AlbumRole>()
                    {
                        new AlbumRole()
                        {
                            User = currentUser,
                            Role = Role.Owner
                        }
                    },
                    AlbumTags = albumTags.Select(t => new AlbumTag()
                    {
                        Tag = db.Tags.FirstOrDefault(ct => ct.Name == t.Name)
                    })
                    .ToArray()
                };

                db.Albums.Add(album);
                db.SaveChanges();

                return $"Album {albumTitle} successfully created!";

            }

        }
    }
}
