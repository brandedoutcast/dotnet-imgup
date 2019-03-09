using System;
using System.Collections.Generic;
using System.Reflection;

namespace Imgup
{
    class HelpCommand : BaseCommand
    {
        public HelpCommand(IEnumerable<string> args) : base(args) { }

        internal static void ShowVersion()
        {
            var versionString = Assembly.GetEntryAssembly()
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                        .InformationalVersion
                                        .ToString();

            Console.WriteLine($"{Constants.APP_NAME} v{versionString}");
            Console.WriteLine("------------");
        }

        public override void Execute()
        {
            ShowVersion();

            Console.WriteLine($"\nUsage: {Constants.APP_NAME} [options]");

            Console.WriteLine("\nOptions:");
            Console.WriteLine($"   {Constants.HELP_CMD} \t Display help");
            Console.WriteLine($"   {Constants.UPLOAD_CMD} \t Upload images to imgur");
            Console.WriteLine($"   {Constants.LIST_CMD} \t List of all uploaded images");
            Console.WriteLine($"   {Constants.DELETE_CMD} \t Delete images from imgur");
            Console.WriteLine($"   {Constants.CLEAR_CMD} \t Clear uploads history");
            Console.WriteLine();
        }
    }
}