using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class PaymentModel
    {
        #region Properties

        [JsonProperty(PropertyName = "receipt")]
        public string Receipt { get; set; }

        #endregion Properties
    }
}