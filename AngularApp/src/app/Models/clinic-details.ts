import { Department } from './Department';

export class ClinicDetails {
    Id: number;
    Name: string;
    Address: string;
    ImageUri: string;
    CountOfDepartments: number;
    CountOfDoctors: number;
    Departments: Department[];
}