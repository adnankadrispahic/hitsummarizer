using HitSums.API.Abstractions;
using HitSums.API.Configurations;
using HitSums.API.Models;
using Microsoft.Extensions.Options;

namespace HitSums.API.Clients
{
    public class GoogleSearchClient : ISearchEngineClient
    {
        public string EngineName => "Google";

        private readonly HttpClient _httpClient;
        private readonly GoogleSearchClientOptions _options;
        private readonly ILogger<GoogleSearchClient> _logger;

        public GoogleSearchClient(
            HttpClient httpClient, 
            IOptions<GoogleSearchClientOptions> options,
            ILogger<GoogleSearchClient> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchEngineResult>> SearchAllAsync(string[] words)
        {
            var results = await Task.WhenAll(words.Select(SearchAsync));

            return results;
        }

        public async Task<SearchEngineResult> SearchAsync(string word)
        {
            string uri = $"{_options.BaseUri}?key={_options.ApiKey}&cx={_options.SearchEngineId}&q={word}";

            var result = new SearchEngineResult 
            { 
                EngineName = this.EngineName,
                Word = word,
                Hits = 0 
            };

            try
            {
                var response = await _httpClient.GetFromJsonAsync<GoogleSearchResponse>(uri);

                if (response is null)
                {
                    throw new HttpRequestException();
                }

                result.Hits = long.TryParse(response.SearchInformation.TotalResults, out long totalResults) ? 
                    totalResults : 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                result.Error = new Error(
                    $"Failed to retrieve response from {this.EngineName} for query '{word}'");
            }  
            
            return result;
        }

    }
}
