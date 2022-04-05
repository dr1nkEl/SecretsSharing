import * as React from 'react';
import List from './List';
import Upload from './Upload';

export default class ListAndUpload extends React.Component{
    render() {
        return (
            <div>
                <Upload />
                <List />
            </div>
            )
    }
}