using System.Text.Json.Serialization;

namespace Hero.WebApp.DataModel.Hero
{
    public class SearchResponseModel
    {
        #region Properties

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("SupplierName")]
        public string SupplierName { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        #endregion Properties
    }
}