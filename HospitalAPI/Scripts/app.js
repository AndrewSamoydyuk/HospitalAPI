function AddClinic(){
    var clinic = { Name: "NewName", Address: "newAddress", ImageUri: "Url" };
    $.ajax({
        type: "POST",
        data: JSON.stringify(clinic),
        url: "api/Clinics",
        contentType: "application/json"
    });
};

function DeleteClinic(id) {
    $.ajax({
        type: "DELETE",
        url: "api/Clinics/"+id
    });
};

function UpdateClinic() {
    var clinic = { Id:5, Name: "Update", Address: "Update", ImageUri: "Update" };
    $.ajax({
        type: "PUT",
        data: JSON.stringify(clinic),
        url: "api/Clinics/5",
        contentType: "application/json"
    });
};

function AddDepartment() {
    var department = { Name: "NewNameForDepartment", ClinicID: 1 };
    $.ajax({
        type: "POST",
        data: JSON.stringify(department),
        url: "api/clinics/addDepartment",
        contentType: "application/json"
    });
};
