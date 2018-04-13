using PhotoShare.Data;
using PhotoShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoShare.Client.Core.LogInCommands
{
    public class ReturnCommand
    {
        //Command <UserName> <Password>
        public static User Execute(string[] data)
        {
            var username = data[1];
            var password = data[2];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == username);

                if (user.IsDeleted == false)
                {
                    throw new ArgumentException($"{username} is not Deleted");
                }

                if (user == null)
                {
                    throw new ArgumentException("There is no such a user");
                }

                if (user.Password != password)
                {
                    throw new ArgumentException("Wrong PASSWORD");
                }

                db.Users.SingleOrDefault(u => u.Id == user.Id).IsDeleted = false;
                db.SaveChanges();

                return user;
            }
        }
    }
}
