import BaseApiService from '../BaseApiService';

class ProductApiService extends BaseApiService {
  constructor() {
    super('Product');
  }

  search(keyword) {
    const url = this.apiUrl + '/Search?keyword=' + keyword;

    return this.getRequest(url);
  }

  checkAvailability(productId, bookDate) {
    const url = this.apiUrl + '/CheckAvailability?productId=' + productId + '&bookDate=' + bookDate;

    return this.getRequest(url);
  } 
}

const productApiService = new ProductApiService();

export default productApiService;
