import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders} from '@angular/common/http';

import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { Clinic } from '../Models/clinic';
import { ClinicDetails } from '../Models/clinic-details';

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };

@Injectable()
export class ClinicsService {

    private clinicUrl = 'http://localhost:49761/api/clinics';

    constructor(private http: HttpClient) { }

    getClinics(): Observable<Clinic[]> {
        return this.http.get(this.clinicUrl).map((data: Clinic[]) => {
            return data.map((clinic: Clinic) => {
                    return { Id: clinic.Id, Name: clinic.Name, Address: clinic.Address, ImageUri: clinic.ImageUri };
                });
            });
    }

    getClinic(id: number): Observable<ClinicDetails> {
        const params = new HttpParams().set('id', id.toString());
        return this.http.get(this.clinicUrl, { params })
            .map((clinic: ClinicDetails) => { return clinic });
    }
}