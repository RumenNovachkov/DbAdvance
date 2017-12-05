namespace TeamBuilder.App.Core.LogInCommands
{
    using System;
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class ReturnCommand
    {
        //Command <UserName> <Password>
        public static User Execute(string[] data)
        {
            var username = data[1];
            var password = data[2];

            using (var db = new TeamBuilderContext())
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
