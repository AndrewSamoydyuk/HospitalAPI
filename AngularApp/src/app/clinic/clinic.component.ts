import { Component, Input } from '@angular/core';
import { Clinic } from '../Models/Clinic';

@Component({
    selector: 'app-clinic',
    templateUrl: './clinic.component.html',
    styleUrls: ['./clinic.component.css']
})
export class ClinicComponent {

    @Input() clinic: Clinic 

}