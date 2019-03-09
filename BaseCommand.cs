using System.Collections.Generic;
using System.Linq;

namespace Imgup
{
    abstract class BaseCommand : ICommand
    {
        public IEnumerable<string> Flags { get; set; }
        public IEnumerable<string> Args { get; set; }

        public BaseCommand(IEnumerable<string> args)
        {
            Flags = args.Where(a => a.StartsWith("-") && a.Length == 2);
            Args = args.Except(Flags);
        }

        public abstract void Execute();
    }
}