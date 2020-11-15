using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class PaxModel
    {
        #region Properties

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        #endregion Properties
    }
}