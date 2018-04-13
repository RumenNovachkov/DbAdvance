namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintFriendsListCommand 
    {
        // ListFriends
        public static string Execute(User user)
        {
            using (var db = new PhotoShareContext())
            {
                var userFrineds = db.Friendships.Where(u => u.UserId == user.Id).Select(f => f.Friend.Username).ToArray();

                if (user == null)
                {
                    throw new ArgumentException($"User {user.Username} not found!");
                }

                var builder = new StringBuilder();

                if (userFrineds.Length == 0)
                {
                    builder.AppendLine($"No friends for this user. :(");
                }
                else
                {
                    builder.AppendLine("Friends:");
                    foreach (var friend in userFrineds)
                    {
                        builder.AppendLine($"-[{friend}]");
                    }
                }

                return builder.ToString().Trim();
            }
        }
    }
}
