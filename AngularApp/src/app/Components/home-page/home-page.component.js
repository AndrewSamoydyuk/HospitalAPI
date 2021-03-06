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
var clinics_service_1 = require("../../Services/clinics.service");
var HomePageComponent = /** @class */ (function () {
    function HomePageComponent(clinicsService) {
        this.clinicsService = clinicsService;
        this.clinics = [];
        this.done = true;
    }
    HomePageComponent.prototype.ngOnInit = function () {
        this.getClinics();
    };
    HomePageComponent.prototype.getClinics = function () {
        var _this = this;
        this.clinicsService.getClinics()
            .subscribe(function (clinics) {
            _this.clinics = clinics;
            _this.done = false;
        });
    };
    HomePageComponent.prototype.addClinic = function (name, address) {
        var _this = this;
        var clinic = { Id: 1, Name: name, Address: address, ImageUri: "image.png" };
        var formData = new FormData();
        var fileBrowser = this.clinicImage.nativeElement;
        formData.append("image", fileBrowser.files[0]);
        this.clinicsService.addClinic(clinic)
            .subscribe(function (data) {
            clinic = data;
            _this.updateImage(formData, clinic.Id);
        });
    };
    HomePageComponent.prototype.updateImage = function (formData, Id) {
        var _this = this;
        this.clinicsService.updateImage(formData, Id)
            .subscribe(function () { return _this.getClinics(); });
    };
    HomePageComponent.prototype.updateClinic = function (clinic) {
        var _this = this;
        this.clinicsService.updateClinic(clinic)
            .subscribe(function (clinic) { return _this.clinics.push(clinic); });
    };
    HomePageComponent.prototype.deleteClinic = function (id) {
        this.clinicsService.deleteClinic(id)
            .subscribe();
        this.clinics = this.clinics.filter(function (h) { return h.Id !== id; });
    };
    __decorate([
        core_1.ViewChild('clinicImage'),
        __metadata("design:type", Object)
    ], HomePageComponent.prototype, "clinicImage", void 0);
    HomePageComponent = __decorate([
        core_1.Component({
            selector: 'app-home-page',
            templateUrl: './home-page.component.html',
            styleUrls: ['./home-page.component.css']
        }),
        __metadata("design:paramtypes", [clinics_service_1.ClinicsService])
    ], HomePageComponent);
    return HomePageComponent;
}());
exports.HomePageComponent = HomePageComponent;
//# sourceMappingURL=home-page.component.js.map