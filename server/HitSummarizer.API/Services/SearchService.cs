

using HitSums.API.Abstractions;
using HitSums.API.Models;

namespace HitSums.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly IEnumerable<ISearchEngineClient> _searchEngineClients;
            
        public SearchService(IEnumerable<ISearchEngineClient> searchEngineClients)
        {
            _searchEngineClients = searchEngineClients;
        }

        public async Task<IEnumerable<SearchEngineSummarizedResult>> SearchAllEnginesAsync(string query)
        {
            string[] words = query.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var searchTasks = _searchEngineClients.Select(async client =>
            {
                var results = await client.SearchAllAsync(words);

                return GetSummarizedResult(client, results, query);
            });

            var summarizedResults = await Task.WhenAll(searchTasks);

            return summarizedResults;
        }

        private SearchEngineSummarizedResult GetSummarizedResult(
            ISearchEngineClient client, IEnumerable<SearchEngineResult> results, string query)
        {
            long totalHits = results.Sum(r => r.Hits);

            var errors = results
                .Where(r => r.Error != null)
                .Select(r => r.Error)
                .ToList();

            return new SearchEngineSummarizedResult
            {
                EngineName = client.EngineName,
                Query = query,
                TotalHits = totalHits,
                Errors = errors
            };
        }
    }
}
