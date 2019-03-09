using System;
using System.Collections.Generic;
using System.Linq;

namespace Imgup
{
    class ListCommand : BaseCommand
    {
        public ListCommand(IEnumerable<string> args) : base(args) { }

        public override void Execute()
        {
            var History = Store.ReadHistory();
            if (History.Count() > 0)
                ConsoleHandler.WriteTable(History);
            else
                Console.WriteLine("Nothing to see here");
        }
    }
}