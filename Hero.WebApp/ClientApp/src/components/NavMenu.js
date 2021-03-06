import "./NavMenu.css";

import {Container, Navbar, NavbarBrand} from "reactstrap";
import React, {Component} from "react";

import {Link} from "react-router-dom";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  }

  render() {
    return (
      <header>
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
          light
        >
          <Container>
            <NavbarBrand tag={Link} to="/">
              Hero Booking App
            </NavbarBrand>
          </Container>
        </Navbar>
      </header>
    );
  }
}
