using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MarkoStudio.Twist.Common.Http
{
    public class JsonContent : StringContent
    {
        public JsonContent(string content)
            : base(content, Encoding.UTF8, "application/json")
        {
        }

        public static JsonContent FromObject<T>(T payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            var serializer = JsonSerializer.CreateDefault();

            using (var writer = new StringWriter(new StringBuilder()))
            {
                serializer.Serialize(writer, payload, typeof(T));
                return new JsonContent(writer.ToString());
            }
        }
    }
}
