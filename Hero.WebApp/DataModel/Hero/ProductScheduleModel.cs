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

        #endregion Properties
    }
}