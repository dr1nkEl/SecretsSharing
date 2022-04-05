import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import ListAndUpload from './components/File/ListAndUpload'

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/File' component={ListAndUpload} />
    </Layout>
);
