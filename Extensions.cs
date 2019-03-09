using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Imgup
{
    static class JsonConverter
    {
        internal static string Serialize<T>(T obj)
        {
            byte[] Json;
            using (var Stream = new MemoryStream())
            {
                var Serializer = new DataContractJsonSerializer(typeof(T));
                Serializer.WriteObject(Stream, obj);
                Json = Stream.ToArray();
                Stream.Close();
            }
            return Encoding.UTF8.GetString(Json, 0, Json.Length);
        }

        internal static T Deserialize<T>(string json) where T : class
        {
            T Result;
            using (var Stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var Serializer = new DataContractJsonSerializer(typeof(T));
                Result = Serializer.ReadObject(Stream) as T;
                Stream.Close();
            }
            return Result;
        }
    }
}