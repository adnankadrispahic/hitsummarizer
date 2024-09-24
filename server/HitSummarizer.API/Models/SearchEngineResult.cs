namespace HitSums.API.Models
{
    public record Error(string Message);

    public class SearchEngineResult
    {
        public required string EngineName { get; set; }
        public required string Word { get; set; }
        public long Hits { get; set; }
        public Error? Error { get; set; }
    }
}
