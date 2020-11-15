import Axios from "axios";

export default class BaseApiService {
  apiUrl = "";

  constructor(controller) {
    this.apiUrl = `api/${controller}`;
  }

  getHttpHeader() {
    this.httpHeader = {
      "Content-Type": "application/json",
    };

    return this.httpHeader;
  }

  postRequest(url, jsonData, successCallback, failCallback) {
    return new Promise((resolve, reject) => {
      Axios.post(url, jsonData, {
        headers: this.getHttpHeader(),
      })
        .then((response) => {
          if (response.data.isSuccess) {
            if (successCallback !== undefined) {
              successCallback(response.data);
            }

            resolve(response.data);
          } else {
            if (failCallback !== undefined) {
              failCallback(response.data);
            }

            reject(response.data);
          }
        })
        .catch((error) => {
          if (failCallback !== undefined) {
            failCallback(error);
          }

          reject(error);
        });
    });
  }

  getRequest(url, successCallback, failCallback) {
    return new Promise((resolve, reject) => {
      Axios.get(url, {
        headers: this.getHttpHeader(),
      })
        .then((response) => {
          if (response.data.isSuccess) {
            if (successCallback !== undefined) {
              successCallback(response.data);
            }

            resolve(response.data);
          } else {
            if (failCallback !== undefined) {
              failCallback(response.data);
            }

            reject(response.data);
          }
        })
        .catch((error) => {
          if (failCallback !== undefined) {
            failCallback(error);
          }

          reject(error);
        });
    });
  }
}
