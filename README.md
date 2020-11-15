# hero-code-test

# Spec
- Using .NET Core SDK 3.1
- ReactJs (need install nodejs to run this app)

# Setup
- Make sure nodejs has been installed
- update secret file:
  - Add Hero API Key: Right click the project --> Manage User Secrets --> Follow format below:
```sh
{
  "HeroApi": {
    "Key": "API_KEY"
  }
}
```

# Development Process
- I start with creating new project and setup HttpClient (+- 1 hour)
- Continue to create UI to search and show the search result (+- 30 minutes)
- Creating API and UI to get the price based-on selected date (+- 2 hours). This process take a long time because many of the product doesn't have availability so I finding the way to know what product that have availability and when the date of the availability
- After that creating booking API's (Create Pax, Create Booking, Create Payment, Finalize and download Voucher) (+- 1 hours)
- Create the booking mechanism UI and implement the API's (+- 30 minutes)
