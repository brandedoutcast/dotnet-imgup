using System;
using System.Collections.Generic;

namespace Imgup
{
    class ClearCommand : BaseCommand
    {
        public ClearCommand(IEnumerable<string> args) : base(args) { }

        public override void Execute()
        {
            if (!Store.HistoryExists())
            {
                Console.WriteLine("It's all clear");
                return;
            }

            var Prompt = new Prompt
            {
                Message = "It'll be tough to delete the uploaded images without the deletehashes in your history\nSo, are you sure? (y/n) ",
                AcceptMessage = "All Cleared!",
                AcceptAction = Store.ClearHistory,
                DeclineMessage = "Phew, it was a close call!"
            };

            ConsoleHandler.Prompt(Prompt);
        }
    }
}