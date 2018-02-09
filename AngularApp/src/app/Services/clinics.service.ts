import { Clinic } from '../Models/Clinic';
import { ClinicDetails } from '../Models/ClinicDetails';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClinicsService {

    constructor(private http: HttpClient) { }

    getClinics(): Observable<Clinic[]> {
        return this.http.get('http://localhost:49761/api/clinics').map((data : Clinic[]) => {
            return data.map((clinic: Clinic) => {
                    return { Id: clinic.Id, Name: clinic.Name, Address: clinic.Address, ImageUri: clinic.ImageUri };
                });
            });
    }

    getClinic(id: number): Observable<ClinicDetails> {
        const params = new HttpParams().set('id', id.toString());
        return this.http.get('http://localhost:49761/api/clinics/', { params })
            .map((clinic: ClinicDetails) => { return clinic });
    }
}