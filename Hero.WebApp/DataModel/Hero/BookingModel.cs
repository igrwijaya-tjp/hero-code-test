using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class BookingModel
    {
        #region Properties

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "created")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "rrp")]
        public decimal PricePerPax { get; set; }

        [JsonProperty(PropertyName = "paid")]
        public decimal Paid { get; set; }

        [JsonProperty(PropertyName = "payable")]
        public decimal Payable { get; set; }

        [JsonProperty(PropertyName = "commission")]
        public decimal Commission { get; set; }

        [JsonProperty(PropertyName = "adjustment")]
        public decimal Adjustment { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        #endregion Properties
    }
}