using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Fetch([FromQuery] string keyword)
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

            var date = DateTime.Now;
            var result = new List<object>();

            while(date < DateTime.Now.AddDays(3))
            {
                foreach (var product in searchResponse.Models)
                {
                    var scheduleResponse = await this._heroApiManager.GetScheduleAsync(product.Id, date);
                    if (scheduleResponse.IsError())
                    {
                        return this.ErrorResponseResult(scheduleResponse);
                    }

                    if (searchResponse.Models.Any())
                    {
                        var getProductResponse = await this._heroApiManager.GetProductPriceAsync(product.Id, date);
                        if (getProductResponse.IsError())
                        {
                            return this.ErrorResponseResult(getProductResponse);
                        }

                        if (getProductResponse.Model.TotalPrice > 0)
                        {
                            result.Add(new
                            {
                                Schedules = searchResponse.Models,
                                Product = getProductResponse.Model,
                                DateAvailability = date
                            });
                        }
                    }

                }

                date = date.AddDays(1);
            }

            return this.SuccessResponseResult(result);
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
                return this.ErrorResponseResult("The product not available for the selected date");
            }

            var getProductResponse = await this._heroApiManager.GetProductPriceAsync(productId, bookingDateTime);
            if (getProductResponse.IsError())
            {
                return this.ErrorResponseResult(getProductResponse);
            }

            var model = getProductResponse.Model;

            var discount = model.Commission * 0.5;
            var totalPriceAfterDiscount = model.TotalPrice - discount;

            //if (totalPriceAfterDiscount <= 0)
            //{
            //    return this.ErrorResponseResult("The product not available for the selected date");
            //}

            return this.SuccessResponseResult(new
            {
                Message = $"The product is available to book on <b>{bookingDateTime.ToString("D")}</b> with price <b>${totalPriceAfterDiscount} AUD</b>. Do you want to continue to book the product?"
            });
        }

        #endregion Public Methods
    }
}