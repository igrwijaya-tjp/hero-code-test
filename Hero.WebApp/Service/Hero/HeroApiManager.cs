using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hero.WebApp.DataModel.Hero;
using Hero.WebApp.Service.Hero.Request;
using Hero.WebApp.Service.Hero.Response;
using Newtonsoft.Json;

namespace Hero.WebApp.Service.Hero
{
    public class HeroApiManager
    {
        private readonly HttpClient _httpClient;

        public HeroApiManager(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<GenericGetModelResponse<SearchResponseModel>> SearchAsync(string keyword)
        {
            var response = new GenericGetModelResponse<SearchResponseModel>();
            var httpTask = _httpClient.GetAsync($"search?q={keyword}&lat=-24.9922916&lng=115.224928");

            var apiResult = await SendApiRequest<ICollection<SearchResponseModel>>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Models.AddRange(apiResult);

            return response;
        }

        public async Task<GenericReadModelResponse<ProductPriceModel>> GetProductPriceAsync(int productId, DateTime dateCheckIn)
        {
            var response = new GenericReadModelResponse<ProductPriceModel>();
            var httpTask = _httpClient.PostAsync($"productpricing/{productId}?dateCheckIn={dateCheckIn.ToString("s")}", null);

            var apiResult = await SendApiRequest<ProductPriceModel>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Model = apiResult;

            return response;
        }

        public async Task<GenericGetModelResponse<ProductScheduleModel>> GetScheduleAsync(int productId, DateTime startDate)
        {
            var response = new GenericGetModelResponse<ProductScheduleModel>();
            var httpTask = _httpClient.GetAsync($"schedule/{productId}/{startDate.ToString("yyyy-MM-dd")}");

            var apiResult = await SendApiRequest<ICollection<ProductScheduleModel>>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Models.AddRange(apiResult);

            return response;
        }

        public async Task<GenericReadModelResponse<PaxModel>> CreatePaxAsync(CreatePaxRequest createPaxRequest)
        {
            var response = new GenericReadModelResponse<PaxModel>();
            var JsonBodyContent = JsonConvert.SerializeObject(createPaxRequest);

            var httpContent = new StringContent(JsonBodyContent, Encoding.UTF8, "application/json");

            var httpTask = _httpClient.PostAsync($"pax", httpContent);

            var apiResult = await SendApiRequest<PaxModel>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Model = apiResult;

            return response;
        }

        public async Task<GenericReadModelResponse<BookingModel>> CreateBookingAsync(CreateBookingRequest createBookingRequest)
        {
            var response = new GenericReadModelResponse<BookingModel>();
            var JsonBodyContent = JsonConvert.SerializeObject(createBookingRequest);
            var httpContent = new StringContent(JsonBodyContent, Encoding.UTF8, "application/json");
            var httpTask = _httpClient.PostAsync("bookings", httpContent);

            var apiResult = await SendApiRequest<BookingModel>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Model = apiResult;

            return response;
        }

        public async Task<GenericReadModelResponse<PaymentModel>> CreatePaymentAsync(CreatePaymentRequest createPaymentRequest)
        {
            var response = new GenericReadModelResponse<PaymentModel>();
            var JsonBodyContent = JsonConvert.SerializeObject(createPaymentRequest);
            var httpContent = new StringContent(JsonBodyContent, Encoding.UTF8, "application/json");
            var httpTask = _httpClient.PostAsync("payments", httpContent);

            var apiResult = await SendApiRequest<PaymentModel>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Model = apiResult;

            return response;
        }

        public async Task<GenericReadModelResponse<string>> FinalizeBookingAsync(string bookingId)
        {
            var response = new GenericReadModelResponse<string>();
            var httpTask = _httpClient.GetAsync($"bookingfinalise/{bookingId}");

            var apiResult = await SendApiRequest<string>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Model = apiResult;

            return response;
        }

        public async Task<GenericReadModelResponse<BookingVoucherModel>> GetVoucherAsync(string bookingId, string paxId)
        {
            var response = new GenericReadModelResponse<BookingVoucherModel>();
            var httpTask = _httpClient.GetAsync($"vouchers/{bookingId}/{paxId}");

            var apiResult = await SendApiRequest<BookingVoucherModel>(response, httpTask);
            if (response.IsError())
            {
                return response;
            }

            response.Model = apiResult;

            return response;
        }

        #region Private Methods

        private async Task<TResult> SendApiRequest<TResult>(BaseResponse response, Task<HttpResponseMessage> httpRequestTask)
        {
            try
            {
                var getRequestResponse = await httpRequestTask;
                var apiResponse = getRequestResponse.EnsureSuccessStatusCode();

                if (!apiResponse.IsSuccessStatusCode)
                {
                    var message = string.Format(
                        CultureInfo.InvariantCulture,
                        Resource.HeroApi_FailedResponseMessageFormat,
                        apiResponse.ReasonPhrase,
                        apiResponse.StatusCode);

                    response.AddErrorMessage(message);

                    return default;
                }

                var responseString = await getRequestResponse.Content.ReadAsStringAsync();
                var modelResult = JsonConvert.DeserializeObject<TResult>(responseString);

                return modelResult;
            }
            catch (Exception ex)
            {
                //TODO: Store exception to internal error handling (ELMAH, etc)
                response.AddErrorMessage(Resource.HeroApi_ExceptionMessage);
            }

            return default;
        }

        #endregion Private Methods
    }
}