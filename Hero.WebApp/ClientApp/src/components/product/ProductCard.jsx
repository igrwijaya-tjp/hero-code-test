import {
  Button,
  Card,
  CardBody,
  CardImg,
  CardSubtitle,
  CardText,
  CardTitle,
} from "reactstrap";
import React, {Component} from "react";

import {BookingModal} from "../booking/BookingModal";

export class ProductCard extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isShowBookingModal: false,
    };
  }

  showBookingModal = () => {
    this.setState({
      isShowBookingModal: true,
    });
  };

  hideBookingModal = () => {
    this.setState({
      isShowBookingModal: false,
    });
  };

  checkSchedule = (selectedDate) => {
    console.log(selectedDate);
  };

  render() {
    return (
      <>
        <Card className="mt-3" onClick={this.showBookingModal}>
          <CardBody>
            <CardTitle tag="p">
              <b>{this.props.item.name}</b>
            </CardTitle>
            <CardSubtitle tag="h6">{this.props.item.supplierName}</CardSubtitle>
            <CardText>{this.props.item.address}</CardText>
          </CardBody>
        </Card>
        {this.state.isShowBookingModal && (
          <BookingModal productId={this.props.item.id} onHide={this.hideBookingModal} />
        )}
      </>
    );
  }
}
