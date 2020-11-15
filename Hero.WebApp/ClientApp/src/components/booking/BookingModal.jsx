import { Button, Form, FormGroup, Input, Label, Modal, ModalBody } from 'reactstrap';
import React, { Component } from 'react';

import { InformationDialog } from '../dialog/InformationDialog';
import { PopupSpinner } from '../spinner/PopupSpinner';
import productApiService from '../../api/product/ProductApiService';

export class BookingModal extends Component {

  constructor(props) {
    super(props);

    this.state = {
      isOpenModal: true,
      isProcessingRequest: false,
      bookDate: new Date(),
      informationDialog: {
        show: false,
        message: ''
      }
    }
  }

  formListener = event => {
    const field = event.target.name;
    const value = event.target.value;

    this.setState(prevState => ({
      ...prevState,
      [field]: value
    }));
  };

  checkSchedule = event => {
    event.preventDefault();

    this.setState({
      isOpenModal: false,
      isProcessingRequest: true
    })

    productApiService.checkAvailability(this.props.productId, this.state.bookDate)
    .then(response => {
      console.log(response);
    }).catch(errorResponse => {
      this.showInformationDialog(errorResponse.message);
    });
  }

  showInformationDialog = (message) => {
    this.setState({
      isOpenModal: false,
      isProcessingRequest: false,
      informationDialog: {
        show: true,
        message: message
      }
    });
  }

  hideInformationDialog = () => {
    this.setState({
      isOpenModal: true,
      isProcessingRequest: false,
      informationDialog: {
        show: false,
        message: ''
      }
    });
  }

  render() {
    return (
        <>
        <Modal isOpen={this.state.isOpenModal}>
            <ModalBody className="text-center">
                <Form onSubmit={this.checkSchedule}>
                <FormGroup>
        <Label for="exampleDate">When you want to book?</Label>
        <Input
          value={this.state.bookDate} 
          onChange={this.formListener}
          type="date"
          name="bookDate"
          placeholder="Please select the date"
        />
      </FormGroup>
      <Button color="primary">Check Schedule</Button>{' '}
      <Button color="secondary" type="button" onClick={this.props.onHide}>Close</Button>
                </Form>
            </ModalBody>
        </Modal>
        {this.state.isProcessingRequest && <PopupSpinner message="checking availability..." />}
        {this.state.informationDialog.show && <InformationDialog message={this.state.informationDialog.message} onHide={this.hideInformationDialog} />}
        </>
        
    );
  }
}
