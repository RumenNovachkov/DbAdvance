namespace PhotoShare.Client.Core
{
    using System;
    using Commands;
    using System.Linq;
    using PhotoShare.Models;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters, User user)
        {
            string command = commandParameters[0].ToLower();

            string result = "";

            switch (command)
            {
                case "exit":
                    result = ExitCommand.Execute();
                    break;
                case "addtown":
                    result = AddTownCommand.Execute(commandParameters);
                    break;
                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParameters, user);
                    break;
                case "deleteuser":
                    result = DeleteUser.Execute(user);
                    return "DeletedUser";
                case "addtag":
                    result = AddTagCommand.Execute(commandParameters);
                    break;
                case "addfriend":
                    result = AddFriendCommand.Execute(commandParameters, user);
                    break;
                case "acceptfriend":
                    result = AcceptFriendCommand.Execute(commandParameters, user);
                    break;
                case "listfriends":
                    result = PrintFriendsListCommand.Execute(user);
                    break;
                case "createalbum":
                    result = CreateAlbumCommand.Execute(commandParameters, user);
                    break;
                case "sharealbum":
                    result = ShareAlbumCommand.Execute(commandParameters);
                    break;
                case "uploadpicture":
                    result = UploadPictureCommand.Execute(commandParameters);
                    break;
                case "addtagto":
                    result = AddTagToCommand.Execute(commandParameters);
                    break;
                case "logout":
                    result = "Logout";
                    break;
                //LogOut Coomand:
                default:
                    throw new InvalidOperationException($"Command {command} not valid!");
            }

            return result;
        }
    }
}
