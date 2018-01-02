function AddClinic(){
    var customer = { Name: "NewName", Address: "newAddress", ImageUri: "Url" };
    $.ajax({
        type: "POST",
        data: JSON.stringify(customer),
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
    var customer = { Id:5, Name: "Update", Address: "Update", ImageUri: "Update" };
    $.ajax({
        type: "PUT",
        data: JSON.stringify(customer),
        url: "api/Clinics/5",
        contentType: "application/json"
    });
};