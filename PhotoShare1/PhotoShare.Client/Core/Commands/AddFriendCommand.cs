namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Data;
    using PhotoShare.Models;
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class AddFriendCommand
    {
        // AddFriend <username1>
        public static string Execute(string[] data, User reqUser)
        {
            string desiredUsername = data[1];

            using (var db = new PhotoShareContext())
            {
                var desUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(df => df.Friend)
                    .FirstOrDefault(u => u.Username == desiredUsername);

                if (desUser == null)
                {
                    throw new ArgumentException($"{desiredUsername} not found!");
                }

                if (desUser.Id == reqUser.Id)
                {
                    throw new ArgumentException("You can't give friendship to yourself :(");
                }

                bool addingFriend = reqUser.FriendsAdded.Any(u => u.Friend.Id == desUser.Id);
                bool acceptedFriendship = desUser.FriendsAdded.Any(u => u.Friend.Id == reqUser.Id);

                if (addingFriend && !acceptedFriendship)
                {
                    throw new InvalidOperationException($"Friend request already sended to {desiredUsername}");
                }

                if (!addingFriend && acceptedFriendship)
                {
                    throw new InvalidOperationException($"{desiredUsername} has already send you friend request, You need to accept it!");
                }

                if (addingFriend && acceptedFriendship)
                {
                    throw new InvalidOperationException($"{desiredUsername} is already a friend to {reqUser.Username}");
                }

                //reqUser.FriendsAdded.Add(new Friendship()
                //{
                //    User = reqUser,
                //    Friend = desUser
                //});
                db.Users.SingleOrDefault(u => u.Username == reqUser.Username).FriendsAdded.Add(new Friendship
                {
                    Friend = desUser
                });

                db.SaveChanges();

                return $"Friend {desiredUsername} added to {reqUser.Username}";
            }
        }
    }
}
