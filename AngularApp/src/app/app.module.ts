import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ClinicComponent } from './clinic/clinic.component';
import { HttpModule } from '@angular/http';
import { HoverDirective } from './hover.directive';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule
    ],
    declarations: [
        AppComponent,
        ClinicComponent,
        HoverDirective
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
