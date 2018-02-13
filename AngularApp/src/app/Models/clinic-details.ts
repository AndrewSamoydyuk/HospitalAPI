import { Department } from './Department';

export class ClinicDetails {
    constructor(
        public Id: number,
        public Name: string,
        public Address: string,
        public ImageUri: string,
        public CountOfDepartments: number,
        public CountOfDoctors: number,
        public Departments: Department[]
    ) { }
}