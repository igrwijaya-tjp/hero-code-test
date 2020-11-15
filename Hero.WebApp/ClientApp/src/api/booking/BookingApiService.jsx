import BaseApiService from '../BaseApiService';

class BookingApiService extends BaseApiService {
  constructor() {
    super('Booking');
  }

  proceed(request) {
    const url = this.apiUrl + '/Proceed';
    const data = JSON.stringify(request);
    
    return this.postRequest(url, data);
  }
  
  getVoucher(bookingId, paxId) {
    const url = this.apiUrl + '/GetVoucher?bookingId=' + bookingId + '&paxId' + paxId;

    return this.getRequest(url);
  }
}

const bookingApiService = new BookingApiService();

export default bookingApiService;
