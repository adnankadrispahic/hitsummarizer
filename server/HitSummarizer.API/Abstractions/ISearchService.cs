using HitSums.API.Models;

namespace HitSums.API.Abstractions
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchEngineSummarizedResult>> SearchAllEnginesAsync(string query);
    }
}
