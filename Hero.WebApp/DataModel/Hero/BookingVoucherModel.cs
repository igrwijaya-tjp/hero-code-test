using Newtonsoft.Json;

namespace Hero.WebApp.DataModel.Hero
{
    public class BookingVoucherModel
    {
        #region Properties

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "voucherUrl")]
        public string VoucherUrl { get; set; }

        #endregion Properties
    }
}