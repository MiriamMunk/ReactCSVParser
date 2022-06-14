import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import Layout from './Layout';
import Generate from './Pages/Generate';
import HomePage from './Pages/Home';
import UpoadPage from './Pages/Upload';

export default class App extends Component {
    render() {
        return (
            <Layout>
                <Route exact path='/' component={HomePage} />
                <Route exact path='/Generate' component={Generate} />
                <Route exact path='/Upload' component={UpoadPage} />
            </Layout>
        );
    }
}