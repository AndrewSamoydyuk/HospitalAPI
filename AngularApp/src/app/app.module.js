"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var app_component_1 = require("./app.component");
var clinic_component_1 = require("./clinic/clinic.component");
var http_1 = require("@angular/common/http");
var hover_directive_1 = require("./Directives/hover.directive");
var home_page_component_1 = require("./home-page/home-page.component");
var router_1 = require("@angular/router");
var clinic_details_component_1 = require("./clinic-details/clinic-details.component");
var preloader_component_1 = require("./preloader/preloader.component");
var routes = [
    { path: '', component: home_page_component_1.HomePageComponent },
    { path: 'clinic/:id', component: clinic_details_component_1.ClinicDetailsComponent }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
                router_1.RouterModule.forRoot(routes)
            ],
            declarations: [
                app_component_1.AppComponent,
                clinic_component_1.ClinicComponent,
                hover_directive_1.HoverDirective,
                home_page_component_1.HomePageComponent,
                clinic_details_component_1.ClinicDetailsComponent,
                preloader_component_1.PreloaderComponent
            ],
            bootstrap: [
                app_component_1.AppComponent
            ]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map