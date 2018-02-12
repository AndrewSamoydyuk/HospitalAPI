import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Clinic } from '../../Models/clinic';
import { ClinicsService } from '../../Services/clinics.service';
import { Location } from '@angular/common';

@Component({
    selector: 'item-info',
    templateUrl: './clinic-details.component.html',
    styleUrls: ['./clinic-details.component.css']
})

export class ClinicDetailsComponent {

    clinic: Clinic;

    constructor(
        private route: ActivatedRoute,
        private clinicsService: ClinicsService,
        private location: Location
    ) {}

    ngOnInit() {
        this.getClinic();
    }

    getClinic(): void {
        const id = +this.route.snapshot.paramMap.get('id');
        this.clinicsService.getClinic(id)
            .subscribe(clinic => this.clinic = clinic);
    }

    goBack(): void {
        this.location.back();
    }
}