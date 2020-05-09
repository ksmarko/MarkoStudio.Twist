using MarkoStudio.Twist.Common.Exceptions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MarkoStudio.Twist.Common.Http
{
    public static class HttpExtensions
    {
        public static async Task<T> Get<T>(
            this HttpClient httpClient,
            string url)
            where T : class
        {
            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            await EnsureSuccessStatusCode(response);

            var serializer = JsonSerializer.CreateDefault();

            return await serializer.DeserializeJsonContentAsync<T>(response).ConfigureAwait(false);
        }


        public static async Task<TResponse> Post<TResponse>(this HttpClient httpClient, string url, object model = null)
            where TResponse : class
        {
            var serializer = JsonSerializer.CreateDefault();

            var content = model != null ? JsonContent.FromObject(model) : null;
            var response = await httpClient.PostAsync(url, content);

            await EnsureSuccessStatusCode(response);

            return await serializer.DeserializeJsonContentAsync<TResponse>(response);
        }

        private static async Task EnsureSuccessStatusCode(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return;

            var responseBody = await responseMessage.TryReadStringAsync();

            var code = (int) responseMessage.StatusCode;

            if (code == 404 && responseMessage.RequestMessage.Method == HttpMethod.Get)
                return;

            if (code == 404)
                throw new NotFoundException(responseBody);

            if (code >= 400 && code < 500)
                throw new BadRequestException(responseBody);

            throw new UnknownException(responseBody);
        }

        private static async Task<string> TryReadStringAsync(this HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
