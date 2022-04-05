import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
    <div>
        <h1>Just home page and nothing extra.</h1>
    </div>
);

export default connect()(Home);
