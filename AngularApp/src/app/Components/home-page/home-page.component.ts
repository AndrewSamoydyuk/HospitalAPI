import { Component } from '@angular/core';
import { Clinic } from '../../Models/clinic';
import { ClinicsService } from '../../Services/clinics.service';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
    clinics: Clinic[] = [];

    constructor(private clinicsService: ClinicsService) { }

    done: boolean = true;

    ngOnInit() {
        this.getClinics();
    }

    getClinics(): void {
        this.clinicsService.getClinics()
            .subscribe(clinics => {
                this.clinics = clinics;
                this.done = false;
            });
    }

    addClinic(clinic: Clinic): void {
        this.clinicsService.addClinic(clinic)
            .subscribe(clinic => this.clinics.push(clinic));
    }

    uodateClinic(clinic: Clinic): void {
        this.clinicsService.updateClinic(clinic)
            .subscribe(clinic => this.clinics.push(clinic));
    }

    deleteClinic(id: number): void {
        this.clinicsService.deleteClinic(id)
            .subscribe();
    }

}