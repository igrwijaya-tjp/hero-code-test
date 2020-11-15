using Newtonsoft.Json;

namespace Hero.WebApp.Service.Hero.Request
{
    public class CreatePaymentRequest
    {
        #region Properties

        [JsonProperty(PropertyName = "bookingId")]
        public string BookingId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "method")]
        public int Method { get; set; }

        [JsonProperty(PropertyName = "isFinal")]
        public bool IsFinal { get; set; }

        #endregion Properties
    }
}