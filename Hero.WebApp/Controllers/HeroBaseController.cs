using Hero.WebApp.Service.Hero.Response;
using Microsoft.AspNetCore.Mvc;

namespace Hero.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroBaseController : ControllerBase
    {
        #region Protected Methods

        protected ActionResult SuccessResponseResult(object data)
        {
            return this.Ok(new
            {
                IsSuccess = true,
                Data = data
            });
        }

        protected ActionResult ErrorResponseResult(string message)
        {
            return this.Ok(new
            {
                IsSuccess = false,
                Data = message
            });
        }

        protected ActionResult ErrorResponseResult(BaseResponse baseResponse)
        {
            return this.Ok(new
            {
                IsSuccess = false,
                Data = baseResponse.GetErrorMessages()
            });
        }

        #endregion Protected Methods
    }
}