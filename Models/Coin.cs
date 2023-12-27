namespace Models
{
    public class Coin
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? Symbol { get; set; }
        public string? Web_slug { get; set; }
        public bool? Asset_platform_id { get; set; }
        public Links? Links { get; set; }
        public Market_data? MarketData { get; set; }
    }
}
