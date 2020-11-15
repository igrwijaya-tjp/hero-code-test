using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class ProductPriceModel
    {
        #region Properties

        [JsonProperty(PropertyName = "rrp")]
        public double PricePerPax { get; set; }

        [JsonProperty(PropertyName = "commission")]
        public double Commission { get; set; }

        [JsonProperty(PropertyName = "totalRrp")]
        public double TotalPrice { get; set; }

        [JsonProperty(PropertyName = "currencyIso")]
        public string CurrencyIso { get; set; }

        [JsonProperty(PropertyName = "availability")]
        public int Availability { get; set; }

        #endregion Properties
    }
}