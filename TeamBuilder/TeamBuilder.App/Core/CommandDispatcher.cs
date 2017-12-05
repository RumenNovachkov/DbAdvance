namespace TeamBuilder.App.Core
{
    using System;
    //using Commands; - To be implemented
    using System.Linq;
    using TeamBuilder.Models;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters, User user)
        {
            string command = commandParameters[0].ToLower();

            string result = "";

            switch (command)
            {
                default:
                    break;
            }

            return result;
        }
    }
}
