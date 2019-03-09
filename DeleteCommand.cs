using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Imgup
{
    class DeleteCommand : BaseCommand
    {
        HttpClient Client;

        public DeleteCommand(IEnumerable<string> args) : base(args)
        {
            Client = new HttpClient { BaseAddress = new Uri(Constants.API_BASE_URL) };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", Constants.IMGUR_CLIENT_ID);
        }

        public override void Execute()
        {
            if (Args.Count() < 1)
            {
                ShowHelp();
                return;
            }

            var DeleteTasks = Args.Select(hash => Delete(hash));

            Task.WhenAll(DeleteTasks)
                .ContinueWith(async hashes => Store.DeleteHistoryItems(await hashes))
                .Wait();
        }

        async Task<string> Delete(string hash)
        {
            var Response = await Client.DeleteAsync($"image/{hash}");
            if (Response.IsSuccessStatusCode)
            {
                ConsoleHandler.WriteSuccess($"deleted {hash}");
                return hash;
            }
            else
            {
                ConsoleHandler.WriteFailure($"{hash} is still breathing for unknown reasons");
                return string.Empty;
            }
        }

        void ShowHelp()
        {
            HelpCommand.ShowVersion();
            
            Console.WriteLine($"\nUsage: {Constants.APP_NAME} {Constants.DELETE_CMD} [hashes...]");

            Console.WriteLine($"\nhashes:");
            Console.WriteLine("   list of deletehashes to delete from imgur");

            Console.WriteLine("\nEx:");
            Console.WriteLine($"   {Constants.APP_NAME} {Constants.DELETE_CMD} TRB7Naih22ilflc QZBbj6QnYlpIVXp");
            Console.WriteLine("\n   deleted TRB7Naih22ilflc");
            Console.WriteLine("   deleted QZBbj6QnYlpIVXp");
            Console.WriteLine();
        }
    }
}