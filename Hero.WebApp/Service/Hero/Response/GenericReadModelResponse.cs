namespace Hero.WebApp.Service.Hero.Response
{
    public class GenericReadModelResponse<TModel> : BaseResponse
    {
        #region Properties

        public TModel Model { get; set; }

        #endregion Properties
    }
}