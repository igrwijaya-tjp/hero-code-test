import {Button, Modal, ModalBody} from "reactstrap";
import React, {Component} from "react";

export class ConfirmationDialog extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Modal isOpen={true}>
        <ModalBody className="text-center">
          <div dangerouslySetInnerHTML={{__html: this.props.message}} />
          <br />
          <br />
          <Button color="primary" onClick={this.props.onYes}>
            Yes
          </Button>{" "}
          <Button color="secondary" onClick={this.props.onNo}>
            No
          </Button>
        </ModalBody>
      </Modal>
    );
  }
}
