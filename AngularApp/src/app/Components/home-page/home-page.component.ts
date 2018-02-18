import { Component, ViewChild } from '@angular/core';
import { Clinic } from '../../Models/clinic';
import { ClinicsService } from '../../Services/clinics.service';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html',
    styleUrls: ['./home-page.component.css']
})
export class HomePageComponent {
    clinics: Clinic[] = [];
    @ViewChild('clinicImage') clinicImage : any;

    constructor(private clinicsService: ClinicsService) { }

    done: boolean = true;

    ngOnInit() {
        this.getClinics();
    }

    getClinics(): void {
        this.clinicsService.getClinics()
            .subscribe(clinics => {
                this.clinics = clinics;
                this.done = false;
            });
    }

    addClinic(name: string, address: string): void {
        let clinic: Clinic = { Id: 1, Name: name, Address: address, ImageUri: "image.png" };
        const formData = new FormData();
        let fileBrowser = this.clinicImage.nativeElement;
        formData.append("image", fileBrowser.files[0])

        this.clinicsService.addClinic(clinic)
            .subscribe(data => {              
                clinic = data;
                this.updateImage(formData, clinic.Id);
            });

    }

    updateImage(formData: FormData, Id: number): void {
        this.clinicsService.updateImage(formData, Id)
            .subscribe(() => this.getClinics());
    }

    updateClinic(clinic: Clinic): void {
        this.clinicsService.updateClinic(clinic)
            .subscribe(clinic => this.clinics.push(clinic));
    }

    deleteClinic(id: number): void {        
        this.clinicsService.deleteClinic(id)
            .subscribe();
        this.clinics = this.clinics.filter(h => h.Id !== id);
    }

}