using System.Collections.Generic;

namespace Hero.WebApp.Service.Hero.Response
{
    public class GenericGetModelResponse<TModel> : BaseResponse
    {
        #region Fields

        private List<TModel> _models;

        #endregion Fields

        #region Properties

        public List<TModel> Models => this._models ??= new List<TModel>();

        #endregion Properties
    }
}