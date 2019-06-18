$(document).ready(function () {
    $("#closeDietForm").click(function () {
        $("#myForm").hide(1000);
    });

    $("#openDietForm").click(function () {
        $("#myForm").show(1000);
    });

    $("#submitDietForm").click(function () {
        alert("Ще получите вашият хранителен режим на имейл");
        $("#myForm").hide(1000);
    });
});