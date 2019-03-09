using System.Collections.Generic;

namespace Imgup
{
    interface ICommand
    {
        IEnumerable<string> Flags { get; set; }
        IEnumerable<string> Args { get; set; }

        void Execute();
    }
}