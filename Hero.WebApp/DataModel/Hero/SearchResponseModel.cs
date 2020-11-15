using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class SearchResponseModel
    {
        #region Properties

        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "SupplierName")]
        public string SupplierName { get; set; }

        [JsonProperty(PropertyName = "FormattedAddressName")]
        public string Address { get; set; }

        #endregion Properties
    }
}