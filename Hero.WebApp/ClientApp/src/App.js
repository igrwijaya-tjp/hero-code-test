import './custom.css'

import React, { Component } from 'react';

import { Booking } from './page/booking/Booking';
import { Home } from './page/home/Home';
import { Layout } from './components/Layout';
import { Route } from 'react-router';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/proceed-booking' component={Booking} />
      </Layout>
    );
  }
}
