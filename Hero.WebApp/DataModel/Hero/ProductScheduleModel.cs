using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class ProductScheduleModel
    {
        #region Properties

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "available")]
        public bool IsAvailable { get; set; }

        [JsonProperty(PropertyName = "minStay")]
        public int MinStay { get; set; }

        #endregion Properties
    }
}