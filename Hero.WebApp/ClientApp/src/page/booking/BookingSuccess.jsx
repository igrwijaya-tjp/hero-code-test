import React, {Component} from "react";

import {Button} from "reactstrap";
import {InformationDialog} from "../../components/dialog/InformationDialog";
import {PopupSpinner} from "../../components/spinner/PopupSpinner";
import bookingApiService from "../../api/booking/BookingApiService";

export class BookingSuccess extends Component {
  constructor(props) {
    super(props);

    this.state = {
      bookingId: this.props.match.params.id,
      paxId: this.props.match.params.paxid,
      isLoading: false,
      informationDialog: {
        show: false,
        message: "",
      },
    };
  }

  downloadVoucher = () => {
    this.setState({
      isLoading: true,
    });

    bookingApiService
      .getVoucher(this.state.bookingId, this.state.paxId)
      .then((response) => {
        window.location.href = response.data;
      })
      .catch((errorResponse) => {
        this.showInformationDialog(errorResponse.message);
      });
  };

  showInformationDialog = (message) => {
    this.setState({
      isLoading: false,
      informationDialog: {
        show: true,
        message: message,
      },
    });
  };

  hideInformationDialog = () => {
    this.setState({
      isLoading: false,
      informationDialog: {
        show: false,
        message: "",
      },
    });
  };

  render() {
    return (
      <div id="booking-success" className="text-center">
        <h5>Booking Success!</h5>
        <p>Code: {this.state.bookingId}</p>
        <br />
        <br />
        <Button color="primary" onClick={this.downloadVoucher}>
          Download Voucher
        </Button>
        {this.state.isLoading && <PopupSpinner />}
        {this.state.informationDialog.show && (
          <InformationDialog
            message={this.state.informationDialog.message}
            onHide={this.hideInformationDialog}
          />
        )}
      </div>
    );
  }
}
