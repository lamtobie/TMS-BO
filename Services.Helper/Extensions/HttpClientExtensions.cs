using System.Net.Http.Headers;
using System.Text;

namespace Services.Helper.Extensions
{
    public static class HttpClientExtensions
    {
        public static void UseBasicAuthentication(this HttpClient client, string userName, string password)
        {
            var byteArray = Encoding.UTF8.GetBytes(userName + ":" + password);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public static void UseBearerToken(this HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void AddCustomHeader(this HttpClient client, Dictionary<string, string> customHeaders)
        {
            if (customHeaders != null)
            {
                foreach(var _header in customHeaders)
                {
                    if (client.DefaultRequestHeaders.Contains(_header.Key))
                    {
                        client.DefaultRequestHeaders.Remove(_header.Key);
                    }
                    client.DefaultRequestHeaders.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }
        }
    }
}
