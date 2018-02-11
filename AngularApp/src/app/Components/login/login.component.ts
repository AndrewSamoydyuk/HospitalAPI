import { Component } from '@angular/core';
import { Login } from '../../Models/Login';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    loginModel: Login = new Login("","");

    login(email: string, password: string) {
        this.loginModel = new Login(email, password);
    }
}