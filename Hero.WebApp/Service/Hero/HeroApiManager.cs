using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hero.WebApp.DataModel.Hero;
using Hero.WebApp.Service.Hero.Response;

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
                var getRequestResponse = await _httpClient.GetAsync($"search?q={keyword}&lat=0&lng=0");
                var apiResponse = getRequestResponse.EnsureSuccessStatusCode();

                if (!apiResponse.IsSuccessStatusCode)
                {
                    response.AddErrorMessage($"Failed to send request with reason: {apiResponse.ReasonPhrase} and status code: {apiResponse.StatusCode}");
                    return response;
                }

                using var responseStream = await getRequestResponse.Content.ReadAsStreamAsync();
                var modelResult = await JsonSerializer.DeserializeAsync<ICollection<SearchResponseModel>>(responseStream);

                response.Models.AddRange(modelResult);
            }
            catch(Exception ex)
            {
                //TODO: Store exception to internal error handling (ELMAH, etc)

                response.AddErrorMessage($"Error when sending request");
            }

            return response;
        }

    }
}
