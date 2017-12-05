namespace TeamBuilder.App.Core.LogInCommands
{
    using System;
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class LogInCommand
    {
        // LogIn <username> <password>
        public static User Execute(string[] data, User user)
        {
            var userName = data[1];
            var passWord = data[2];

            using (var db = new TeamBuilderContext())
            {
                user = db.Users.Where(u => u.Username == userName).SingleOrDefault();

                if (user == null)
                {
                    throw new ArgumentException("There is no such User, You may create a new Account with \"RegisterUser\"");
                }

                if (user.Password != passWord)
                {
                    throw new ArgumentException($"Wrong Password, please try again.");
                }

                Console.WriteLine($"Welcome {userName}");
                return user;
            }
        }
    }
}
