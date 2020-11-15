using Newtonsoft.Json;

namespace Hero.WebApp.Service.Hero.Request
{
    public class CreatePaxRequest
    {
        #region Properties

        [JsonProperty(PropertyName = "first")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string EmailAddress { get; set; }

        [JsonProperty(PropertyName = "mobile")]
        public string PhoneNumber { get; set; }

        #endregion Properties
    }
}