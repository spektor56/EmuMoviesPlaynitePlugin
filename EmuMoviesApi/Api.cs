using System;
using EmuMoviesApi.Models;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace EmuMoviesApi
{
    public class Api
    {
        private string _username;
        private string _password;
        private string _productKey = "31A2EC66B7E3173AFC54DAB5F686A731330E";
        private RestClient _client = new RestClient("https://api.gamesdbase.com/");
        private bool _authenticated;

        public string Username
        {
            set => _username = value;
        }

        public string Password
        {
            set => _password = value;
        }

        public Api(string username, string password)
        {
            _client.AddHandler("text/html", () => new CustomHtmlDeserializer());
            _username = username;
            _password = password;
        }

        private async Task LoginAsync()
        {
            var request = new RestRequest("login.aspx", DataFormat.Xml);
            request.AddQueryParameter("user", _username);
            request.AddQueryParameter("api", _password);
            request.AddQueryParameter("product", _productKey);
            var response = await _client.GetAsync<LoginResponse>(request);
            if (response.Result.Success)
            {
                _authenticated = true;
                _client.AddOrUpdateDefaultParameter(new Parameter("sessionid", response.Result.Session, ParameterType.QueryString));
            }
        }

        private async Task<T> GetResponseAsync<T>(RestRequest request)
        {
            if (!_authenticated)
            {
                await LoginAsync();
            }

            var response = await _client.ExecuteAsync(request);
            var xmlSerializer = new XmlSerializer(typeof(T));
            IResults result;

            using (var reader = new StringReader("<ROOTNODE>" + response.Content + @"</ROOTNODE>"))
            {
                result = (IResults)xmlSerializer.Deserialize(reader);
            }

            if (result != null && result.Results != null && result.Results.Result != null &&
                result.Results.Result.Success == false && result.Results.Result.MSG != null &&
                (result.Results.Result.MSG.Equals("First pass validation error", System.StringComparison.OrdinalIgnoreCase) ||
                result.Results.Result.MSG.Equals("Session has expired. Please login.", System.StringComparison.OrdinalIgnoreCase))
                )
            {
                _authenticated = false;
                await LoginAsync();

                response = await _client.ExecuteAsync(request);

                using (var reader = new StringReader("<ROOTNODE>" + response.Content + @"</ROOTNODE>"))
                {
                    result = (IResults)xmlSerializer.Deserialize(reader);
                }
            }

            return (T)result;
        }

        private async Task<T> GetResponsePostAsync<T>(RestRequest request)
        {
            if (!_authenticated)
            {
                await LoginAsync();
            }
            
            var response = await _client.ExecutePostAsync<T>(request);
            if (response.Data != null)
            {
                return response.Data;
            }

            var xmlSerializer = new XmlSerializer(typeof(T));
            IResult result;
            using (var reader = new StringReader("<ROOTNODE>" + response.Content + @"</ROOTNODE>"))
            {
                result = (IResult)xmlSerializer.Deserialize(reader);
            }

            if (result != null && result.Result != null &&
                result.Result.Success == false && result.Result.MSG != null &&
                (result.Result.MSG.Equals("First pass validation error", System.StringComparison.OrdinalIgnoreCase) ||
                result.Result.MSG.Equals("Session has expired. Please login.", System.StringComparison.OrdinalIgnoreCase))
                )
            {
                _authenticated = false;
                await LoginAsync();

                response = await _client.ExecutePostAsync<T>(request);
            }
            return response.Data;
        }

        public async Task<List<Media>> GetMedia()
        {
            var media = new List<Media>();
            var request = new RestRequest("getmedias.aspx", DataFormat.Xml);
            var mediaResponse = await GetResponseAsync<MediaResponse>(request);
            
            if (mediaResponse != null && mediaResponse.Medias != null && mediaResponse.Medias.Media != null)
            {
                media = mediaResponse.Medias.Media;
            }

            return media;
        }

        public async Task<List<Models.System>> GetSystems()
        {
            var systems = new List<Models.System>();
            var request = new RestRequest("getsystems.aspx", DataFormat.Xml);
            var systemsResponse = await GetResponseAsync<SystemsResponse>(request);

            if (systemsResponse != null && systemsResponse.Systems != null && systemsResponse.Systems.System != null)
            {
                systems = systemsResponse.Systems.System;
            }

            return systems;
        }

        public async Task<string> GetMediaUrl(string game, string system, MediaType mediaType)
        {
            string url = null;
            var request = new RestRequest("search.aspx", DataFormat.Xml);
            request.AddQueryParameter("search", HttpUtility.UrlEncode(game.ToLower()), false);
            request.AddQueryParameter("system", system);
            request.AddQueryParameter("media", Enum.GetName(typeof(MediaType), mediaType));

            var searchResponse = await GetResponseAsync<SearchResponse>(request);

            if (searchResponse != null && searchResponse.Results != null && searchResponse.Results.Result != null)
            {
                url = searchResponse.Results.Result.URL;
            }

            return url;
        }

        public async Task<string> GetBulkMediaUrl(List<string> games, string system, MediaType mediaType)
        {
            var request = new RestRequest("searchbulk.aspx");
            request.AddQueryParameter("testnorecords", "1");
            request.AddQueryParameter("teststruct", "1");
            request.AddQueryParameter("biggertimeout", "1");
            request.AddQueryParameter("txt", "1");
            request.AddQueryParameter("more2", "1");
            request.AddQueryParameter("system", system);
            request.AddQueryParameter("media", Enum.GetName(typeof(MediaType), mediaType));
            request.AddQueryParameter("rnd", "121551");

            //game = game.Replace("+", "%2b");
            //game = game.Replace(" ", "+");
            //game = game.ToLower();

            request.AddParameter("application/x-www-form-urlencoded", "FileNames=" + HttpUtility.UrlEncode(string.Join("\r\n",games.Select(game => game.ToLower()))), ParameterType.RequestBody);
            
            var searchResponse = await GetResponsePostAsync<SearchBulkResponse>(request); 
            
            if (searchResponse != null && searchResponse.Images.Count > 0)
            {
                foreach (var game in games)
                {
                    if (searchResponse.Images.ContainsKey(game.ToLower()))
                    {
                        return _client.BaseUrl + searchResponse.Images[game.ToLower()];
                    }
                }
            }
            
            return null;
        }
    }
}
