namespace PhotoShare.Client.Core.Commands
{
    using Microsoft.EntityFrameworkCore;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1>
        public static string Execute(string[] data, User acceptingUser)
        {
            string friendUsername = data[1];

            using (var db = new PhotoShareContext())
            {
                acceptingUser = db.Users.SingleOrDefault(u => u.Id == acceptingUser.Id);
                var friendUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fu => fu.Friend)
                    .Where(u => u.Username == friendUsername)
                    .FirstOrDefault();

                if (friendUser == null)
                {
                    throw new ArgumentException($"{friendUsername} not found!");
                }

                bool acceptingFriendship = acceptingUser.FriendsAdded.Any(u => u.Friend.Id == friendUser.Id);
                bool requestingFriendship = friendUser.FriendsAdded.Any(u => u.Friend.Id == acceptingUser.Id);

                if (acceptingFriendship && requestingFriendship)
                {
                    throw new InvalidOperationException($"{friendUsername} is already a friend to {acceptingUser.Username}.");
                }

                if (!requestingFriendship && !acceptingFriendship)
                {
                    throw new InvalidOperationException($"{friendUsername} has not added {acceptingUser.Username} as a friend!");
                }

                if (!requestingFriendship && acceptingFriendship)
                {
                    throw new InvalidOperationException($"{acceptingUser.Username} has send Friend request. {friendUsername} need to accept it!");
                }

                //acceptingUser.FriendsAdded.Add(new Friendship()
                //{
                //    User = acceptingUser,
                //    Friend = friendUser
                //});
                db.Users.SingleOrDefault(u => u.Id == acceptingUser.Id).FriendsAdded.Add(new Friendship()
                {
                    Friend = friendUser
                });

                db.SaveChanges();

                return $"{acceptingUser.Username} accepted {friendUsername} as a friend.";
            }
        }
    }
}
