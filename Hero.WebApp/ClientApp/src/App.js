import './custom.css'

import React, { Component } from 'react';

import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { Home } from './page/home/Home';
import { Layout } from './components/Layout';
import { Route } from 'react-router';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
