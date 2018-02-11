import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Clinic } from '../../Models/Clinic';
import { ClinicsService } from '../../Services/clinics.service';

@Component({
    selector: 'item-info',
    templateUrl: './clinic-details.component.html',
    styleUrls: ['./clinic-details.component.css']
})

export class ClinicDetailsComponent {

    clinic: Clinic = new Clinic();
    id: number;
    constructor(private activateRoute: ActivatedRoute, private clinicsService: ClinicsService) {

        this.id = activateRoute.snapshot.params['id'];
    }

    ngOnInit() {
        this.clinicsService.getClinic(this.id)
            .subscribe(clinicData => {
                this.clinic = clinicData
            })
    }
}