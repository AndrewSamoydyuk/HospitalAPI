import { Component } from '@angular/core';
import { Login } from '../../Models/login';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    loginModel: Login;

    login(email: string, password: string) {
        this.loginModel = { email : email, password : password };
    }
}