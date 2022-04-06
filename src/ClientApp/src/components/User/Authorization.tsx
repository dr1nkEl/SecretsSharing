import * as React from 'react';
import { NavLink } from 'reactstrap';

export default class Authorization extends React.Component {
    constructor(props: any) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    authRequest(email: string, password: string){
        $.ajax('/api/Account/Authorize',{
            method: 'POST',
            data: {email, password},
            dataType: 'json',
            async: true,
            success: function (response) {
                alert('Authorized.');
            },
            error: function (response) {
                alert('Wrong credentials.');
            }
        });
    };

    async handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        const email : string = String($('#authEmailInput').val());
        const password: string = String($('#authPasswordInput').val());
        this.authRequest(email, password);
    }

    render() {
        return (
            <div className="dropdown">
                <NavLink className="text-dark" id="dropdownMenuButton" data-toggle="dropdown" aria-expanded="false">
                    User Email
                </NavLink>
                <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <form onSubmit={this.handleSubmit}>
                        <input placeholder="email" id="authEmailInput"/>
                        <input placeholder="password" type="password" id="authPasswordInput" />
                        <button>Log in</button>
                    </form>
                </div>
            </div>
        )
    }
}