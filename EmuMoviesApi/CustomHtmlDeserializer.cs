using EmuMoviesApi.Models;
using RestSharp;
using RestSharp.Deserializers;
using System;

namespace EmuMoviesApi
{
    public class CustomHtmlDeserializer : IDeserializer
    {
        public T Deserialize<T>(IRestResponse response) 
        {
            if (string.IsNullOrEmpty(response.Content)) return default;

            if (typeof(T) == typeof(SearchBulkResponse))
            {
                var records = response.Content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                SearchBulkResponse searchBulkResponse = new SearchBulkResponse();
                foreach (var record in records)
                {
                    var content = record.Split(new string[] { "||" }, StringSplitOptions.None);
                    if (content.Length > 1)
                    {
                        searchBulkResponse.Images.Add(content[0], content[1]);
                    }
                }
                return (T)Convert.ChangeType(searchBulkResponse, typeof(T));
            }

            return default;
        }
    }
}
