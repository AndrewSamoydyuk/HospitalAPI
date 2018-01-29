$(function () {
    $('#submit').click(function (e) {
        e.preventDefault();
        var data = {
            Email: $('#email').val(),
            Password: $('#password').val(),
            ConfirmPassword: $('#confirmpassword').val(),
            FullName: $('#fullName').val(),
            Sex: $('#sex').val(),
            Address: $('#address').val(),
            Phone: $('#phone').val(),
            DateOfBirth: $('#dateOfBirth').val()
        };

        $.ajax({
            type: 'POST',
            url: '/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).success(function (data) {
            alert("Done");
        }).fail(function (data) {
            alert("Something went wrong");
        });
    });


    var tokenKey = "tokenInfo";
    $('#submitLogin').click(function (e) {
        e.preventDefault();
        var loginData = {
            grant_type: 'password',
            username: $('#emailLogin').val(),
            password: $('#passwordLogin').val()
        };

        $.ajax({
            type: 'POST',
            url: '/Token',
            data: loginData
        }).success(function (data) {
            $('.userName').text(data.userName);
            $('.userInfo').css('display', 'block');
            $('.loginForm').css('display', 'none');
            sessionStorage.setItem(tokenKey, data.access_token);
            console.log(data.access_token);
        }).fail(function (data) {
            alert('SOmething went wrong');
        });
    });

    $('#logOut').click(function (e) {
        e.preventDefault();
        sessionStorage.removeItem(tokenKey);
    });

    $('#getItemsButton').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'GET',
            url: '/api/patients/',
            beforeSend: function (xhr) {

                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function (data) {
                var i;
                for (i = 0; i < data.length; i++) {
                    $('#contentDiv').append(data[i].FullName + "<br/>");
                }
            },
            fail: function (data) {
                alert(data);
            }
        });
    });

    $('#getPatientInfo').click(function (e) {
        e.preventDefault();
        var id = $("#patientID").val();
        $.ajax({
            type: 'GET',
            url: '/api/patients/' + id,
            beforeSend: function (xhr) {

                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            success: function (data) {
                $('#contentDiv').append(JSON.stringify(data));              
            },
            fail: function (data) {
                alert(data);
            }
        });
    });

    $('#addClinic').click(function (e) {
        e.preventDefault();
        var clinic = { Name: "NewName", Address: "newAddress", ImageUri: "Url" };

        $.ajax({
            type: "POST",
            data: JSON.stringify(clinic),
            url: "api/Clinics",
            beforeSend: function (xhr) {

                var token = sessionStorage.getItem(tokenKey);
                xhr.setRequestHeader("Authorization", "Bearer " + token);
            },
            contentType: "application/json",

            success: function (data) {
                alert("Success");
            },
            fail: function (data) {
                alert(data);
            }
        });
    });

    $('#uploadImage').click(function (e) {
        e.preventDefault();
        var files = document.getElementById('uploadFile').files;

        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }

                $.ajax({
                    type: "POST",
                    url: "api/Clinics/5/updateImage",
                    contentType: false,
                    processData: false,
                    beforeSend: function (xhr) {

                        var token = sessionStorage.getItem(tokenKey);
                        xhr.setRequestHeader("Authorization", "Bearer " + token);
                    },
                    data: data,
                    success: function (data) {
                        alert("Success");
                    },
                    fail: function (data) {
                        alert(data);
                    }
                });
            }
        }
    });

});

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

function AddVisit() {
    var visit = { Date: "January 30 2018", Diagnosis: "NewDiagnos", DoctorID: 4 };
    $.ajax({
        type: "POST",
        data: JSON.stringify(visit),
        url: "api/patients/3/addvisit",
        contentType: "application/json"
    });
};

function AddMedication() {
    var medication = { CountOfDays: 7, MedicationID: 1 };
    $.ajax({
        type: "POST",
        data: JSON.stringify(medication),
        url: "api/patients/9/addMedication",
        contentType: "application/json"
    });
};

function ChangeStatus() {
    var status = 1;
    $.ajax({
        type: "PUT",
        data: JSON.stringify(status),
        url: "api/patients/6/changeStatus",
        contentType: "application/json"
    });
};



