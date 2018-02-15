import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class SharedService {
    subscrShowUser = new Subject();

    showUser() {
        this.subscrShowUser.next();
    }

}