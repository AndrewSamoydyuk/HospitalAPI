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
require("rxjs/add/operator/map");
var httpOptions = { headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' }) };
var ClinicsService = /** @class */ (function () {
    function ClinicsService(http) {
        this.http = http;
        this.clinicUrl = 'http://localhost:49761/api/clinics';
    }
    ClinicsService.prototype.getClinics = function () {
        return this.http.get(this.clinicUrl).map(function (data) {
            return data.map(function (clinic) {
                return { Id: clinic.Id, Name: clinic.Name, Address: clinic.Address, ImageUri: clinic.ImageUri };
            });
        });
    };
    ClinicsService.prototype.getClinic = function (id) {
        var params = new http_1.HttpParams().set('id', id.toString());
        return this.http.get(this.clinicUrl, { params: params })
            .map(function (clinic) { return clinic; });
    };
    ClinicsService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], ClinicsService);
    return ClinicsService;
}());
exports.ClinicsService = ClinicsService;
//# sourceMappingURL=clinics.service.js.map