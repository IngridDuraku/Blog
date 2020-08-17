// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function clearError() {
    document.getElementById("error").innerText = "";
}

function clearErrorMsg() {
    var errors = document.getElementsByName("error");
    for (var i = 0; i < errors.length; i++) {
        errors[i].innerText = "";
        errors[i].value = "";
    }
}