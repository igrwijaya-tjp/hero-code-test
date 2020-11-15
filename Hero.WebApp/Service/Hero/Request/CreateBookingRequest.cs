using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hero.WebApp.DataModel.Hero;
using Newtonsoft.Json;

namespace Hero.WebApp.Service.Hero.Request
{
    public class CreateBookingRequest
    {
        #region Fields

        private ICollection<BookingProductModel> _bookingProducts;

        #endregion Fields

        #region Properties

        [JsonProperty(PropertyName = "bookingProducts")]
        public ICollection<BookingProductModel> BookingProducts
        {
            get { return this._bookingProducts ??= new Collection<BookingProductModel>(); }
        }

        #endregion Properties
    }
}