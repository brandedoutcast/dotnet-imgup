using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Imgup
{
    static class Store
    {
        internal static void AddToHistory(IEnumerable<ImageDetail> incomingData)
        {
            var ExistingData = ReadHistory();
            var NewData = ExistingData.Concat(incomingData);
            WriteHistory(NewData);
        }

        internal static IEnumerable<ImageDetail> ReadHistory()
        {
            var Data = Enumerable.Empty<ImageDetail>();

            if (HistoryExists())
            {
                var Content = File.ReadAllText(GetHistoryFilePath());
                Data = JsonConverter.Deserialize<IEnumerable<ImageDetail>>(Content);
            }

            return Data;
        }

        internal static void ClearHistory()
        {
            if (HistoryExists())
                File.Delete(GetHistoryFilePath());
        }

        internal static void DeleteHistoryItems(IEnumerable<string> hashes)
        {
            if (HistoryExists())
            {
                var ExistingData = ReadHistory();
                var NewData = ExistingData.Where(i => !hashes.Contains(i.DeleteHash));

                WriteHistory(NewData, true);
            }
        }

        static void WriteHistory(IEnumerable<ImageDetail> data, bool force = false)
        {
            if (data.Count() == 0)
            {
                ClearHistory();
                return;
            }

            var Content = JsonConverter.Serialize(data);

            if (!force)
                File.AppendAllText(GetHistoryFilePath(), Content);
            else
                File.WriteAllText(GetHistoryFilePath(), Content);
        }

        internal static bool HistoryExists()
        {
            return File.Exists(GetHistoryFilePath());
        }

        static string GetHistoryFilePath()
        {
            var ExeDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Path.Combine(ExeDir, Constants.UPLOAD_HISTORY_FILENAME);
        }
    }
}