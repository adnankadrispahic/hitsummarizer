using HitSums.API.Models;

namespace HitSums.API.Abstractions
{
    public interface ISearchEngineClient
    {
        string EngineName { get; }
        Task<SearchEngineResult> SearchAsync(string word);
        Task<IEnumerable<SearchEngineResult>> SearchAllAsync(string[] words);
    }
}
