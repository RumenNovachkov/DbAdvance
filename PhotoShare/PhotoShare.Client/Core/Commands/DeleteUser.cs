namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Data;
    using PhotoShare.Models;

    public class DeleteUser
    {
        // DeleteUser
        public static string Execute(User user)
        {
            using (var db = new PhotoShareContext())
            {
                db.Users.SingleOrDefault(u => u.Id == user.Id).IsDeleted = true;
                
                db.SaveChanges();

                return $"User {user.Username} was deleted from the database!";
            }
        }
    }
}
