﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Services.Helper.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAs<T>(this HttpContent httpContent)
        {
            using (var streamReader = new StreamReader(await httpContent.ReadAsStreamAsync()))
            {
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static StringContent AsStringContent(this object obj, string contentType)
        {
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return content;
        }


        public static StringContent AsJsonContent(this object obj)
        {
            return obj.AsStringContent("application/json");
        }
    }
}
