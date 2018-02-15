import { Component, Output, EventEmitter } from '@angular/core';
import { AuthenticationService } from '../../Services/authentication.service';
import { Location } from '@angular/common';
import { SharedService } from '../../Services/shared.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent {
    constructor(
        private authService: AuthenticationService,
        private location: Location,
        private sharedService : SharedService
    ) { }

    ngOnInit() {
        this.authService.logout();
    }

    login(email: string, password: string) {
        this.authService.login(email, password)
            .subscribe(() => {
                this.sharedService.showUser();
                this.location.back();
            });
    }

}