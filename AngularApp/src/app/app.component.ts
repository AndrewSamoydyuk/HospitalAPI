import { Component, OnDestroy } from '@angular/core';
import { CommonChild, eventSubscriber } from './common-child.interface';

import { SharedService } from './Services/shared.service';

@Component({
    selector: 'my-app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    currentUser: any;

    constructor(private sharedService: SharedService) {
        this.updateUserStatus = this.updateUserStatus.bind(this);
        eventSubscriber(sharedService.subscrShowUser, this.updateUserStatus, true)
    }

    ngOnInit() {
        this.updateUserStatus();
    }

    updateUserStatus() {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    hideUser() {
        this.currentUser = null;
    }

}
