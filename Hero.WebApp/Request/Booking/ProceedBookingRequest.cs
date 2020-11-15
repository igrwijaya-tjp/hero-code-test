namespace Hero.WebApp.Request.Booking
{
    public class ProceedBookingRequest
    {
        #region Properties

        public int ProductId { get; set; }

        public string BookDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        #endregion Properties
    }
}