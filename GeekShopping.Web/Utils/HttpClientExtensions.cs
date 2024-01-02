﻿using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue _contentType = new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(dataAsString, serializeOptions);
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            string dataAsString = JsonSerializer.Serialize(data);
            
            StringContent content = new StringContent(dataAsString);
            content.Headers.ContentType = _contentType;

            return httpClient.PostAsync(url, content);
        }
        
        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            string dataAsString = JsonSerializer.Serialize(data);
            
            StringContent content = new StringContent(dataAsString);
            content.Headers.ContentType = _contentType;

            return httpClient.PutAsync(url, content);
        }
    }
}
