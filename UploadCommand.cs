using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Imgup
{
    class UploadCommand : BaseCommand
    {
        HttpClient Client;

        public UploadCommand(IEnumerable<string> args) : base(args)
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

            var DeleteLocalFiles = string.Equals("-o", Flags.FirstOrDefault());
            var UploadedImages = new List<ImageDetail>();

            Console.WriteLine();
            var Uploads = Args.Select(async file =>
            {
                if (File.Exists(file))
                {
                    var Filename = Path.GetFileName(file);

                    if ((new FileInfo(file).Length / 1024) / 1024 <= 10)
                    {
                        Console.Write($"Uploading ${Filename}...");
                        var ImageDetail = await Upload(file);
                        if (ImageDetail != null)
                        {
                            ImageDetail.UploadedOn = DateTime.Now;
                            UploadedImages.Add(ImageDetail);
                            ConsoleHandler.WriteSuccess($"\r   {Filename} uploaded to {ImageDetail.Link}");

                            if (DeleteLocalFiles)
                                File.Delete(file);
                        }
                        else
                            ConsoleHandler.WriteFailure($"\r   {Filename} failed to upload due to unknown reasons");
                    }
                    else
                        ConsoleHandler.WriteFailure($"   {Filename} failed to upload as it exceeds the size limit of 10MB");
                }
                else
                    ConsoleHandler.WriteFailure($"   {file} failed to upload as it doesn't exist");
            }).ToArray();

            Task.WaitAll(Uploads);
            Console.ResetColor();
            Console.WriteLine();
            
            if (UploadedImages.Count > 0)
                Store.AddToHistory(UploadedImages);
        }

        async Task<ImageDetail> Upload(string filepath)
        {
            var ByteContent = await File.ReadAllBytesAsync(filepath);
            var Base64Content = Convert.ToBase64String(ByteContent);
            var Response = await Client.PostAsync("image", new StringContent(Base64Content));

            if (Response.IsSuccessStatusCode)
            {
                var jsonContent = await Response.Content.ReadAsStringAsync();
                return JsonConverter.Deserialize<ImgurResponse>(jsonContent).data;
            }

            return null;
        }

        void ShowHelp()
        {
            HelpCommand.ShowVersion();

            Console.WriteLine($"\nUsage: {Constants.APP_NAME} {Constants.UPLOAD_CMD} [flags] [images...]");

            Console.WriteLine($"\nFlags:");
            Console.WriteLine($"   -o \t Delete local files after upload");

            Console.WriteLine($"\nimages:");
            Console.WriteLine("   list of image filepaths to upload on imgur");

            Console.WriteLine("\nEx:");
            Console.WriteLine($"   {Constants.APP_NAME} {Constants.UPLOAD_CMD} -o ./flower.jpg ../img/table.jpg");
            Console.WriteLine("\n   flower.jpg uploaded to http://i.imgur.com/orunSTu.jpg");
            Console.WriteLine("   table.jpg uploaded to http://i.imgur.com/itaoTyS.jpg");
            Console.WriteLine();
        }
    }
}