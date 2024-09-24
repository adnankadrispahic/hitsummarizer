namespace HitSums.API.Models
{
    public record WebPages(long TotalEstimatedMatches);

    public class BingSearchResponse
    {
        public required WebPages WebPages { get; set; }
    }
}
