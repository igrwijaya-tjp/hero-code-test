import {Button, Modal, ModalBody} from "reactstrap";
import React, {Component} from "react";

export class InformationDialog extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Modal isOpen={true}>
        <ModalBody className="text-center">
          {this.props.message}
          <br />
          <br />
          <Button color="secondary" onClick={this.props.onHide}>
            Close
          </Button>
        </ModalBody>
      </Modal>
    );
  }
}
