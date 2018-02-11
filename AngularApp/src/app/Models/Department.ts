export class Department {
    Id: number;
    Name: string;
    ResultsOfTreatment: ResultsOfTreatment;
}

class ResultsOfTreatment {
    CountOfCured: number;
    CountOfNotCured: number;
    CountOfOnTreatment: number;
}