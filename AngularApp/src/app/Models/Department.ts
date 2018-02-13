export class Department {
    constructor(
        public Id: number,
        public Name: string,
        public ResultsOfTreatment: ResultsOfTreatment
    ) { }
}

class ResultsOfTreatment {
    constructor(
        public CountOfCured: number,
        public CountOfNotCured: number,
        public CountOfOnTreatment: number
    ) { }
}