import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { HoverDirective } from './Directives/hover.directive';
import { ClickDirective } from './Directives/click.directive';

import { ClinicsService } from './Services/clinics.service';
import { AuthenticationService } from './Services/authentication.service';

import { JwtInterceptor } from './jwt.interceptor';
import { AppComponent } from './app.component';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { ClinicDetailsComponent } from './Components/clinic-details/clinic-details.component';
import { PreloaderComponent } from './Components/preloader/preloader.component';
import { LoginComponent } from './Components/login/login.component';
import { SharedService } from './Services/shared.service';


@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        HoverDirective,
        HomePageComponent,
        ClinicDetailsComponent,
        PreloaderComponent,
        LoginComponent,
        ClickDirective
    ],
    bootstrap: [
        AppComponent
    ],
    providers: [
        ClinicsService,
        SharedService,
        AuthenticationService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            multi: true
        }
    ]
})
export class AppModule { }
