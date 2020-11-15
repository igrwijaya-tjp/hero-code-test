using System.Collections.Generic;
using System.Linq;

namespace Hero.WebApp.Service.Hero.Response
{
    public class BaseResponse
    {
        #region Fields

        private ICollection<string> _errorMessages = new List<string>();

        #endregion Fields

        #region Public Methods

        public string[] GetErrorMessages()
        {
            return this._errorMessages.ToArray();
        }

        public bool IsError()
        {
            return this._errorMessages.Any();
        }

        public void AddErrorMessage(string message)
        {
            this._errorMessages.Add(message);
        }

        #endregion Public Methods
    }
}