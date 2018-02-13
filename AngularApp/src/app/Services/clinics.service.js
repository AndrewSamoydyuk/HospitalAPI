"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var catchError_1 = require("rxjs/operators/catchError");
var retry_1 = require("rxjs/operators/retry");
var ErrorObservable_1 = require("rxjs/observable/ErrorObservable");
var httpOptions = {
    headers: new http_1.HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};
var ClinicsService = /** @class */ (function () {
    function ClinicsService(http) {
        this.http = http;
        this.clinicUrl = 'http://localhost:49761/api/clinics';
    }
    ClinicsService.prototype.getClinics = function () {
        return this.http.get(this.clinicUrl)
            .pipe(retry_1.retry(3), catchError_1.catchError(this.hangleError));
    };
    ClinicsService.prototype.getClinic = function (id) {
        var params = new http_1.HttpParams().set('id', id.toString());
        return this.http.get(this.clinicUrl, { params: params })
            .pipe(retry_1.retry(3), catchError_1.catchError(this.hangleError));
    };
    ClinicsService.prototype.addClinic = function (clinic) {
        return this.http.post(this.clinicUrl, clinic, httpOptions)
            .pipe(catchError_1.catchError(this.hangleError));
    };
    ClinicsService.prototype.deleteClinic = function (id) {
        var params = new http_1.HttpParams().set('id', id.toString());
        return this.http.delete(this.clinicUrl, { params: params })
            .pipe(catchError_1.catchError(this.hangleError));
    };
    ClinicsService.prototype.updateClinic = function (clinic) {
        var url = this.clinicUrl + "/" + clinic.Id;
        return this.http.put(url, clinic, httpOptions)
            .pipe(catchError_1.catchError(this.hangleError));
    };
    ClinicsService.prototype.hangleError = function (error) {
        if (error.error instanceof ErrorEvent) {
            //client-side or network error
            console.error('An error occured : ', error.error.message);
        }
        else {
            console.error("Server returned code " + error.status + ", body was: " + error.error);
        }
        return new ErrorObservable_1.ErrorObservable('Something bad happened, please try again later.');
    };
    ClinicsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], ClinicsService);
    return ClinicsService;
}());
exports.ClinicsService = ClinicsService;
//# sourceMappingURL=clinics.service.js.map