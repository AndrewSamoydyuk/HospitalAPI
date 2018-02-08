import { Clinic } from '../Models/Clinic';
import { ClinicDetails } from '../Models/ClinicDetails';
import { Http, Response } from '@angular/http'
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClinicsService {

    constructor(private http: Http) { }

    getClinics(): Observable<Clinic[]> {
        return this.http.get('http://localhost:49761/api/clinics')
            .map((response: Response) => <Clinic[]>response.json())
    }

    getClinic(id: number): Observable<ClinicDetails> {
        return this.http.get('http://localhost:49761/api/clinics/' + id.toString()).map((response: Response) => <ClinicDetails>response.json())
    }
}