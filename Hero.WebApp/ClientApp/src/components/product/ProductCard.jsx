import { Button, Card, CardBody, CardImg, CardSubtitle, CardText, CardTitle } from 'reactstrap';
import React, { Component } from 'react';

export class ProductCard extends Component {

  constructor(props) {
    super(props);
  }

  render() {
    return (
        <Card className="mt-3">
        <CardImg top width="100%" src={this.props.item.imageUrl} alt={this.props.item.name} />
        <CardBody>
          <CardTitle tag="p"><b>{this.props.item.name}</b></CardTitle>
          <CardSubtitle tag="h6">{this.props.item.supplierName}</CardSubtitle>
          <CardText>{this.props.item.address}</CardText>
        </CardBody>
      </Card>
    );
  }
}
