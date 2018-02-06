import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ClinicComponent } from './clinic/clinic.component';
import { HttpModule } from '@angular/http';
import { HoverDirective } from './hover.directive';
import { HomePageComponent } from './home-page/home-page.component';
import { RouterModule } from '@angular/router';

const routes = [
    { path: '', component: HomePageComponent }
];

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        RouterModule.forRoot(routes)
    ],
    declarations: [
        AppComponent,
        ClinicComponent,
        HoverDirective,
        HomePageComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
