namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var albumName = data[1];
            var pictureTitle = data[2];
            var picturePath = data[3];
        
            using (var db = new PhotoShareContext())
            {
                var album = db.Albums.Where(a => a.Name == albumName).SingleOrDefault();
        
                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }
        
                if (db.Pictures.Where(p => p.Album == album).Any(p => p.Title == pictureTitle))
                {
                    throw new ArgumentException($"Photo with such name already exists in {albumName}");
                }

                var picture = new Picture()
                {
                    Album = album,
                    Title = pictureTitle,
                    Path = picturePath
                };

                db.Pictures.Add(picture);
                db.SaveChanges();

                return $"Picture {pictureTitle} added to {albumName}!";
            }
        }
    }
}
