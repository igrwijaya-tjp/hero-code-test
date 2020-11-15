import { Button, Col, Form, FormGroup, Input, Label, Row } from 'reactstrap';
import React, { Component } from 'react';

export class Booking extends Component {

  constructor(props) {
    super(props);

    this.state = {
        firstName: '',
        lastName: '',
        phoneNumber: '',
        email: ''
    }
  }

  componentDidMount = () => {
      var bookingProductId = window.localStorage.getItem('booking__productId');

      if(bookingProductId === null){
          window.location.href = "/";
      }
  };

  formListener = event => {
    const field = event.target.name;
    const value = event.target.value;

    this.setState(prevState => ({
      ...prevState,
      [field]: value
    }));
  };

  render() {
    return (
      <div id="home">
        <section id="booking-form">
            <h5>Booking Form</h5>
            <hr/>
        <Form>
        <FormGroup row>
        <Label for="firstName" sm={2}>First Name</Label>
        <Col sm={10}>
          <Input value={this.state.firstName} 
          onChange={this.formListener} type="text" name="firstName" id="firstName" />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Label for="lastName" sm={2}>Last Name</Label>
        <Col sm={10}>
          <Input value={this.state.lastName} 
          onChange={this.formListener} type="text" name="lastName" id="lastName" />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Label for="phoneNumber" sm={2}>Phone Number</Label>
        <Col sm={10}>
          <Input value={this.state.phoneNumber} 
          onChange={this.formListener} type="number" name="phoneNumber" id="phoneNumber" />
        </Col>
      </FormGroup>
      <FormGroup row>
        <Label for="customerEmail" sm={2}>Email</Label>
        <Col sm={10}>
          <Input value={this.state.email} 
          onChange={this.formListener} type="email" name="email" id="customerEmail" />
        </Col>
      </FormGroup>
      <FormGroup check row>
        <Col sm={{ size: 10, offset: 2 }}>
          <Button color="primary">Book Now</Button>
        </Col>
      </FormGroup>
    </Form>
        </section>
      </div>
    );
  }
}
