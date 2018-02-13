import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { catchError } from 'rxjs/operators/catchError';
import { retry } from 'rxjs/operators/retry';
import { Observable } from 'rxjs/Observable';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';

import { Clinic } from '../Models/clinic';
import { ClinicDetails } from '../Models/clinic-details';

const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class ClinicsService {

    private clinicUrl = 'http://localhost:49761/api/clinics';

    constructor(private http: HttpClient) { }

    getClinics(): Observable<Clinic[]> {

        return this.http.get<Clinic[]>(this.clinicUrl)
            .pipe(
                retry(3),
                catchError(this.hangleError)
            );
    }

    getClinic(id: number): Observable<ClinicDetails> {

        const params = new HttpParams().set('id', id.toString());

        return this.http.get<ClinicDetails>(this.clinicUrl, { params })
            .pipe(
                retry(3),
                catchError(this.hangleError)
            );
    }

    addClinic(clinic: Clinic): Observable<Clinic> {
        return this.http.post<Clinic>(this.clinicUrl, clinic, httpOptions)
            .pipe(
                catchError(this.hangleError)
            );
    }


    deleteClinic(id: number): Observable<{}> {
        const params = new HttpParams().set('id', id.toString());

        return this.http.delete(this.clinicUrl, { params })
            .pipe(
                catchError(this.hangleError)
            );
    }

    updateClinic(clinic: Clinic): Observable<Clinic> {

        const url = `${this.clinicUrl}/${clinic.Id}`;
        return this.http.put<Clinic>(url, clinic, httpOptions)
            .pipe(
                catchError(this.hangleError)
            );
    }

    private hangleError(error: HttpErrorResponse) {
        if (error.error instanceof ErrorEvent) {
            //client-side or network error
            console.error('An error occured : ', error.error.message);
        } else {
            console.error(`Server returned code ${error.status}, body was: ${error.error}`);
        }

        return new ErrorObservable('Something bad happened, please try again later.');
    }
}