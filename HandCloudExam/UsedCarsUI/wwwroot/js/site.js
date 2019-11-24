// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var webapi = "https://localhost:44389/";

$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: webapi + "hc/UsedCar/GetUsedCars",
        type: "GET",
        crossDomain: true,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        statusCode: {
            200: function (response) {
                var html = '';
                $.each(response, function (key, item) {
                    html += '<tr>';
                    html += '<td>' + item.id + '</td>';
                    html += '<td>' + item.model + '</td>';
                    html += '<td>' + item.description + '</td>';
                    html += '<td>' + item.year + '</td>';
                    html += '<td>' + item.brand + '</td>';
                    html += '<td>' + item.kilometers + '</td>';
                    html += '<td>' + item.price + '</td>';
                    html += '<td><button class="btn btn-primary" onclick="return getById(' + item.id + ')">Edit</button> ';
                    html += '<button class="btn btn-danger" onclick = "Delete(' + item.id + ')" > Delete</a ></td > ';
                    html += '</tr>';
                });
                $('.tbody').html(html);
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Add() {
    if (!validateForm()) {
        return false;
    }
    var car = {
        Id: 0,
        Model: $('#model').val(),
        Description: $('#description').val(),
        Year: $('#year').val(),
        Brand: $('#brand').val(),
        Kilometers: $('#kilometers').val(),
        Price: $('#price').val()
    };
    $.ajax({
        url: webapi + "hc/UsedCar/AddUsedCar",
        data: JSON.stringify(car),
        type: "POST",
        crossDomain: true,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        statusCode: {
            200: function (response) {
                loadData();
                $('#myModal').modal('hide');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function getById(id) {
    $.ajax({
        url: webapi + "hc/UsedCar/GetUsedCar/?Id=" + id,
        type: "GET",
        crossDomain: true,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        statusCode: {
            200: function (result) {
                $('#id').val(result.id);
                $('#model').val(result.model);
                $('#description').val(result.description);
                $('#year').val(result.year);
                $('#brand').val(result.brand);
                $('#kilometers').val(result.kilometers);
                $('#price').val(result.price);

                $('#myModal').modal('show');
                $('#btnUpdate').show();
                $('#btnAdd').hide();
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Update() {
    if (!validateForm())
        return false;
    var car = {
        Id: $('#id').val(),
        Model: $('#model').val(),
        Description: $('#description').val(),
        Year: $('#year').val(),
        Brand: $('#brand').val(),
        Kilometers: $('#kilometers').val(),
        Price: $('#price').val()
    };
    $.ajax({
        url: webapi + "hc/UsedCar/UpdateUsedCar",
        data: JSON.stringify(car),
        type: "PUT",
        crossDomain: true,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        statusCode: {
            200: function (response) {
                loadData();
                $('#myModal').modal('hide');
                $('#id').val("");
                $('#model').val("");
                $('#description').val("");
                $('#year').val("");
                $('#brand').val("");
                $('#kilometers').val("");
                $('#price').val("");
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function Delete(id) {
    if (confirm("Are you sure for this?")) {
        $.ajax({
            url: webapi + "hc/UsedCar/DeleteUsedCar?Id=" + id,
            type: "DELETE",
            crossDomain: true,
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            statusCode: {
                200: function (response) {
                    loadData();
                }
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function clearFields() {
    $('#id').val("");
    $('#model').val("");
    $('#description').val("");
    $('#year').val("");
    $('#brand').val("");
    $('#kilometers').val("");
    $('#price').val("");

    $('#model').css('border-color', 'LightGrey');
    $('#brand').css('border-color', 'LightGrey');
    $('#kilometers').css('border-color', 'LightGrey');
    $('#price').css('border-color', 'LightGrey');

    $('#btnUpdate').hide();
    $('#btnAdd').show();
}

function validateForm() {
    var isValidForm = true;
    var isNotEmpty = true;
    var isNumeric = true;

    if ($('#model').val().trim() == "") {
        $('#model').css('border-color', 'Red');
        isNotEmpty = false;
    } else {
        $('#model').css('border-color', 'LightGrey');
    }
    if (!$.isNumeric($('#year').val())) {
        $('#year').css('border-color', 'Red');
        isNumeric = false;
    } else {
        $('#year').css('border-color', 'LightGrey');
    }
    if ($('#brand').val().trim() == "") {
        $('#brand').css('border-color', 'Red');
        isNotEmpty = false;
    } else {
        $('#brand').css('border-color', 'LightGrey');
    }

    if ($('#kilometers').val().trim() == "") {
        $('#kilometers').css('border-color', 'Red');
        isNotEmpty = false;
    } else if (!$.isNumeric($('#kilometers').val())) {
        $('#kilometers').css('border-color', 'Red');
        isNumeric = false;
    } else {
        $('#kilometers').css('border-color', 'LightGrey');
    }

    if ($('#price').val().trim() == "") {
        $('#price').css('border-color', 'Red');
        isNotEmpty = false;
    } else if (!$.isNumeric($('#price').val())) {
        $('#price').css('border-color', 'Red');
        isNumeric = false;
    } else {
        $('#price').css('border-color', 'LightGrey');
    }

    if (!isNumeric) {
        isValidForm = false;
        $('#status').html("Warning: This value must be numeric");
    }

    if (!isNotEmpty) {
        isValidForm = false;
        $('#status').html("Warning: There are empty fields");
    }

    if (isValidForm)
        $('#status').empty();
        
    return isValidForm;
}