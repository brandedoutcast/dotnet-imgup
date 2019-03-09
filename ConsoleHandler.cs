using System;
using System.Collections.Generic;

namespace Imgup
{
    class Prompt
    {
        internal string Message { get; set; }
        internal string AcceptMessage { get; set; }
        internal string DeclineMessage { get; set; }
        internal Action AcceptAction { get; set; }
        internal Action DeclineAction { get; set; }
    }

    static class ConsoleHandler
    {
        internal static void WriteSuccess(string message) => WriteMessage(message, ConsoleColor.Green);
        internal static void WriteFailure(string message) => WriteMessage(message, ConsoleColor.Red);

        static void WriteMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }

        internal static void WriteTable(IEnumerable<ImageDetail> details)
        {
            Console.WriteLine();
            Console.WriteLine("Id \t\t Link \t\t\t\t\t DeleteHash \t\t UploadedOn");
            Console.WriteLine("-- \t\t ---- \t\t\t\t\t ---------- \t\t ----------");

            foreach (var detail in details)
                Console.WriteLine($"{detail.Id} \t {detail.Link} \t {detail.DeleteHash} \t {detail.UploadedOn}");

            Console.WriteLine();
        }

        internal static void Prompt(Prompt prompt)
        {
            Console.Write(prompt.Message);
            var Decision = Console.ReadKey();
            Console.WriteLine();

            if (Decision.KeyChar.Equals('y') || Decision.KeyChar.Equals('Y'))
            {
                if (!string.IsNullOrWhiteSpace(prompt.AcceptMessage))
                    Console.WriteLine(prompt.AcceptMessage);

                if (prompt.AcceptAction != null)
                    prompt.AcceptAction();
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(prompt.DeclineMessage))
                    Console.WriteLine(prompt.DeclineMessage);

                if (prompt.DeclineAction != null)
                    prompt.DeclineAction();
            }
        }
    }
}