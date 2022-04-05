import * as React from 'react';

export default class Upload extends React.Component {

    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <form>
                <input className="form-input" placeholder="roflanEbaloNaming" />
                <button type="button" className="btn btn-primary">Upload</button>
            </form>
        )
    }
}