using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Hero.WebApp.Service.Hero;
using Microsoft.AspNetCore.Mvc;

namespace Hero.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : HeroBaseController
    {
        #region Fields

        private readonly HeroApiManager _heroApiManager;

        #endregion Fields

        #region Constructors

        public ProductController(HeroApiManager heroApiManager)
        {
            this._heroApiManager = heroApiManager;
        }

        #endregion Constructors

        #region Public Methods

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return this.ErrorResponseResult(Resource.Product_KeywordEmptyMessage);
            }

            var searchResponse = await this._heroApiManager.SearchAsync(keyword);
            if (searchResponse.IsError())
            {
                return this.ErrorResponseResult(searchResponse);
            }

            return this.SuccessResponseResult(searchResponse.Models);
        }

        [HttpGet]
        public async Task<IActionResult> CheckAvailability([FromQuery] int productId, string bookDate)
        {
            DateTime.TryParse(bookDate, out DateTime bookingDateTime);

            var searchResponse = await this._heroApiManager.GetScheduleAsync(productId, bookingDateTime);
            if (searchResponse.IsError())
            {
                return this.ErrorResponseResult(searchResponse);
            }

            if (!searchResponse.Models.Any())
            {
                return this.ErrorResponseResult(Resource.Product_PriceUnavailable);
            }

            var getProductResponse = await this._heroApiManager.GetProductPriceAsync(productId, bookingDateTime);
            if (getProductResponse.IsError())
            {
                return this.ErrorResponseResult(getProductResponse);
            }

            var model = getProductResponse.Model;

            var discount = model.Commission * 0.5;
            var totalPriceAfterDiscount = model.TotalPrice - discount;

            if (totalPriceAfterDiscount <= 0)
            {
                return this.ErrorResponseResult(Resource.Product_PriceUnavailable);
            }

            return this.SuccessResponseResult(new
            {
                Message = string.Format(
                    CultureInfo.InvariantCulture,
                    Resource.Product_PriceAvailableFormat,
                    bookingDateTime.ToString("D"),
                    totalPriceAfterDiscount,
                    model.CurrencyIso)
            });
        }

        #endregion Public Methods
    }
}