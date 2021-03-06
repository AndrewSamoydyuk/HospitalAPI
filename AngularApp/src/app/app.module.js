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
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/common/http");
var app_routing_module_1 = require("./app-routing.module");
var hover_directive_1 = require("./Directives/hover.directive");
var click_directive_1 = require("./Directives/click.directive");
var clinics_service_1 = require("./Services/clinics.service");
var authentication_service_1 = require("./Services/authentication.service");
var jwt_interceptor_1 = require("./jwt.interceptor");
var app_component_1 = require("./app.component");
var home_page_component_1 = require("./Components/home-page/home-page.component");
var clinic_details_component_1 = require("./Components/clinic-details/clinic-details.component");
var preloader_component_1 = require("./Components/preloader/preloader.component");
var login_component_1 = require("./Components/login/login.component");
var shared_service_1 = require("./Services/shared.service");
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
                forms_1.FormsModule,
                app_routing_module_1.AppRoutingModule
            ],
            declarations: [
                app_component_1.AppComponent,
                hover_directive_1.HoverDirective,
                home_page_component_1.HomePageComponent,
                clinic_details_component_1.ClinicDetailsComponent,
                preloader_component_1.PreloaderComponent,
                login_component_1.LoginComponent,
                click_directive_1.ClickDirective
            ],
            bootstrap: [
                app_component_1.AppComponent
            ],
            providers: [
                clinics_service_1.ClinicsService,
                shared_service_1.SharedService,
                authentication_service_1.AuthenticationService,
                {
                    provide: http_1.HTTP_INTERCEPTORS,
                    useClass: jwt_interceptor_1.JwtInterceptor,
                    multi: true
                }
            ]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map