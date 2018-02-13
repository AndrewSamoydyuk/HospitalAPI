"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Department = /** @class */ (function () {
    function Department(Id, Name, ResultsOfTreatment) {
        this.Id = Id;
        this.Name = Name;
        this.ResultsOfTreatment = ResultsOfTreatment;
    }
    return Department;
}());
exports.Department = Department;
var ResultsOfTreatment = /** @class */ (function () {
    function ResultsOfTreatment(CountOfCured, CountOfNotCured, CountOfOnTreatment) {
        this.CountOfCured = CountOfCured;
        this.CountOfNotCured = CountOfNotCured;
        this.CountOfOnTreatment = CountOfOnTreatment;
    }
    return ResultsOfTreatment;
}());
//# sourceMappingURL=Department.js.map