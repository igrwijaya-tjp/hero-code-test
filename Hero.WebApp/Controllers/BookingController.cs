using System;
using System.Threading.Tasks;
using Hero.WebApp.DataModel.Hero;
using Hero.WebApp.Request.Booking;
using Hero.WebApp.Service.Hero;
using Hero.WebApp.Service.Hero.Request;
using Hero.WebApp.Service.Hero.Response;
using Microsoft.AspNetCore.Mvc;

namespace Hero.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : HeroBaseController
    {
        #region Fields

        private readonly HeroApiManager _heroApiManager;

        #endregion Fields

        #region Constructors

        public BookingController(HeroApiManager heroApiManager)
        {
            this._heroApiManager = heroApiManager;
        }

        #endregion Constructors

        #region Public Methods

        [HttpPost]
        public async Task<IActionResult> Proceed([FromBody] ProceedBookingRequest request)
        {
            var createPaxResponse = await this.CreatePax(request);
            if (createPaxResponse.IsError())
            {
                return this.ErrorResponseResult(createPaxResponse);
            }

            var createBookingResponse = await this.CreateBookingAsync(request, createPaxResponse.Model);
            if (createBookingResponse.IsError())
            {
                return this.ErrorResponseResult(createBookingResponse);
            }

            var getTotalPriceResponse = await this.GetTotalPriceAsync(request.ProductId, request.BookDate);
            if (getTotalPriceResponse.IsError())
            {
                return this.ErrorResponseResult(getTotalPriceResponse);
            }

            var createPaymentResponse = await this.CreatePaymentAsync(createBookingResponse.Model, getTotalPriceResponse.Model);
            if (createPaymentResponse.IsError())
            {
                return this.ErrorResponseResult(createPaymentResponse);
            }

            var finalizeBookingResponse = await this.FinalizeBookingAsync(createBookingResponse.Model);
            if (finalizeBookingResponse.IsError())
            {
                return this.ErrorResponseResult(finalizeBookingResponse);
            }

            return this.SuccessResponseResult(new
            {
                BookingId = createBookingResponse.Model,
                PaxId = createPaxResponse.Model
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetVoucher([FromQuery] string bookingId, string paxId)
        {
            var getVoucherResponse = await this.GetVoucherAsync(bookingId, paxId);
            if (getVoucherResponse.IsError())
            {
                return this.ErrorResponseResult(getVoucherResponse);
            }

            return this.SuccessResponseResult(getVoucherResponse.Model.VoucherUrl);
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<GenericReadModelResponse<string>> CreatePax(ProceedBookingRequest webRequest)
        {
            var response = new GenericReadModelResponse<string>();
            var apiRequest = new CreatePaxRequest
            {
                FirstName = webRequest.FirstName,
                LastName = webRequest.LastName,
                PhoneNumber = webRequest.PhoneNumber,
                EmailAddress = webRequest.EmailAddress
            };

            var apiResponse = await this._heroApiManager.CreatePaxAsync(apiRequest);
            if (apiResponse.IsError())
            {
                response.AddErrorMessages(apiResponse.GetErrorMessages());
                return response;
            }

            response.Model = apiResponse.Model.Id;

            return response;
        }

        private async Task<GenericReadModelResponse<string>> CreateBookingAsync(ProceedBookingRequest webRequest, string paxId)
        {
            var response = new GenericReadModelResponse<string>();
            var bookingProductModel = new BookingProductModel
            {
                ProductId = webRequest.ProductId,
                DateCheckIn = webRequest.BookDate,
                PaxIds = new string[] { paxId },
                Nights = 1
            };

            var apiRequest = new CreateBookingRequest();

            apiRequest.BookingProducts.Add(bookingProductModel);

            var apiResponse = await this._heroApiManager.CreateBookingAsync(apiRequest);
            if (apiResponse.IsError())
            {
                response.AddErrorMessages(apiResponse.GetErrorMessages());
                return response;
            }

            response.Model = apiResponse.Model.Id;

            return response;
        }

        private async Task<GenericReadModelResponse<double>> GetTotalPriceAsync(int productId, string bookDate)
        {
            var response = new GenericReadModelResponse<double>();

            DateTime.TryParse(bookDate, out DateTime bookingDateTime);

            var apiResponse = await this._heroApiManager.GetProductPriceAsync(productId, bookingDateTime);
            if (apiResponse.IsError())
            {
                response.AddErrorMessages(apiResponse.GetErrorMessages());
                return response;
            }

            var model = apiResponse.Model;

            var discount = model.Commission * 0.5;
            var totalPriceAfterDiscount = model.TotalPrice - discount;

            response.Model = totalPriceAfterDiscount;

            return response;
        }

        private async Task<BaseResponse> CreatePaymentAsync(string bookingId, double amount)
        {
            var response = new BaseResponse();
            var apiRequest = new CreatePaymentRequest
            {
                BookingId = bookingId,
                Amount = amount,
                Method = 1,
                IsFinal = true
            };

            var apiResponse = await this._heroApiManager.CreatePaymentAsync(apiRequest);
            if (apiResponse.IsError())
            {
                response.AddErrorMessages(apiResponse.GetErrorMessages());
                return response;
            }

            return response;
        }

        private async Task<BaseResponse> FinalizeBookingAsync(string bookingId)
        {
            var response = new BaseResponse();
            var apiResponse = await this._heroApiManager.FinalizeBookingAsync(bookingId);

            if (apiResponse.IsError())
            {
                response.AddErrorMessages(apiResponse.GetErrorMessages());
                return response;
            }

            return response;
        }

        private async Task<GenericReadModelResponse<BookingVoucherModel>> GetVoucherAsync(string bookingId, string paxId)
        {
            var apiResponse = await this._heroApiManager.GetVoucherAsync(bookingId, paxId);

            return apiResponse;
        }

        #endregion Private Methods
    }
}