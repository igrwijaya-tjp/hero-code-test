using System;
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
                return this.ErrorResponseResult("Keyword cannot empty");
            }

            var searchResponse = await this._heroApiManager.SearchAsync(keyword);
            if (searchResponse.IsError())
            {
                return this.ErrorResponseResult(searchResponse);
            }

            return this.SuccessResponseResult(searchResponse.Models);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule([FromQuery] int id, string startDate)
        {
            DateTime.TryParse(startDate, out DateTime bookingDateTime);

            var searchResponse = await this._heroApiManager.GetScheduleAsync(id, bookingDateTime);
            if (searchResponse.IsError())
            {
                return this.ErrorResponseResult(searchResponse);
            }

            return this.SuccessResponseResult(searchResponse.Models);
        }

        [HttpPost]
        public async Task<IActionResult> GetPrice([FromQuery] int id, string dateCheckIn, int nights)
        {
            DateTime.TryParse(dateCheckIn, out DateTime bookingDateTime);

            var searchResponse = await this._heroApiManager.GetProductPriceAsync(id, bookingDateTime, nights);
            if (searchResponse.IsError())
            {
                return this.ErrorResponseResult(searchResponse);
            }

            var model = searchResponse.Model;

            var discount = model.Commission * (50/100);
            var totalPriceAfterDiscount = model.TotalPrice - discount;

            return this.SuccessResponseResult(new {
                Discount = discount,
                TotalPrice = totalPriceAfterDiscount,
                Currency = model.CurrencyIso
            });
        }

        #endregion Public Methods
    }
}