using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Common.Http
{
    public static class SerializationExtensions
    {
        public static async Task<T> DeserializeJsonContentAsync<T>(
            this JsonSerializer jsonSerializer,
            HttpResponseMessage response) where T : class
        {
            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var reader = new StreamReader(stream))
                return jsonSerializer.Deserialize<T>(new JsonTextReader(reader));
        }
    }
}
