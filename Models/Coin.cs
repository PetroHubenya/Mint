namespace Models
{
    public class Coin
    {
        public required string Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public double? PriceUsd { get; set; }
    }
}
