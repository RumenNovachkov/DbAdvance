namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data, User user)
        {
            string property = data[1].ToLower();
            string newValue = data[2];

            using (var db = new PhotoShareContext())
            {
                var currentUser = db.Users.SingleOrDefault(u => u.Id == user.Id);
                var exceptionMessage = $"Value {newValue} not valid." + Environment.NewLine;

                switch (property)
                {
                    case "password":
                        if (!newValue.Any(c => Char.IsLower(c)) || 
                            !newValue.Any(c => Char.IsDigit(c)))
                        {
                            throw new ArgumentException(exceptionMessage + "Invalid Password");
                        }
                        currentUser.Password = newValue;
                        break;
                    case "borntown":
                        var newBornTown = db.Towns.Where(t => t.Name == newValue).FirstOrDefault();
                        if (newBornTown == null)
                        {
                            throw new ArgumentException(exceptionMessage + $"Town {newValue} not found!");
                        }
                        currentUser.BornTown = newBornTown;
                        break;
                    case "currenttown":
                        var newCurrentTown = db.Towns.Where(t => t.Name == newValue).FirstOrDefault();
                        if (newCurrentTown == null)
                        {
                            throw new ArgumentException(exceptionMessage + $"Town {newValue} not found!");
                        }
                        currentUser.CurrentTown = newCurrentTown;
                        break;
                    default: throw new ArgumentException($"Property {property} not supported!");
                }

                db.SaveChanges();

                return $"User {user.Username} {property} is {newValue}.";
            }

        }
    }
}
