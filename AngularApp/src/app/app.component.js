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
var common_child_interface_1 = require("./common-child.interface");
var shared_service_1 = require("./Services/shared.service");
var AppComponent = /** @class */ (function () {
    function AppComponent(sharedService) {
        this.sharedService = sharedService;
        this.updateUserStatus = this.updateUserStatus.bind(this);
        common_child_interface_1.eventSubscriber(sharedService.subscrShowUser, this.updateUserStatus, true);
    }
    AppComponent.prototype.ngOnInit = function () {
        this.updateUserStatus();
    };
    AppComponent.prototype.updateUserStatus = function () {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    };
    AppComponent.prototype.hideUser = function () {
        this.currentUser = null;
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css']
        }),
        __metadata("design:paramtypes", [shared_service_1.SharedService])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map