import { Component } from '@angular/core';
import { Clinic } from './Clinic';
import { ClinicsService } from './clinics.service';

@Component({
    selector: 'my-app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [ClinicsService]
})
export class AppComponent  {
    clinics: Clinic[] = []

    constructor(private clinicsService: ClinicsService) { }

    ngOnInit() {
        this.clinicsService.getClinics()
            .subscribe(clinicsData => { this.clinics = clinicsData }
            )
    }
}
