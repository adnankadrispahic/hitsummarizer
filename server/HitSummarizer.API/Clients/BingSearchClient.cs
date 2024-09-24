using HitSums.API.Abstractions;
using HitSums.API.Configurations;
using HitSums.API.Models;
using Microsoft.Extensions.Options;

namespace HitSums.API.Clients
{
    public class BingSearchClient : ISearchEngineClient
    {
        public string EngineName => "Bing";

        private readonly HttpClient _httpClient;
        private readonly BingSearchClientOptions _options;
        private readonly ILogger<BingSearchClient> _logger;

        public BingSearchClient(
            HttpClient httpClient, 
            IOptions<BingSearchClientOptions> options,
            ILogger<BingSearchClient> logger)
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
            string uri = $"{_options.BaseUri}/v7.0/search?q={word}";

            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _options.ApiKey);

            var result = new SearchEngineResult
            {
                EngineName = this.EngineName,
                Word = word,
                Hits = 0
            };

            try
            {
                var response = await _httpClient.GetFromJsonAsync<BingSearchResponse>(uri);

                if (response is null)
                {
                    throw new HttpRequestException();
                }

                result.Hits = response.WebPages.TotalEstimatedMatches;
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
