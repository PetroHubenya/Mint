namespace Models
{
    public class Coin
    {
        /*
         * https://api.coingecko.com/api/v3/coins/markets?vs_currency=Usd&order=market_cap_desc&per_page=1&page=1
         * 
         * [{
         * "id":"bitcoin",
         * "symbol":"btc",
         * "name":"Bitcoin",
         * "image":"https://assets.coingecko.com/coins/images/1/large/bitcoin.png?1696501400",
         * "current_price":43425,
         * "market_cap":849966036969,
         * "market_cap_rank":1,
         * "fully_diluted_valuation":911484746771,
         * "total_volume":25831931209,
         * "high_24h":43789,
         * "low_24h":42123,
         * "price_change_24h":1114.4,
         * "price_change_percentage_24h":2.63386,
         * "market_cap_change_24h":23704790478,
         * "market_cap_change_percentage_24h":2.86892,
         * "circulating_supply":19582650.0,
         * "total_supply":21000000.0,
         * "max_supply":21000000.0,
         * "ath":69045,
         * "ath_change_percentage":-37.13954,
         * "ath_date":"2021-11-10T14:24:11.849Z",
         * "atl":67.81,
         * "atl_change_percentage":63906.04805,
         * "atl_date":"2013-07-06T00:00:00.000Z",
         * "roi":null,
         * "last_updated":"2023-12-28T03:03:21.843Z"
         * }]         
         */


        public required string Id { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public double? CurrentPrice { get; set; }
        public double? MarketCap { get; set;}
        public double? TotalVolume { get; set;}
    }
}
