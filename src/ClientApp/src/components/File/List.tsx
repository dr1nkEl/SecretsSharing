import * as React from 'react';

export default class List extends React.Component {

    constructor(props: any) {
        super(props);
    }

    private someFiles = [
        {
            name: "asd",
        },
        {
            name: "dsa"
        }
    ];

    //files = this.props.files.map((file) =>
    //    <li>{file.Name}</li>);

    private files = this.someFiles.map((file) =>
        <li>{file.name}</li>);

    render() {
        return (
            <li>
                {this.files}
            </li>
        )
    }
}