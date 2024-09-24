namespace HitSums.API.Models
{
    public record SearchInformation(string TotalResults);

    public class GoogleSearchResponse
    {   
        public required SearchInformation SearchInformation {  get; set; }
    }
}
