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
var router_1 = require("@angular/router");
var clinics_service_1 = require("../../Services/clinics.service");
var common_1 = require("@angular/common");
var ClinicDetailsComponent = /** @class */ (function () {
    function ClinicDetailsComponent(route, clinicsService, location) {
        this.route = route;
        this.clinicsService = clinicsService;
        this.location = location;
    }
    ClinicDetailsComponent.prototype.ngOnInit = function () {
        this.getClinic();
    };
    ClinicDetailsComponent.prototype.getClinic = function () {
        var _this = this;
        var id = +this.route.snapshot.paramMap.get('id');
        this.clinicsService.getClinic(id)
            .subscribe(function (clinic) { return _this.clinic = clinic; });
    };
    ClinicDetailsComponent.prototype.goBack = function () {
        this.location.back();
    };
    ClinicDetailsComponent = __decorate([
        core_1.Component({
            selector: 'item-info',
            templateUrl: './clinic-details.component.html',
            styleUrls: ['./clinic-details.component.css']
        }),
        __metadata("design:paramtypes", [router_1.ActivatedRoute,
            clinics_service_1.ClinicsService,
            common_1.Location])
    ], ClinicDetailsComponent);
    return ClinicDetailsComponent;
}());
exports.ClinicDetailsComponent = ClinicDetailsComponent;
//# sourceMappingURL=clinic-details.component.js.map