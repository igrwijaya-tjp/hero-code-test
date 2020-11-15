import './custom.css'

import React, { Component } from 'react';
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom';

import { Booking } from './page/booking/Booking';
import { BookingSuccess } from './page/booking/BookingSuccess';
import { Home } from './page/home/Home';
import { Layout } from './components/Layout';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Router>
          <Switch>
          <Route exact path='/' component={Home} />
        <Route exact path='/proceed-booking' component={Booking} />
        <Route path='/booking-success/:id/:paxid' component={BookingSuccess} />
          </Switch>
        </Router>
        
      </Layout>
    );
  }
}
