import { Component } from '@angular/core';
import { ClinicsService } from './Services/clinics.service';

@Component({
    selector: 'my-app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [ClinicsService]
})
export class AppComponent  {}
