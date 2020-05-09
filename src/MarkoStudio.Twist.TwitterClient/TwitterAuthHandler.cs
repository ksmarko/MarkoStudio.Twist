using Microsoft.Extensions.Options;
using OAuth;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MarkoStudio.Twist.TwitterClient;

namespace MarkoStudio.Twist.TwitterClient
{
    internal class TwitterAuthHandler : DelegatingHandler
    {
        private readonly TwitterAuthOptions _auth;

        public TwitterAuthHandler(IOptions<TwitterAuthOptions> options)
        {
            _auth = options?.Value ?? throw new ArgumentNullException(nameof(options));

            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = new OAuthRequest
            {
                Method = request.Method.Method,
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = _auth.ConsumerKey,
                ConsumerSecret = _auth.ConsumerSecret,
                RequestUrl = request.RequestUri.ToString(),
                Version = "1.0a"
            };
            var auth = client.GetAuthorizationHeader();

            request.Headers.Add("Authorization", auth);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
