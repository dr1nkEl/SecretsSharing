import * as React from 'react';
import { NavLink } from 'reactstrap';

export default class Authorization extends React.Component {
    constructor(props: any) {
        super(props);
    }

    render() {
        return (
            <div className="dropdown">
                <NavLink className="text-dark" id="dropdownMenuButton" data-toggle="dropdown" aria-expanded="false">
                    User Email
                </NavLink>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <a className="dropdown-item" href="#">Action</a>
                    <a className="dropdown-item" href="#">Another action</a>
                    <a className="dropdown-item" href="#">Something else here</a>
                </div>
            </div>
        )
    }
}