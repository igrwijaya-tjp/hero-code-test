import {
  Button,
  Card,
  CardBody,
  CardImg,
  CardText,
  CardTitle,
  Modal,
  ModalBody,
  Spinner,
} from "reactstrap";
import React, {Component} from "react";

export class PopupSpinner extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Modal isOpen={true} size="sm">
        <ModalBody className="text-center">
          <Spinner color="primary" />
          <br /> {this.props.message}
        </ModalBody>
      </Modal>
    );
  }
}
