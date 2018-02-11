import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { HoverDirective } from './Directives/hover.directive';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { RouterModule } from '@angular/router';
import { ClinicDetailsComponent } from './Components/clinic-details/clinic-details.component';
import { PreloaderComponent } from './Components/preloader/preloader.component';
import { LoginComponent } from './Components/login/login.component';
import { FormsModule } from '@angular/forms';

const routes = [
    { path: '', component: HomePageComponent },
    { path: 'clinic/:id', component: ClinicDetailsComponent },
    { path: 'login', component: LoginComponent }
];

@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(routes)
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
