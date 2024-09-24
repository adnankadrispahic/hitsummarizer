namespace HitSums.API.Models
{
    public class SearchEngineSummarizedResult
    {
        public required string EngineName { get; set; }
        public required string Query { get; set; }
        public long TotalHits { get; set; }
        public List<Error?>? Errors { get; set; }
    }
}
