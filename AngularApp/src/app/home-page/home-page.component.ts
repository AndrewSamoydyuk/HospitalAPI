import { Component } from '@angular/core';
import { Clinic } from '../Clinic';
import { ClinicsService } from '../clinics.service';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
    clinics: Clinic[] = []

    constructor(private clinicsService: ClinicsService) { }

    ngOnInit() {
        this.clinicsService.getClinics()
            .subscribe(clinicsData => { this.clinics = clinicsData }
            )
    }
}