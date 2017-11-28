namespace BusTicketsSystem.App.Core
{
    using System;
    using Commands;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0].ToLower();

            string result = "";

            switch (command)
            {
                case "exit": result = ExitCommand.Execute();
                    break;
                case "print-info": result = PrintInfo.Execute(commandParameters);
                    break;
                case "buy-ticket": result = BuyTicket.Execute(commandParameters);
                    break;
                case "publish-review": result = PublishReview.Execute(commandParameters);
                    break;
                case "print-reviews": result = PrintReviews.Execute(commandParameters);
                    break;
                case "change-trip-status": result = ChangeTripStatus.Execute(commandParameters);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
