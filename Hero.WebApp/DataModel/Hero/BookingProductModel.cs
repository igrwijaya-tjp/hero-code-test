using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class BookingProductModel
    {
        #region Properties

        [JsonProperty(PropertyName = "productId")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "paxId")]
        public string[] PaxIds { get; set; }

        [JsonProperty(PropertyName = "dateCheckIn")]
        public string DateCheckIn { get; set; }

        #endregion Properties
    }
}