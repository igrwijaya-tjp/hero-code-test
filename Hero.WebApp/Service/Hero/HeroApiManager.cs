using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Hero.WebApp.DataModel.Hero;
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
            try
            {
                var getRequestResponse = await _httpClient.GetAsync($"search?q={keyword}&lat=-24.9922916&lng=115.224928");
                var apiResponse = getRequestResponse.EnsureSuccessStatusCode();

                if (!apiResponse.IsSuccessStatusCode)
                {
                    response.AddErrorMessage($"Failed to send request with reason: {apiResponse.ReasonPhrase} and status code: {apiResponse.StatusCode}");
                    return response;
                }

                var responseString = await getRequestResponse.Content.ReadAsStringAsync();
                var modelResult = JsonConvert.DeserializeObject<ICollection<SearchResponseModel>>(responseString);

                response.Models.AddRange(modelResult);
            }
            catch (Exception ex)
            {
                //TODO: Store exception to internal error handling (ELMAH, etc)

                response.AddErrorMessage($"Error when sending request");
            }

            return response;
        }

        public async Task<GenericReadModelResponse<ProductPriceModel>> GetProductPriceAsync(int productId, DateTime dateCheckIn)
        {
            var response = new GenericReadModelResponse<ProductPriceModel>();
            try
            {
                var getRequestResponse = await _httpClient.PostAsync($"productpricing/{productId}?dateCheckIn={dateCheckIn.ToString("s")}", null);
                var apiResponse = getRequestResponse.EnsureSuccessStatusCode();

                if (!apiResponse.IsSuccessStatusCode)
                {
                    response.AddErrorMessage($"Failed to send request with reason: {apiResponse.ReasonPhrase} and status code: {apiResponse.StatusCode}");
                    return response;
                }

                var responseString = await getRequestResponse.Content.ReadAsStringAsync();
                var modelResult = JsonConvert.DeserializeObject<ProductPriceModel>(responseString);

                response.Model = modelResult;
            }
            catch (Exception ex)
            {
                //TODO: Store exception to internal error handling (ELMAH, etc)

                response.AddErrorMessage($"Error when sending request");
            }

            return response;
        }

        public async Task<GenericGetModelResponse<ProductScheduleModel>> GetScheduleAsync(int productId, DateTime startDate)
        {
            var response = new GenericGetModelResponse<ProductScheduleModel>();
            try
            {
                var getRequestResponse = await _httpClient.GetAsync($"schedule/{productId}/{startDate.ToString("yyyy-MM-dd")}");
                var apiResponse = getRequestResponse.EnsureSuccessStatusCode();

                if (!apiResponse.IsSuccessStatusCode)
                {
                    response.AddErrorMessage($"Failed to send request with reason: {apiResponse.ReasonPhrase} and status code: {apiResponse.StatusCode}");
                    return response;
                }

                var responseString = await getRequestResponse.Content.ReadAsStringAsync();
                var modelResult = JsonConvert.DeserializeObject<ICollection<ProductScheduleModel>>(responseString);

                response.Models.AddRange(modelResult);
            }
            catch (Exception ex)
            {
                //TODO: Store exception to internal error handling (ELMAH, etc)

                response.AddErrorMessage($"Error when sending request");
            }

            return response;
        }

        #region Private Methods



        #endregion
    }
}