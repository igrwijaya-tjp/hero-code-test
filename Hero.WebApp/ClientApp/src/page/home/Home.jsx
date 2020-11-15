import {Col, Form, FormGroup, Input, Label, Row} from "reactstrap";
import React, {Component} from "react";

import {PopupSpinner} from "../../components/spinner/PopupSpinner";
import {ProductCard} from "../../components/product/ProductCard";
import productApiService from "../../api/product/ProductApiService";

export class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      keyword: "",
      isSearching: false,
      searchResult: null,
    };
  }

  searchProduct = (e) => {
    e.preventDefault();

    this.setState({
      isSearching: true,
      searchResult: null,
    });

    productApiService
      .search(this.state.keyword)
      .then((response) => {
        this.setState({
          isSearching: false,
          searchResult: response.data,
        });
      })
      .catch((errorResponse) => {
        console.log(errorResponse);
      });
  };

  formListener = (event) => {
    const field = event.target.name;
    const value = event.target.value;

    this.setState((prevState) => ({
      ...prevState,
      [field]: value,
    }));
  };

  render() {
    return (
      <div id="home">
        <section id="search-form">
          <Form onSubmit={this.searchProduct}>
            <FormGroup>
              <Input
                value={this.state.keyword}
                onChange={this.formListener}
                type="text"
                name="keyword"
                placeholder="type something and hit enter"
                className="text-center"
              />
            </FormGroup>
            {this.state.isSearching && <PopupSpinner message="searching..." />}
          </Form>
        </section>

        <section id="search-result">
          <Row>
            {this.state.searchResult !== null &&
              this.state.searchResult.map((item, index) => (
                <Col key={index} md="4">
                  <ProductCard item={item} />
                </Col>
              ))}
          </Row>
        </section>
      </div>
    );
  }
}
