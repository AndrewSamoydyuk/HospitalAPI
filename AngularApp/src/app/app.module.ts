import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ClinicComponent } from './clinic/clinic.component';
import { HttpModule } from '@angular/http';
import { HoverDirective } from './Directives/hover.directive';
import { HomePageComponent } from './home-page/home-page.component';
import { RouterModule } from '@angular/router';
import { ClinicDetailsComponent } from './clinic-details/clinic-details.component';
import { PreloaderComponent } from './preloader/preloader.component';

const routes = [
    { path: '', component: HomePageComponent },
    { path: 'clinic/:id', component: ClinicDetailsComponent}
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
        HomePageComponent,
        ClinicDetailsComponent,
        PreloaderComponent
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
