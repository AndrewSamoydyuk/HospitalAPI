import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomePageComponent } from './Components/home-page/home-page.component';
import { ClinicDetailsComponent } from './Components/clinic-details/clinic-details.component';
import { LoginComponent } from './Components/login/login.component';

const routes = [
    { path: '', component: HomePageComponent },
    { path: 'clinic/:id', component: ClinicDetailsComponent },
    { path: 'login', component: LoginComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
