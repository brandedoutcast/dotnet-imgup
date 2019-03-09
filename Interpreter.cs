using System;
using System.Collections.Generic;
using System.Linq;

namespace Imgup
{
    class Interpreter
    {
        ICommand Command;

        void Parse(string commandName, IEnumerable<string> commandArgs)
        {
            switch (commandName)
            {
                case Constants.UPLOAD_CMD:
                    Command = new UploadCommand(commandArgs);
                    break;
                case Constants.DELETE_CMD:
                    Command = new DeleteCommand(commandArgs);
                    break;
                case Constants.LIST_CMD:
                    Command = new ListCommand(commandArgs);
                    break;
                case Constants.CLEAR_CMD:
                    Command = new ClearCommand(commandArgs);
                    break;
                case Constants.HELP_CMD:
                default:
                    Command = new HelpCommand(commandArgs);
                    break;
            }
        }

        internal void Process(string[] args)
        {
            var CommandName = args.Length == 0 ? string.Empty : args[0];
            var CommandArgs = args.Length == 0 ? args : args.Skip(1);

            Parse(CommandName, CommandArgs);
            Command.Execute();
            Console.ResetColor();
        }
    }
}