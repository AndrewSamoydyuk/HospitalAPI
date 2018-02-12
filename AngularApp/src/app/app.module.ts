import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';

import { HoverDirective } from './Directives/hover.directive';

import { AppComponent } from './app.component';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { ClinicDetailsComponent } from './Components/clinic-details/clinic-details.component';
import { PreloaderComponent } from './Components/preloader/preloader.component';
import { LoginComponent } from './Components/login/login.component';

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
        LoginComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
