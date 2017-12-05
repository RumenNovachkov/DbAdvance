using System;
using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.LogInCommands
{
    public class RegisterUserCommand
    {
        // RegisterUser <username> <password> <repeat-password> <email>
        public static User Execute(string[] data)
        {
            string username = data[1];
            string password = data[2];
            string repeatPassword = data[3];
            string email = data[4];

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            using (var db = new TeamBuilderContext())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    throw new InvalidOperationException($"Username {username} is already taken!");
                }
            }

            User user = new User
            {
                Username = username,
                Password = password,
                IsDeleted = false,
            };

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            Console.WriteLine("User " + user.Username + " was registered successfully!");

            return user;
        }
    }
}
