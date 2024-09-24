namespace HitSums.API.Configurations
{
    public class GoogleSearchClientOptions
    {
        public required string BaseUri { get; init; }
        public required string SearchEngineId { get; init; }
        public required string ApiKey { get; init; }
    }
}
