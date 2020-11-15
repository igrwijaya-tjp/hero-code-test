using Hero.WebApp.Service.Hero;
using Microsoft.AspNetCore.Mvc;

namespace Hero.WebApp.Controllers
{
    [Route("api/[controller]")]
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



        #endregion
    }
}